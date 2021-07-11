using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /// <summary>
    /// 음식재료를 추가한다
    /// </summary>
    public void AddItem(FoodMaterial foodMaterial, int count)
    {
        int index = GetIndex(foodMaterial);
        if (index == -1)
        {
            ItemFoodMaterial newItem = new ItemFoodMaterial();
            newItem.foodMaterial = foodMaterial;
            newItem.count = count;
        }
        else
        {
            _foodMaterialList[index].count += count;
        }
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
        int index = GetIndex(foodMaterial);
        if (index == -1) return false;

        ItemFoodMaterial selectItem = _foodMaterialList[index];

        selectItem.count -= count;
        if (selectItem.count <= 0)
            _foodMaterialList.RemoveAt(index);

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

    /// <summary>
    /// InitData 음식 리스트
    /// </summary>
    public void Awake()
    {
        if (_ingameManager == null)
            _ingameManager = GameManager.Instance.InGameManager;

        if (_foodList == null)
            _foodList = new Dictionary<Food, int>();

        if (_foodMaterialList == null)
            _foodMaterialList = new List<ItemFoodMaterial>();

        //////////////
        AddGold(17);
    }

    #region Variable

    InGameManager _ingameManager = null;

    List<ItemFoodMaterial> _foodMaterialList = null;
    public List<ItemFoodMaterial> FoodmaterialList { get { return _foodMaterialList; } }
    public ref  List<ItemFoodMaterial> GetFoodMaterialList() { return ref _foodMaterialList; }
    public class ItemFoodMaterial
    {
        public FoodMaterial foodMaterial;
        public int count = 0;
    }

    Dictionary<Food, int> _foodList = null;
    public Dictionary<Food, int> FoodList { get { return _foodList; } }
    public ref Dictionary<Food, int> GetFoodList() { return ref _foodList; }

    int _curGold = 0; 
    public int CurGold { get { return _curGold; } }
    #endregion

    int GetIndex(FoodMaterial foodMaterial)
    {
        for (int i = 0; i < _foodMaterialList.Count; ++i)
        {
            if (_foodMaterialList[i].foodMaterial == foodMaterial)
                return i;
        }

        return -1;
    }
}
