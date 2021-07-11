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

    #region Variable

    FoodCollection _foodCollection;

    #endregion
    
    #region Monobehaivour Function

    private void OnEnable()
    {
        if (_uiParent == null) return;

        if (_foodCollection == null)
            _foodCollection = GameManager.Instance.FoodCollection;
        if (_foodCollection == null) return;

        Dictionary<int, CollectFood> collectFoods = _foodCollection.CollectFoodsCode;

        for (int i = 0; i < collectFoods.Count; ++i)
        {
            AddItemSlot(collectFoods[i].name);
        }
    } 
    #endregion
}
