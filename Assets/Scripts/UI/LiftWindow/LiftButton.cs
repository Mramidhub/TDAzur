using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftButton : UIBase
{
    [SerializeField] Text numberText;
    Image image;
    Color defaultColor;
    public int floorNumber = 0;
    GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    public void Init(int number)
    {
        floorNumber = number;
        numberText.text = (number + 1).ToString();
    }

    public void PressOn()
    {
        if (gameLogic)
        {
            var result = gameLogic.AddDestinationFloor(floorNumber);

            if (result)
            {
                image.color = Color.red;
                gameLogic.openDoorEvent.AddListener(DeactiveButton);
            }
        }
    }

    public void DeactiveButton(int floor)
    {
        if (floor == floorNumber)
        {
            image.color = defaultColor;
            gameLogic.openDoorEvent.RemoveListener(DeactiveButton);
        }
    }
}
