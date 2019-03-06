using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : UIBase
{
    public int floorNumber = 0;
    [SerializeField] Text floorNumberText;
    [SerializeField] Text doorStatusText;
    [SerializeField] Image backGroundDoorStatus;
    [SerializeField] Button upButton;
    [SerializeField] Button downButton;

    Color doorStatusBackColor;
    GameLogic gameLogic;

    public enum CalledStatus {toUp, toDown, none}
    public CalledStatus status = CalledStatus.none;

    private void Start()
    {
        doorStatusBackColor = backGroundDoorStatus.color;
    }

    public void Init(int number)
    {
        gameLogic = AppManager.instance.gameLogic;
        gameLogic.openDoorEvent.AddListener(OpenDoorHandler);

        floorNumber = number;
        floorNumberText.text = (number + 1).ToString();
    }

    public void SetDoorStatus(bool oppened)
    {
        if (oppened)
        {
            doorStatusText.text = "Doors oppened";
            backGroundDoorStatus.color = Color.red;
        }
        else
        {
            doorStatusText.text = "Doors closed";
            backGroundDoorStatus.color = doorStatusBackColor;
        }

        UpButtonDisable();
        DownButtonDisable();
    }

    public void UpButtonEnable()
    {
        if (status == CalledStatus.toDown) return;

        gameLogic.AddLiftCall(floorNumber, true);

        upButton.GetComponent<Image>().color = Color.red;
        status = CalledStatus.toUp;
    }

    public void UpButtonDisable()
    {
        upButton.GetComponent<Image>().color = Color.black;
        status = CalledStatus.none;
    }

    public void DownButtonEnable()
    {
        if (status == CalledStatus.toUp) return;

        gameLogic.AddLiftCall(floorNumber, false);

        downButton.GetComponent<Image>().color = Color.red;
        status = CalledStatus.toDown;
    }

    public void DownButtonDisable()
    {
        downButton.GetComponent<Image>().color = Color.black;
        status = CalledStatus.none;
    }

    public void DisableButtons()
    {
        DownButtonDisable();
        UpButtonDisable();
    }

    public void OpenDoorHandler(int floor)
    {
        if (floor == floorNumber)
        {
            DisableButtons();
            status = CalledStatus.none;
        }
    }


}
