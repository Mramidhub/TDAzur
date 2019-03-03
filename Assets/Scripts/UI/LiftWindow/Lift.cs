using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    ControlPanel panel;

    float liftSpeed = 1f;
    float liftSpeedTemp = 1f;
    float openDoorTime = 1f;
    float tempOpenDoorTime = 1f;
    int currentFloor = 0;

    List<int> destinationFloors = new List<int>();

    public enum LiftState { up, down, stopped, openned }
    LiftState liftState = LiftState.stopped;
    LiftState previosState = LiftState.stopped;



    public void UpdateState()
    {
        LiftMove();
    }

    public void Init(float newSpeed)
    {
        liftSpeed = newSpeed;
        liftSpeedTemp = newSpeed;
        tempOpenDoorTime = openDoorTime;
    }

    void LiftMove()
    {
        if (liftState == LiftState.stopped)
        {
            return;
        }

        if (liftState == LiftState.openned)
        {
            if (openDoorTime > 0)
            {
                openDoorTime -= Time.deltaTime;
                return;
            }

            openDoorTime = tempOpenDoorTime;
            liftState = previosState;
            return;
        }

        if (liftState == LiftState.up)
        {
            if (liftSpeed > 0)
            {
                liftSpeed -= Time.deltaTime;
                return;
            }

            currentFloor += 1;
            liftSpeed = liftSpeedTemp;

            if (destinationFloors.Contains(currentFloor))
            {
                previosState = liftState;
                liftState = LiftState.openned;
            }
        }
        else if(liftState == LiftState.down)
        {
            if (liftSpeed > 0)
            {
                liftSpeed -= Time.deltaTime;
                return;
            }

            currentFloor -= 1;
            liftSpeed = liftSpeedTemp;

            if (destinationFloors.Contains(currentFloor))
            {
                previosState = liftState;
                liftState = LiftState.openned;
            }
        }
    }


    public void Stop()
    {
        destinationFloors.Clear();
        liftSpeed = liftSpeedTemp;
        openDoorTime = tempOpenDoorTime;
    
    }

    public void AddDestinationFloor(int newFloorNumber)
    {
        if (liftState == LiftState.up)
        {
            if (newFloorNumber <= currentFloor)
            {
                return;
            }
            else
            {
                destinationFloors.Add(newFloorNumber);
                liftState = LiftState.up;
            }
        }
        else if (liftState == LiftState.down)
        {
            if (newFloorNumber >= currentFloor)
            {
                return;
            }
            else
            {
                destinationFloors.Add(newFloorNumber);
                liftState = LiftState.down;
            }
        }
    }

    

}
