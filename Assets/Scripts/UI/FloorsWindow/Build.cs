using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : UIBase
{
    FloorBuilder builder = new FloorBuilder();
    List<Floor> floors;

    [SerializeField] Transform floorParent;

    public void MakeFloors(int count)
    {
        for (int a = 0; a < count; a++)
        {
            var floor = builder.BuildFloor();
            floor.transform.SetParent(floorParent);

            var floorComponent = floor.GetComponent<Floor>();
            floorComponent.floorNumber = a;

            floors.Add(floor.GetComponent<Floor>());
        }
    }

    public int GetFloorCount()
    {
        return floors.Count;
    }
}
