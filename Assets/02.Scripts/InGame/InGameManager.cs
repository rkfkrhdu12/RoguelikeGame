using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public bool AddGold(int price)
    {
        if (_inventory == null) return false;

        if (CurGold >= price)
        {
            _inventory.AddGold(-price);
            return true;
        }
        else
            return false;
    }

    public bool SubtractionGold(int price)
    {
        if (_inventory == null) return false;

        _inventory.AddGold(price);

        return true;
    }

    public bool Buy(Food item, int count = 1)
    {
        if (_inventory == null || item == null) return false;

        if (AddGold(item.price * count)) _inventory.AddItem(item, count);

        return true;
    }

    public bool Buy(FoodMaterial item, int count = 1)
    {
        if (_inventory == null || item == null) return false;

        if (AddGold(item.price * count)) _inventory.AddItem(item, count);

        return true;
    }

    public bool Sell(Food item, int count = 1)
    {
        if (_inventory == null || item == null) return false;

        if (SubtractionGold(item.price * count)) _inventory.AddItem(item, count);

        return true;
    }

    public bool Sell(FoodMaterial item, int count = 1)
    {
        if (_inventory == null || item == null) return false;

        if (SubtractionGold(item.price * count)) _inventory.AddItem(item, count);

        return true;
    }


    #region Variable

    /// <summary>
    /// 입력 관련 매니저
    /// </summary>
    public InGameInput InGameInput { get { return _inGameInput; } set { if (_inGameInput == null) { _inGameInput = value; } } }
    private InGameInput _inGameInput = null;

    public int CurGold { get { return _inventory.CurGold; } }
    public Dictionary<Food, int> FoodList { get { return Inventory.FoodList; }}
    public Dictionary<FoodMaterial, int> FoodMaterialList { get { return Inventory.FoodmaterialList; } }

    /// <summary>
    /// Inventory의 음식과 음식재료를 참조형태로 데이터를 연동시킴(포인터)
    /// </summary>
    public InventoryData Inventory;
    public struct InventoryData
    {
        public Dictionary<Food, int> FoodList;
        public Dictionary<FoodMaterial, int> FoodmaterialList;

        public void Init(ref Dictionary<Food, int> foodList, ref Dictionary<FoodMaterial, int> foodmaterialList)
        {
            if (FoodList == null)           FoodList = foodList;
            if (FoodmaterialList == null)   FoodmaterialList = foodmaterialList;
        }
    }

    Inventory _inventory = null;
    #endregion

    #region MonoBehaviour Function

    private void Awake()
    {
        GameManager.Instance.InGameManager = this;

        if (_inventory == null)
        {
            _inventory = new Inventory();
            _inventory.Awake();
        }
    }

    /// /////////////////////////////////////////////////////////////////////
    private void Start()
    {
        FoodCollection foodCollection = GameManager.Instance.FoodCollection;

        Buy(foodCollection.FoodMaterialsCode[0]);
        Buy(foodCollection.FoodMaterialsCode[0]);
        Buy(foodCollection.FoodMaterialsCode[0]);

        Buy(foodCollection.FoodMaterialsCode[1]);
        Buy(foodCollection.FoodMaterialsCode[2]);
        Buy(foodCollection.FoodMaterialsCode[3]);

        foodCollection.Collect(foodCollection.AllFoodsCode[0].name);
        foodCollection.Collect(foodCollection.AllFoodsCode[1].name);

        Buy(foodCollection.CollectFoodsCode[0].food);
        Buy(foodCollection.CollectFoodsCode[1].food);
    }

    #endregion

}
