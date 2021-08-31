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

    FoodCollector _foodCollection;

    #endregion
    
    #region Monobehaivour Function

    private void OnEnable()
    {
        if (_myUI == null) return;

        if (_foodCollection == null)
            _foodCollection = GameManager.Instance.FoodCollector;
        if (_foodCollection == null) return;

        List<Food> collectFoods = _foodCollection.CollectFoodList;

        for (int i = 0; i < collectFoods.Count; ++i)
        {
            AddItemSlot(collectFoods[i].name);
        }
    } 
    #endregion
}
