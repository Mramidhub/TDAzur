using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : UIBase
{
    public int floorNumber = 0;
    [SerializeField] Text floorNumberText;
    [SerializeField] Text doorStatusText;
    [SerializeField] Button upButton;
    [SerializeField] Button downButton;

    GameLogic gameLogic;

    public enum CalledStatus {toUp, toDown, none}
    public CalledStatus status = CalledStatus.none;

    public void Init(int number)
    {
        gameLogic = AppManager.instance.gameLogic;

        floorNumber = number;
        floorNumberText.text = (number + 1).ToString();
    }

    public void SetDoorStatus(bool oppened)
    {
        if (oppened)
        {
            doorStatusText.text = "Doors oppened";
        }
        else
        {
            doorStatusText.text = "Doors closed";
        }

        UpButtonDisable();
        DownButtonDisable();
    }

    public void UpButtonEnable()
    {
        if (status == CalledStatus.toDown) return;

        gameLogic.AddDestinationFloor(floorNumber);


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

        gameLogic.AddDestinationFloor(floorNumber);

        downButton.GetComponent<Image>().color = Color.red;
        status = CalledStatus.toDown;
    }

    public void DownButtonDisable()
    {
        downButton.GetComponent<Image>().color = Color.black;
        status = CalledStatus.none;
    }


}
