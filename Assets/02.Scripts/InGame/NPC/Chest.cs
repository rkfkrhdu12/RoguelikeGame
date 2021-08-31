using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : NPC
{
    /// <summary>
    /// 
    /// </summary>
    public override bool Action()
    {
        if (!base.Action()) return false;

        return true;
    }

    #region Monobehaivour Function

    private void OnEnable()
    {
        if (_myUI == null) return;

        var buttons = _myUI?.GetComponentsInChildren<ButtonPro>();
        if (buttons == null) return;

    }
    #endregion
}
