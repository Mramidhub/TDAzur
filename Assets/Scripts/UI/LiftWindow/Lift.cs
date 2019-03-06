using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lift : UIBase
{
    [SerializeField] ButtonBuilder builder;
    List<LiftButton> buttons = new List<LiftButton>();
    [SerializeField] StatusBar statusBar;
    [SerializeField] Scrollbar scrollBar;
    GameLogic gameLogic;

    [SerializeField] Transform buttonParent;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void Init(int count)
    {
        for (int a = 0; a < count; a++)
        {
            var button = builder.BuildProduct(buttonParent);

            var buttonComponent = button.GetComponent<LiftButton>();
            buttonComponent.Init(a);

            buttons.Add(button.GetComponent<LiftButton>());
        }

        statusBar.Init(count);

        var currentPos = buttonParent.transform;
        buttonParent.localPosition = new Vector3(currentPos.localPosition.x, -100000, currentPos.localPosition.z);
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
