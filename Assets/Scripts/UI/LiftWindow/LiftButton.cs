using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftButton : MonoBehaviour
{
    [SerializeField] int floorNumber = 0;
    Lift lift;

    private void Start()
    {
        lift = FindObjectOfType<Lift>();
    }

    public void PressOn()
    {
        if (lift)
        {
            Debug.Log("press floor " + floorNumber);
            lift.AddDestinationFloor(floorNumber);
        }
    }
}
