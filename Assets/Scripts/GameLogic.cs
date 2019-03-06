using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameLogic : MonoBehaviour
{
    bool gameProcessOn = false;
    [SerializeField] Building building;
    [SerializeField] Lift lift;

    [SerializeField] float liftSpeed = 2f;
     float liftSpeedTemp = 2f;
    [SerializeField] float openDoorTime = 2f;
     float tempOpenDoorTime = 2f;

    int currentFloor = 0;

    List<int> destinationFloors = new List<int>();
    List<LiftCall> liftCalls = new List<LiftCall>();

    public enum LiftState { up, down, stopped, openned }
    LiftState liftState = LiftState.stopped;
    LiftState previosState = LiftState.stopped;

 
    public UnityEvent startGame = new UnityEvent();
    public EventOpenDoor openDoorEvent = new EventOpenDoor();
    public EventChangeFloor changeFloorEvent = new EventChangeFloor();


    private void FixedUpdate()
    {
        if (gameProcessOn)
        {
            LiftMove();
        }
    }

    public void InitGame(int floorCount)
    {
        building.Init(floorCount);
        lift.Init(floorCount);

        startGame.Invoke();
        gameProcessOn = true;

        currentFloor = 0;
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

            SetDoorStatus(currentFloor, false);

            openDoorTime = tempOpenDoorTime;
            liftState = previosState;

            if (destinationFloors.Count == 0)
            {
                if (liftCalls.Count > 0)
                {
                    var firstCall = liftCalls[0];
                    var newDestinationFloor = firstCall.floorNumber;

                    if (newDestinationFloor > currentFloor)
                    {
                        liftState = LiftState.up;
                    }
                    else if (newDestinationFloor < currentFloor)
                    {
                        liftState = LiftState.down;
                    }

                    destinationFloors.Add(newDestinationFloor);
                    return;
                }

                Stop();
            }
        }

        if (liftSpeed > 0)
        {
            liftSpeed -= Time.deltaTime;
            return;
        }

        if (liftState == LiftState.up)
        {
            currentFloor += 1;
        }
        else if (liftState == LiftState.down)
        {
            currentFloor -= 1;
        }

        changeFloorEvent.Invoke(currentFloor);

        liftSpeed = liftSpeedTemp;

        LiftCall floorOnCall = null;

        if (liftCalls.Count > 0)
        {
            liftCalls.ForEach(x =>
            {
                if (x.floorNumber == currentFloor)
                {
                    floorOnCall = x;
                }
            }); 
        }

        if (floorOnCall != null)
        {
            var floor = building.GetFloor(currentFloor);

            if (destinationFloors.Count > 1 || destinationFloors.Count == 1 && destinationFloors[0] != floorOnCall.floorNumber)
            {
                if (liftState == LiftState.up && floorOnCall.calledStatus != Floor.CalledStatus.toUp
                    || liftState == LiftState.down && floorOnCall.calledStatus != Floor.CalledStatus.toDown)
                {
                    return;
                }
            }
  
            previosState = liftState;
            liftState = LiftState.openned;

            SetDoorStatus(currentFloor, true);
            liftCalls.Remove(floorOnCall);

            openDoorEvent.Invoke(currentFloor);
        }

        if (destinationFloors.Contains(currentFloor))
        {
            var floor = building.GetFloor(currentFloor);

            previosState = liftState;
            liftState = LiftState.openned;

            SetDoorStatus(currentFloor, true);
            liftCalls.Remove(floorOnCall);

            destinationFloors.Remove(currentFloor);

            openDoorEvent.Invoke(currentFloor);
        }
 
    }

    void SetDoorStatus(int index, bool open)
    {
        var floor = building.GetFloor(index);

        if (floor)
        {
            floor.SetDoorStatus(open);
        }
    }


    public void Stop()
    {
        destinationFloors.Clear();
        liftCalls.Clear();
        liftSpeed = liftSpeedTemp;
        openDoorTime = tempOpenDoorTime;
        liftState = LiftState.stopped;

        SetDoorStatus(currentFloor, false);
        AllFloorsDeactiveCalledStatus();
    }

    public void AddLiftCall(int floorNumber, bool up)
    {
        var liftCall = new LiftCall();

        if (up)
        {
            liftCall.calledStatus = Floor.CalledStatus.toUp;
        }
        else
        {
            liftCall.calledStatus = Floor.CalledStatus.toDown;
        }

        liftCall.floorNumber = floorNumber;
        liftCalls.Add(liftCall);

        if (liftState == LiftState.stopped)
        {
            AddDestinationFloor(floorNumber);
        }
    }

    public bool AddDestinationFloor(int newFloorNumber)
    {
        switch (liftState)
        {
            case LiftState.up:
                if (newFloorNumber <= currentFloor)
                {
                    return false;
                }
                break;
            case LiftState.down:
                if (newFloorNumber >= currentFloor)
                {
                    return false;
                }
                break;
            case LiftState.stopped:
                if (newFloorNumber > currentFloor)
                {
                    destinationFloors.Clear();
                    destinationFloors.Add(newFloorNumber);
                    liftState = LiftState.up;
                }
                else if(newFloorNumber < currentFloor)
                {
                    destinationFloors.Clear();
                    destinationFloors.Add(newFloorNumber);
                    liftState = LiftState.down;
                }
                return true;
        }

        destinationFloors.Add(newFloorNumber);

        return true;
    }

    void AllFloorsDeactiveCalledStatus()
    {
        building.AllFloorsDeactiveCalledStatus();
    }

}

public class LiftCall
{
    public int floorNumber = 0;
    public Floor.CalledStatus calledStatus = Floor.CalledStatus.none;
}
