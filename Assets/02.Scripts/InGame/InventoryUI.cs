using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void OnUpdate()
    {
        if (_itemObjectList == null)
            _itemObjectList = new List<GameObject>();


    }

    public GameObject GetLastListObject() { return _itemObjectList[_itemObjectList.Count - 1]; }

    private void OnEnable()
    {
        if (_itemObjectList == null)
            _itemObjectList = new List<GameObject>();

    }

    private void OnDisable()
    {

    }


    InventoryRefData _inventory;
    struct InventoryRefData
    {
        public Dictionary<Food, int> FoodList;
        public Dictionary<FoodMaterial, int> FoodmaterialList;
    }
    Dictionary<Food, int> _foodList { get { return _inventory.FoodList; } }
    Dictionary<FoodMaterial, int> _foodmaterialList { get { return _inventory.FoodmaterialList; } }
    List<GameObject> _itemObjectList = null;

    public void Init(ref Dictionary<Food, int> FoodList, ref Dictionary<FoodMaterial, int> FoodmaterialList)
    {
        _inventory.FoodList = FoodList;
        _inventory.FoodmaterialList = FoodmaterialList;

        if (_itemObjectList == null)
            _itemObjectList = new List<GameObject>();

        ///////////////////////////////////////////////////////////////////////////////////
        if (GameManager.Instance.FoodCollection == null) return;
        _foodList.Remove(GameManager.Instance.FoodCollection.CollectFoodsCode[0].food);
        _foodmaterialList.Remove(GameManager.Instance.FoodCollection.FoodMaterialsCode[0]);
    }
}
