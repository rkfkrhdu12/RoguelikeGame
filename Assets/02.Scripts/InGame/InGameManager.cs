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
    public Dictionary<Food, int> FoodList { get { return _inventory.FoodList; }}
    public ref Dictionary<Food, int> GetFoodList() { return ref _inventory.GetFoodList(); }
    public List<Inventory.ItemFoodMaterial> FoodMaterialList { get { return _inventory.FoodmaterialList; } }
    public ref  List<Inventory.ItemFoodMaterial> GetFoodMaterialList() { return ref _inventory.GetFoodMaterialList(); }

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
        if (foodCollection == null) return;

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
