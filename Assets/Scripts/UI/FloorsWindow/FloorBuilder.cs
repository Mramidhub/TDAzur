using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public GameObject BuildFloor()
    {
        var newFloor = Instantiate(prefab);
        return newFloor;
    }
}
