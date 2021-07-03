using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryCharacter : NPC
{

    public override bool Action()
    {
        if (!base.Action()) return false;

        Debug.Log("I'm Hungry !");

        return true;
    }
}
