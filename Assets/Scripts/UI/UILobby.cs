using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    AppManager appManager;

    InputField floorsCount;

    private void Awake()
    {
        appManager = AppManager.instance;
    }

    public void StartGame()
    {
        if (!appManager) return;

        var floors = int.Parse(floorsCount.text);
        appManager.InitApp(floors);
    }
}
