using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : NPC
{
    /// <summary>
    /// 배고픈 NPC 와의 상호작용
    /// </summary>
    public override bool Action()
    {
        if (!base.Action()) return false;



        return true;
    }

    #region Monobehaivour Function

    private void OnEnable()
    {
        if (_uiParent == null) return;

        var buttons = _uiParent?.GetComponentsInChildren<ButtonPro>();
        if (buttons == null) return;

    } 
    #endregion
}
