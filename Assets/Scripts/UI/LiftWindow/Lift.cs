using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    ButtonBuilder builder = new ButtonBuilder();
    List<LiftButton> buttons;

    [SerializeField] Transform buttonParent;

    public void MakeButtons(int count)
    {
        for (int a = 0; a < count; a++)
        {
            var button = builder.BuildButton();
            button.transform.SetParent(buttonParent);

            var floorComponent = button.GetComponent<LiftButton>();
            floorComponent.floorNumber = a;

            buttons.Add(button.GetComponent<LiftButton>());
        }
    }


}
