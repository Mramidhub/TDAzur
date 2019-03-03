using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);

        OnShow();
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        OnHide();
    }

    public virtual void OnShow() { }
    public virtual void OnHide() { }
}
