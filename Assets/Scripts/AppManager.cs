using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    public UIManager uIManager;
    public GameLogic gameLogic;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void InitApp(int floors)
    {
        gameLogic.InitGame(floors);
    }

}
