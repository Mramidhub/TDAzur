using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    bool gameProcessOn = true;
    [SerializeField] Build build;
    [SerializeField] Lift lift;

    float liftSpeed = 2f;
    float liftSpeedTemp = 2f;
    float openDoorTime = 2f;
    float tempOpenDoorTime = 2f;
    int currentFloor = 0;

    List<int> destinationFloors = new List<int>();
    List<int> destinationFloorsUp = new List<int>();
    List<int> destinationFloorsDown = new List<int>();

    public enum LiftState { up, down, stopped, openned }
    LiftState liftState = LiftState.stopped;
    LiftState previosState = LiftState.stopped;

 
    UnityEvent startGame = new UnityEvent();

    private void Start()
    {
        lift = FindObjectOfType<Lift>();
    }

    private void FixedUpdate()
    {
        if (gameProcessOn)
        {
            LiftMove();
        }
    }

    public void InitGame(int floorCount)
    {
        build.MakeFloors(floorCount);
        lift.MakeButtons(floorCount);


        startGame.Invoke();
        gameProcessOn = true;
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

            SetDoorStatus(currentFloor, false);

            openDoorTime = tempOpenDoorTime;
            liftState = previosState;
            Debug.Log("status " + liftState);

            if (currentFloor == destinationFloors[destinationFloors.Count - 1])
            {
                if (liftState == LiftState.up && destinationFloorsDown.Count > 0)
                {
                    destinationFloors = destinationFloorsDown;
                    destinationFloorsUp.Clear();

                    liftState = LiftState.down;
                }
                else if (liftState == LiftState.down && destinationFloorsUp.Count > 0)
                {
                    destinationFloors = destinationFloorsUp;
                    destinationFloorsDown.Clear();

                    liftState = LiftState.up;
                }
                else
                {
                    liftState = LiftState.stopped;
                    SetDoorStatus(currentFloor, false);

                    destinationFloors.Clear();
                    destinationFloorsDown.Clear();
                    destinationFloorsUp.Clear();
                }
                return;
            }
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

            Debug.Log("on floor " + currentFloor);

            if (destinationFloors.Contains(currentFloor))
            {
                var floor = build.GetFloor(currentFloor);
                if (floor.status == Floor.CalledStatus.toDown) return;

                previosState = liftState;
                liftState = LiftState.openned;

                SetDoorStatus(currentFloor, true);

                Debug.Log("floor up " + currentFloor + " " + "door open");
            }
        }
        else if (liftState == LiftState.down)
        {
            if (liftSpeed > 0)
            {
                liftSpeed -= Time.deltaTime;
                return;
            }
            Debug.Log("floor" + currentFloor);

            currentFloor -= 1;
            liftSpeed = liftSpeedTemp;

            if (destinationFloors.Contains(currentFloor))
            {
                var floor = build.GetFloor(currentFloor);
                if (floor.status == Floor.CalledStatus.toUp) return;

                Debug.Log("next floor down " + currentFloor + " " + "door open");

                previosState = liftState;
                liftState = LiftState.openned;

                SetDoorStatus(currentFloor, true);
            }
        }
    }

    void SetDoorStatus(int index, bool open)
    {
        var floor = build.GetFloor(index);

        if (floor)
        {
            floor.SetDoorStatus(open);
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
        //if (directionCall == LiftDirectionCall.toDown)
        //{
        //    destinationFloorsDown.Add(newFloorNumber);

        //}
        //else if (directionCall == LiftDirectionCall.toUp)
        //{
        //    destinationFloorsUp.Add(newFloorNumber);

        //}

        //if (liftState == LiftState.stopped)
        //{
        //    if (newFloorNumber < currentFloor)
        //    {
        //        liftState = LiftState.down;
        //        destinationFloorsUp.Clear();
        //        destinationFloorsDown.Add(newFloorNumber);
        //        destinationFloors = destinationFloorsDown;
        //    }
        //    else if (newFloorNumber > currentFloor)
        //    {
        //        liftState = LiftState.up;
        //        destinationFloorsDown.Clear();
        //        destinationFloorsUp.Add(newFloorNumber);
        //        destinationFloors = destinationFloorsUp;
        //    }
        //    return;
        //}


        switch (liftState)
        {
            case LiftState.up:
                if (newFloorNumber <= currentFloor)
                {
                    destinationFloorsDown.Add(newFloorNumber);
                    return;
                }
                else
                {
                    destinationFloorsUp.Add(newFloorNumber);
                }
                break;
            case LiftState.down:
                if (newFloorNumber >= currentFloor)
                {
                    destinationFloorsUp.Add(newFloorNumber);
                    return;
                }
                else
                {
                    destinationFloorsDown.Add(newFloorNumber);
                }
                break;
            case LiftState.stopped:
                if (newFloorNumber > currentFloor)
                {
                    liftState = LiftState.up;
                    destinationFloorsUp.Add(newFloorNumber);
                    destinationFloors = destinationFloorsUp;
                }
                else if (newFloorNumber < currentFloor)
                {
                    liftState = LiftState.down;
                    destinationFloorsDown.Add(newFloorNumber);
                    destinationFloors = destinationFloorsDown;
                }
                break;

        }
        return;
    }

}
