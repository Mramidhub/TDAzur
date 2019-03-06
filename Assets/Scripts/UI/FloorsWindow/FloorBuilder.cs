using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : BuilderBase
{
    [SerializeField] GameObject prefab;

    public override GameObject BuildProduct(Transform parent)
    {
        GameObject newFloor = Instantiate(prefab, parent) as GameObject;
        return newFloor;
    }
}
