using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryCharacter : NPC
{
    /// <summary>
    /// 배고픈 NPC 와의 상호작용
    /// </summary>
    public override bool Action()
    {
        if (!base.Action()) return false;
        
        return true;
    }

    private void OnEnable()
    {
        if (_uiParent == null) return;

        var buttons = _uiParent?.GetComponentsInChildren<ButtonPro>();
        if (buttons == null) return;

    }
}
