using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void OnClick(Inventory.ItemFoodMaterial foodMaterial)
    {
        Debug.Log(foodMaterial.foodMaterial.name + "  " + foodMaterial.count);
    }

    public void Init(ref Dictionary<Food, int> FoodList, ref List<Inventory.ItemFoodMaterial> FoodmaterialList)
    {
        _inventory.FoodList = FoodList;
        _inventory.FoodMaterialList = FoodmaterialList;

        ///////////////////////////////////////////////////////////////////////////////////
        FoodCollection foodCollection = GameManager.Instance.FoodCollection;
        if (foodCollection == null) return;
        _foodList.Remove(foodCollection.CollectFoodsCode[0].food);

        foreach (var it in _foodMaterialList)
        {
            if (foodCollection.FoodMaterialsCode[0] == it.foodMaterial)
            {
                _foodMaterialList.Remove(it);
                break;
            }
        }

        UpdateFoodMaterial();
    }


    InventoryRefData _inventory;
    struct InventoryRefData
    {
        public Dictionary<Food, int> FoodList;
        public List<Inventory.ItemFoodMaterial> FoodMaterialList;
    }

    Dictionary<Food, int> _foodList { get { return _inventory.FoodList; } }
    List<Inventory.ItemFoodMaterial> _foodMaterialList { get { return _inventory.FoodMaterialList; } }

    [SerializeField] FoodMaterialUI _foodMaterialUI = null;

    private void OnEnable()
    {
        UpdateFoodMaterial();
    }

    void UpdateFoodMaterial()
    {
        if (_foodMaterialUI == null ||
            _foodMaterialList == null) return;

        if (_foodMaterialList.Count != _foodMaterialUI.childCount)
        {
            _foodMaterialUI.OnUpdate(_foodMaterialList, OnClick);
        }
    }
}
