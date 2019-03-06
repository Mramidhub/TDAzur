using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuilder : BuilderBase
{
    [SerializeField] GameObject prefab;

    public override GameObject BuildProduct(Transform parent)
    {
        GameObject newFloor = Instantiate(prefab, parent) as GameObject;
        return newFloor;
    }
}
