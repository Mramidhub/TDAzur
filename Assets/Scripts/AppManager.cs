using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    public static AppManager instance;

    public UIManager uIManager;
    public GameLogic gameLogic;

    public enum GameState { Lobby, Game}
    GameState gameState = GameState.Lobby;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void InitApp(int floors)
    {
        gameLogic.InitGame(floors);
    }

}
