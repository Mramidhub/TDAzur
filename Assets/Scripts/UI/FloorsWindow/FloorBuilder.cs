using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public GameObject BuildFloor()
    {
        GameObject newFloor = Instantiate(prefab) as GameObject;
        return newFloor;
    }
}
