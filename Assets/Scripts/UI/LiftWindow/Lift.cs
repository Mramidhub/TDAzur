using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
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
            Debug.Log("stopped ");
            return;
        }

        if (liftState == LiftState.openned)
        {
            if (openDoorTime > 0)
            {
                openDoorTime -= Time.deltaTime;
                return;
            }

            Debug.Log("door closed ");

            openDoorTime = tempOpenDoorTime;
            liftState = previosState;
            Debug.Log("status " + liftState);

            if (currentFloor == destinationFloors[destinationFloors.Count - 1])
            {
                liftState = LiftState.stopped;
            }
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
                Debug.Log("next floor up " + currentFloor + " " + "door open");
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
                Debug.Log("next floor down " + currentFloor + " " + "door open");

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
        else if (liftState == LiftState.stopped)
        {
            destinationFloors.Add(newFloorNumber);

            if (newFloorNumber > currentFloor)
            {
                liftState = LiftState.up;
            }
            else if (newFloorNumber < currentFloor)
            {
                liftState = LiftState.down;
            }
        }
    }

    

}
