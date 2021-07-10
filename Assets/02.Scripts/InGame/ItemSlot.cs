using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("OnClick");

        if (curAction == null) return;

        curAction();
    }

    #region Variable

    public delegate void UIAction();
    public UIAction curAction;

    #endregion
}
