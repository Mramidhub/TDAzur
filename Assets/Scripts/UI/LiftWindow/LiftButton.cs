using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftButton : MonoBehaviour
{
    public int floorNumber = 0;
    GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void PressOn()
    {
        if (gameLogic)
        {
            Debug.Log("press floor " + floorNumber);
            gameLogic.AddDestinationFloor(floorNumber);
        }
    }
}
