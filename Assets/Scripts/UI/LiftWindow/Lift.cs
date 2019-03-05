using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    ButtonBuilder builder;
    List<LiftButton> buttons = new List<LiftButton>();
    GameLogic gameLogic;

    [SerializeField] Transform buttonParent;

    private void Start()
    {
        builder = FindObjectOfType<ButtonBuilder>();
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void MakeButtons(int count)
    {
        for (int a = 0; a < count; a++)
        {
            var button = builder.BuildButton();
            button.transform.SetParent(buttonParent);

            var buttonComponent = button.GetComponent<LiftButton>();
            buttonComponent.Init(a);

            buttons.Add(button.GetComponent<LiftButton>());
        }
    }

    public void Stop()
    {
        gameLogic.Stop();
        DeactiveAllButtons();
    }

    public void DeactiveAllButtons()
    {
        foreach (var b in buttons)
        {
            b.DeactiveButton(b.floorNumber);
        }
    }


}
