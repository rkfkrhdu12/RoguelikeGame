using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : NPC
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
        var buttons = _uiParent.GetComponentsInChildren<ButtonPro>();

        
    }
}
