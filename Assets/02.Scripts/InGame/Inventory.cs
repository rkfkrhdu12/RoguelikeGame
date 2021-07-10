using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /// <summary>
    /// InitData 음식 리스트
    /// </summary>
    public void Awake()
    {
        if (_ingameManager == null)
            _ingameManager = GameManager.Instance.InGameManager;

        if (_foodList == null)
            _foodList = new Dictionary<Food, int>();

        if (_foodmaterialList == null)
            _foodmaterialList = new Dictionary<FoodMaterial, int>();

        //////////////
        AddGold(17);
    }

    /// <summary>
    /// 음식재료를 추가한다
    /// </summary>
    public void AddItem(FoodMaterial foodMaterial, int count)
    {
        if (!_foodmaterialList.ContainsKey(foodMaterial))
            _foodmaterialList.Add(foodMaterial, 0);

        _foodmaterialList[foodMaterial] += count;
    }

    /// <summary>
    /// 음식을 추가한다
    /// </summary>
    public void AddItem(Food food, int count)
    {
        if (!_foodList.ContainsKey(food))
            _foodList.Add(food, 0);

        _foodList[food] += count;
    }

    /// <summary>
    /// 음식재료를 뺀다.
    /// </summary>
    public bool RemoveItem(FoodMaterial foodMaterial, int count)
    {
        if (_foodmaterialList.ContainsKey(foodMaterial))
        {
            if (_foodmaterialList[foodMaterial] < count) return false;

            _foodmaterialList[foodMaterial] -= count;
        }

        return true;
    }

    /// <summary>
    /// 음식을 뺀다
    /// </summary>
    public bool RemoveItem(Food food, int count)
    {
        if (_foodList.ContainsKey(food))
        {
            if (_foodList[food] < count) return false;

            _foodList[food] -= count;
        }

        return true;
    }

    /// <summary>
    /// 골드를 추가한다.
    /// </summary>
    public void AddGold(int gold)
    {
        _curGold = Mathf.Clamp(_curGold + gold, 0, 100000);
    }

    #region Variable

    InGameManager _ingameManager = null;

    Dictionary<Food, int> _foodList = null;
    public Dictionary<Food, int> FoodList { get { return _foodList; } }
    public ref Dictionary<Food, int> GetFoodList() { return ref _foodList; }

    Dictionary<FoodMaterial, int> _foodmaterialList = null;
    public Dictionary<FoodMaterial, int> FoodmaterialList { get { return _foodmaterialList; } }
    public ref Dictionary<FoodMaterial, int> GetFoodMaterialList() { return ref _foodmaterialList; }

    int _curGold = 0; 
    public int CurGold { get { return _curGold; } }
    #endregion

}
