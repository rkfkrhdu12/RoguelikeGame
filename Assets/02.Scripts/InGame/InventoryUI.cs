using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void OnClick(Inventory.ItemFoodMaterial foodMaterial)
    {
        LogManager.Log(foodMaterial.foodMaterial.name + "  " + foodMaterial.count);
    }

    public void OnPauseMode()
    {
        if (_itemSlotManager == null ||
            _foodMaterialList == null) return;

        if (_foodMaterialList.Count != _itemSlotManager.childCount)
        {
            _itemSlotManager.OnUpdate(_foodMaterialList, OnClick);
        }
    }

    public void Init(ref Dictionary<Food, int> FoodList, ref List<Inventory.ItemFoodMaterial> FoodmaterialList)
    {
        _inventory.FoodList = FoodList;
        _inventory.FoodMaterialList = FoodmaterialList;
    }

    InventoryRefData _inventory;
    struct InventoryRefData
    {
        public Dictionary<Food, int> FoodList;
        public List<Inventory.ItemFoodMaterial> FoodMaterialList;
    }

    Dictionary<Food, int> _foodList { get { return _inventory.FoodList; } }
    List<Inventory.ItemFoodMaterial> _foodMaterialList { get { return _inventory.FoodMaterialList; } }

    [SerializeField] ItemSlotManager _itemSlotManager = null;
}
