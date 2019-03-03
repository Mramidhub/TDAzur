using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    bool gameProcessOn = true;
    Build build;
    Lift lift;

    UnityEvent startGame = new UnityEvent();

    private void Start()
    {
        lift = FindObjectOfType<Lift>();
    }

    private void FixedUpdate()
    {
        if (gameProcessOn)
        {
            lift.UpdateState();
        }
    }

    public void InitGame(int floorCount)
    {
        build.MakeFloors(floorCount);


        startGame.Invoke();
        gameProcessOn = true;
    }


}
