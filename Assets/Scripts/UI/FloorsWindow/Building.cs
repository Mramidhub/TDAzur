using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Building : UIBase
{
    [SerializeField] FloorBuilder builder;
    List<Floor> floors = new List<Floor>();

    [SerializeField] Transform floorParent;

    public void Init(int count)
    {
        for (int a = count-1; a >= 0; a--)
        {
            var floor = builder.BuildProduct(floorParent);

            var floorComponent = floor.GetComponent<Floor>();
            floorComponent.floorNumber = a;
            floorComponent.Init(a);

            floors.Add(floorComponent);
        }

        var currentPos = floorParent.transform;
        floorParent.localPosition = new Vector3(currentPos.localPosition.x, 100000, currentPos.localPosition.z);
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

    public void AllFloorsDeactiveCalledStatus()
    {
        if (floors.Count > 0)
        {
            foreach (var f in floors)
            {
                f.DisableButtons();
            }
        }
    }
}
