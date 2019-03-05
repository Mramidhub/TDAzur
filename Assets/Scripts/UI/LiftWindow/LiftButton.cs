using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftButton : MonoBehaviour
{
    [SerializeField] Text numberText;
    int floorNumber = 0;
    GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
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
            gameLogic.AddDestinationFloor(floorNumber, GameLogic.LiftDirectionCall.none);
        }
    }
}
