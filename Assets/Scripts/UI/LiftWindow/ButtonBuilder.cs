using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuilder : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public GameObject BuildButton()
    {
        var newButton = Instantiate(prefab);
        return newButton;
    }
}
