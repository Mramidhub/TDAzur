using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : UIBase
{
    [SerializeField] Slider slider;
    [SerializeField] Text floorNumberText;

    public void Init(int maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = 1;

        var gameLogic = AppManager.instance.gameLogic;
        gameLogic.changeFloorEvent.AddListener(SetFloor);
    }

    public void SetFloor(int floorNumber)
    {
        slider.value = floorNumber+1;
        floorNumberText.text = (floorNumber + 1).ToString();
    }


}
