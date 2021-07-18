using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollection : MonoBehaviour
{
    /// <summary>
    /// 요리를 수집했을 때
    /// </summary>
    public void Collect(string foodName)
    {
        if (!_collectFoods.ContainsKey(foodName))
        {
            AddCollectFoods(foodName, _allFoods[foodName]);

            UpdateSerializeData();
        }
    }

    /// Inspector에 보여주기위한 데이터를 직렬화시킨 변수들 *UnityEditor
    #region Serialize Variables

#if UNITY_EDITOR
    [SerializeField, Header("모든 음식컬렉션")]
    List<UIFood> _sAllFoods = new List<UIFood>();

    [SerializeField, Header("수집된 음식컬렉션")]
    List<UIFood> _sCollectFoods = new List<UIFood>();
#endif
    #endregion

    /// 일반 변수들
    #region Variables

    /// <summary>
    /// 전체 컬렉션
    /// </summary>
    Dictionary<string, UIFood> _allFoods = new Dictionary<string, UIFood>();
    Dictionary<int, UIFood> _allFoodsCode = new Dictionary<int, UIFood>();

    public Dictionary<string, UIFood> AllFoods { get { return _allFoods; } }
    public Dictionary<int, UIFood> AllFoodsCode { get { return _allFoodsCode; } }

    /// <summary>
    /// 수집한 컬렉션
    /// </summary>
    Dictionary<string, UIFood> _collectFoods = new Dictionary<string, UIFood>();
    Dictionary<int, UIFood> _collectFoodsCode = new Dictionary<int, UIFood>();

    public Dictionary<string, UIFood> CollectFoods { get { return _collectFoods; } }
    public Dictionary<int, UIFood> CollectFoodsCode { get { return _collectFoodsCode; } }

    ////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 남은 컬렉션
    /// </summary>
    Dictionary<string, UIFood> _remainFoods = new Dictionary<string, UIFood>();
    Dictionary<int, UIFood> _remainFoodsCode = new Dictionary<int, UIFood>();

    public Dictionary<string, UIFood> RemainFoods { get { return _remainFoods; } }
    public Dictionary<int, UIFood> RemainFoodsCode { get { return _remainFoodsCode; } }
    //////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 모든 재료
    /// </summary>
    Dictionary<string, FoodMaterial> _materials = new Dictionary<string, FoodMaterial>();
    Dictionary<int, FoodMaterial> _materialsCode = new Dictionary<int, FoodMaterial>();

    public Dictionary<string, FoodMaterial> FoodMaterials { get { return _materials; } }
    public Dictionary<int, FoodMaterial> FoodMaterialsCode { get { return _materialsCode; } }

    #endregion

    /// MonoBehaviour 지원 함수
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

    /// Private 함수
    #region Private Functions

    /// 재료 Init
    private void InitMaterials()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Material>();

        var matList = FoodCollectionSheet.Material.MaterialList;
        foreach (var line in matList)
        {
            if (_materials.ContainsKey(line.name)) continue;

            FoodMaterial newData = new FoodMaterial()
            {
                name = line.name,
                index = line.index,
                price = line.price,
            };

            AddMaterial(newData);
        }
    }

    /// 음식 Init
    private void InitFoods()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Food>();

        foreach (var line in FoodCollectionSheet.Food.FoodList)
        {
            if (_allFoods.ContainsKey(line.name)) continue;

            UIFood newData = new UIFood()
            {
                food = new Food(),

                name = line.name,
                index = line.index,
                price = line.price,
                foodPrice = line.foodprice,

                materials = new List<FoodMaterial>(),
            };

            if (InitFoodsMaterials(line.material0Name, newData))
                if (InitFoodsMaterials(line.material1Name, newData))
                    if (InitFoodsMaterials(line.material2Name, newData))
                        if (InitFoodsMaterials(line.material3Name, newData))
                            if (InitFoodsMaterials(line.material4Name, newData)) { }

            AddAllFoods(line.name, newData);
        }
    }

    /// 음식에 재료 Init
    bool InitFoodsMaterials(string matName, UIFood newData)
    {
        if (!string.IsNullOrWhiteSpace(matName) )
        {
            FoodMaterial foodMaterial = null;
            if (_materials.ContainsKey(matName))
                foodMaterial = _materials[matName];
            else if (_allFoods.ContainsKey(matName))
                foodMaterial = _allFoods[matName].food;
            else return false;

            newData.materials.Add(foodMaterial);

            return true;
        }

        return false;
    }

    private void AddAllFoods(string key, UIFood value)
    {
        _allFoods.Add(key, value);
        _allFoodsCode.Add(value.index, value);

        /////////////////////////////////////////
        _remainFoods.Add(key, value);
        _remainFoodsCode.Add(value.index, value);
    }

    private void AddCollectFoods(string key, UIFood value)
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

    /// 직렬화된 변수들에 대한 함수 *UnityEditor
    #region Serialize Functions

    /// <summary>
    /// Inspector 에 보여주기위한 직렬화된 변수들을 Init *UnityEditor
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
    /// 컬렉션 데이터 Update *UnityEditor
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
public class UIFood
{
    public Food food;

    public string name                      { get { return food.name; } set { food.name = value; } }
    public int index                        { get { return food.index; } set { food.index = value; } }
    public int price                        { get { return food.price; } set { food.price = value; } }
    public int foodPrice                    { get { return food.foodPrice; } set { food.foodPrice = value; } }

    public List<FoodMaterial> materials     { get { return food.materials; } set { food.materials = value; } }
}

[System.Serializable]
public class Food : FoodMaterial
{
    public List<FoodMaterial> materials;
}

[System.Serializable]
public class FoodMaterial
{
    public string name;
    public int index;
    public int price;
    public int foodPrice;
}
