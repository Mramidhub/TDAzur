using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    ButtonBuilder builder;
    List<LiftButton> buttons = new List<LiftButton>();

    [SerializeField] Transform buttonParent;

    private void Start()
    {
        builder = FindObjectOfType<ButtonBuilder>();
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


}
