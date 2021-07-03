using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : NPC
{
    /// <summary>
    /// 조리사(셰프) NPC 와의 상호작용
    /// </summary>
    public override bool Action()
    {
        if (!base.Action()) return false;

        return true;
    }
}
