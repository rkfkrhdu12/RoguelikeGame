using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryCharacter : NPC
{
    /// <summary>
    /// ����� NPC ���� ��ȣ�ۿ�
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
