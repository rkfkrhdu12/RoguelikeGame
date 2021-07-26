using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    /// <summary>
    /// �丮�� �������� ��
    /// </summary>
    public void Collect(string foodName)
    {
        if (!_foodManager.Foods.ContainsKey(foodName)) return;

        if (!_collectFoods.ContainsKey(foodName))
        {
            Food collectFood = _foodManager.Foods[foodName];

            _collectFoods.Add(foodName, collectFood);
            _collectFoodsCode.Add(collectFood.index, collectFood);

            if (_remainFoods.ContainsKey(foodName))
                _remainFoods.Remove(foodName);
            if (_remainFoodsCode.ContainsKey(collectFood.index))
                _remainFoodsCode.Remove(collectFood.index);
        }
    }

    #region Public Variable
    public Dictionary<string, Food> CollectFoods { get { return _collectFoods; } }
    public Dictionary<int, Food> CollectFoodsCode { get { return _collectFoodsCode; } }
    public Dictionary<string, Food> RemainFoods { get { return _remainFoods; } }
    public Dictionary<int, Food> RemainFoodsCode { get { return _remainFoodsCode; } }
    #endregion

    /// �Ϲ� ������
    #region Private Variable
    /// <summary>
    /// ������ �÷���
    /// </summary>
    Dictionary<string, Food> _collectFoods = new Dictionary<string, Food>();   
    Dictionary<int, Food> _collectFoodsCode = new Dictionary<int, Food>();      

    /// <summary>
    /// ���� �÷���
    /// </summary>
    Dictionary<string, Food> _remainFoods = new Dictionary<string, Food>();     
    Dictionary<int, Food> _remainFoodsCode = new Dictionary<int, Food>();

    FoodManager _foodManager = null;

    #endregion

    /// MonoBehaviour ���� �Լ�
    #region MonoBehaviour Functions
    void Awake()
    {
        GameManager.Instance.FoodCollector = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_foodManager == null) _foodManager = GameManager.Instance.FoodManager;
    }

    #endregion
}

[System.Serializable]
public class Food : FoodMaterial
{
    public List<FoodMaterial> materials;

    public void AddFoodMaterial(FoodMaterial foodMaterial)
    {
        materials.Add(foodMaterial);
    }
}

[System.Serializable]
public class FoodMaterial
{
    public string name;
    public int index;
    public int price;
    public int foodPrice;
}
