using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : UIBase
{
    AppManager appManager;

    [SerializeField] InputField floorsCount;

    private void Start()
    {
        appManager = AppManager.instance;
    }

    public void StartGame()
    {
        if (!appManager) return;

        var floors = int.Parse(floorsCount.text);
        appManager.InitApp(floors);

        Hide();
    }
}
