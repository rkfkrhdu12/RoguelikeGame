using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public bool Buy(int price)
    {
        if (_curGold >= price)
        {
            _curGold -= price;
            return true;
        }
        else
            return false;
    }

    public bool Sell(int price)
    {
        _curGold += price;

        return true;
    }

    #region Variable

    [SerializeField, Header("ÇöÀç °ñµå·®")]
    int _curGold = 0; 
    #endregion
}
