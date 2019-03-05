using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Build : UIBase
{
    FloorBuilder builder;
    List<Floor> floors = new List<Floor>();

    [SerializeField] Transform floorParent;

    private void Start()
    {
        builder = GameObject.FindObjectOfType<FloorBuilder>();
    }

    public void MakeFloors(int count)
    {
        for (int a = 0; a < count; a++)
        {
            var floor = builder.BuildFloor();
            floor.transform.SetParent(floorParent);

            var floorComponent = floor.GetComponent<Floor>();
            floorComponent.floorNumber = a;
            floorComponent.Init(a);

            floors.Add(floorComponent);
        }
    }

    public int GetFloorCount()
    {
        return floors.Count;
    }

    public Floor GetFloor(int index)
    {
        Floor returnedFloor = null;

        returnedFloor = floors.First(z => z.floorNumber == index);

        return returnedFloor;
    }
}
