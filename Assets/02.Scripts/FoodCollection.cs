using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollection : MonoBehaviour
{
    /// <summary>
    /// �丮�� �������� ��
    /// </summary>
    public void Collect(string foodName)
    {
        if (!_collectFoods.ContainsKey(foodName))
        {
            AddCollectFoods(foodName, _allFoods[foodName]);

            UpdateSerializeData();
        }
    }

    /// Inspector�� �����ֱ����� �����͸� ����ȭ��Ų ������ *UnityEditor
    #region Serialize Variables

#if UNITY_EDITOR
    [SerializeField, Header("��� �����÷���")]
    List<CollectFood> _sAllFoods = new List<CollectFood>();

    [SerializeField, Header("������ �����÷���")]
    List<CollectFood> _sCollectFoods = new List<CollectFood>();
#endif
    #endregion

    /// �Ϲ� ������
    #region Variables

    /// <summary>
    /// ��ü �÷���
    /// </summary>
    Dictionary<string, CollectFood> _allFoods = new Dictionary<string, CollectFood>();
    Dictionary<int, CollectFood> _allFoodsCode = new Dictionary<int, CollectFood>();

    public Dictionary<string, CollectFood> AllFoods { get { return _allFoods; } }
    public Dictionary<int, CollectFood> AllFoodsCode { get { return _allFoodsCode; } }

    /// <summary>
    /// ������ �÷���
    /// </summary>
    Dictionary<string, CollectFood> _collectFoods = new Dictionary<string, CollectFood>();
    Dictionary<int, CollectFood> _collectFoodsCode = new Dictionary<int, CollectFood>();

    public Dictionary<string, CollectFood> CollectFoods { get { return _collectFoods; } }
    public Dictionary<int, CollectFood> CollectFoodsCode { get { return _collectFoodsCode; } }

    ////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ���� �÷���
    /// </summary>
    Dictionary<string, CollectFood> _remainFoods = new Dictionary<string, CollectFood>();
    Dictionary<int, CollectFood> _remainFoodsCode = new Dictionary<int, CollectFood>();

    public Dictionary<string, CollectFood> RemainFoods { get { return _remainFoods; } }
    public Dictionary<int, CollectFood> RemainFoodsCode { get { return _remainFoodsCode; } }
    //////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// ��� ���
    /// </summary>
    Dictionary<string, FoodMaterial> _materials = new Dictionary<string, FoodMaterial>();
    Dictionary<int, FoodMaterial> _materialsCode = new Dictionary<int, FoodMaterial>();

    public Dictionary<string, FoodMaterial> FoodMaterials { get { return _materials; } }
    public Dictionary<int, FoodMaterial> FoodMaterialsCode { get { return _materialsCode; } }

    #endregion

    /// MonoBehaviour ���� �Լ�
    #region MonoBehaviour Functions
    void Awake()
    {
        GameManager.Instance.FoodCollection = this;

        DontDestroyOnLoad(gameObject);

        InitMaterials();
        InitFoods();

        #region Serialize Variable

        InitSerializeVariable();

        #endregion
    }

    #endregion

    /// Private �Լ�
    #region Private Functions

    /// ��� Init
    private void InitMaterials()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Material>();

        foreach (var line in FoodCollectionSheet.Material.MaterialList)
        {
            if (_materials.ContainsKey(line.name)) continue;

            FoodMaterial newData = new FoodMaterial
            {
                name = line.name,
                index = line.index,
                sellPrice = line.sell,
            };

            AddMaterial(newData);
        }
    }

    /// ���� Init
    private void InitFoods()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Food>();

        foreach (var line in FoodCollectionSheet.Food.FoodList)
        {
            if (_allFoods.ContainsKey(line.name)) continue;

            CollectFood newData = new CollectFood
            {
                name = line.name,
                index = line.index,
                sellPrice = line.sell,

                materials = new List<FoodMaterial>(),
            };

            if (InitFoodsMaterials(line.material0Name, newData))
                if (InitFoodsMaterials(line.material1Name, newData))
                    if (InitFoodsMaterials(line.material2Name, newData))
                        if (InitFoodsMaterials(line.material3Name, newData)) ;

            AddAllFoods(line.name, newData);
        }
    }

    /// ���Ŀ� ��� Init
    bool InitFoodsMaterials(string matName, CollectFood newData)
    {
        if (!string.IsNullOrWhiteSpace(matName) && _materials.ContainsKey(matName))
        {
            FoodMaterial foodMaterial = _materials[matName];
            newData.materials.Add(foodMaterial);

            return true;
        }

        return false;
    }

    private void AddAllFoods(string key, CollectFood value)
    {
        _allFoods.Add(key, value);
        AllFoodsCode.Add(value.index, value);

        /////////////////////////////////////////
        _remainFoods.Add(key, value);
        _remainFoodsCode.Add(value.index, value);
    }

    private void AddCollectFoods(string key, CollectFood value)
    {
        _collectFoods.Add(key, value);
        _collectFoodsCode.Add(value.index, value);

        /////////////////////////////////////
        _remainFoods.Remove(key);
        _remainFoodsCode.Remove(value.index);
    }

    private void AddMaterial(FoodMaterial value)
    {
        _materials.Add(value.name, value);
        _materialsCode.Add(value.index, value);
    }

    /// ����ȭ�� �����鿡 ���� �Լ� *UnityEditor
    #region Serialize Functions

    /// <summary>
    /// Inspector �� �����ֱ����� ����ȭ�� �������� Init *UnityEditor
    /// </summary>
    void InitSerializeVariable() 
    {
#if UNITY_EDITOR
        _sAllFoods.Clear();

        foreach (var food in _allFoods)
        {
            _sAllFoods.Add(food.Value);
        }
#endif
    }

    /// <summary>
    /// �÷��� ������ Update *UnityEditor
    /// </summary>
    void UpdateSerializeData()
    {
#if UNITY_EDITOR
        _sCollectFoods.Clear();

        foreach (var food in _collectFoods)
            _sCollectFoods.Add(food.Value);
#endif
    }
    #endregion

    #endregion
}

[System.Serializable]
public class CollectFood
{
    public Food food;

    public string name                      { get { return food.name; } set { food.name = value; } }
    public int index                        { get { return food.index; } set { food.index = value; } }
    public int sellPrice                    { get { return food.sellPrice; } set { food.sellPrice = value; } }

    public List<FoodMaterial> materials     { get { return food.materials; } set { food.materials = value; } }
}

[System.Serializable]
public struct Food
{
    public string name;
    public int index;
    public int sellPrice;

    public List<FoodMaterial> materials;
}

[System.Serializable]
public struct FoodMaterial
{
    public string name;
    public int index;
    public int sellPrice;
}
