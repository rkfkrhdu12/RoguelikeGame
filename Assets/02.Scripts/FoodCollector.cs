using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    /// <summary>
    /// 요리를 수집했을 때
    /// </summary>
    public void Collect(string foodName)
    {
        if (!_foodManager.Foods.ContainsKey(foodName)) return;

        if (SearchRemainFoodName(foodName) != null)
        {
            Food collectFood = _foodManager.Foods[foodName];

            _collectFoodList.Add(collectFood);

            if (_remainFoodList.Contains(collectFood))
                _remainFoodList.Remove(collectFood);

            if (GameManager.Instance.DBManager != null)
                StartCoroutine(GameManager.Instance.DBManager.FoodCollectionUpload());

            LogManager.Log(collectFood.name + " is Collect");
        }
    }
    public void Collect(int foodIndex)
    {
        if (!_foodManager.FoodsCode.ContainsKey(foodIndex)) return;

        if (SearchRemainFoodIndex(foodIndex) != null)
        {
            Food collectFood = _foodManager.FoodsCode[foodIndex];

            _collectFoodList.Add(collectFood);

            if (_remainFoodList.Contains(collectFood))
                _remainFoodList.Remove(collectFood);

            if (GameManager.Instance.DBManager != null)
                StartCoroutine(GameManager.Instance.DBManager.FoodCollectionUpload());

            LogManager.Log(collectFood.name + " is Collect");
        }
    }

    public Food SearchCollectFoodName(string foodName)
    {
        Food findFood = null;
        foreach (var i in CollectFoodList)
        {
            if (i.name == foodName)
                findFood = i;
        }

        return findFood;
    }

    public Food SearchCollectFoodIndex(int index)
    {
        Food findFood = null;
        foreach (var i in CollectFoodList)
        {
            if (i.index == index)
                findFood = i;
        }

        return findFood;
    }

    public Food SearchRemainFoodName(string foodName)
    {
        Food findFood = null;
        foreach (var i in RemainFoodList)
        {
            if (i.name == foodName)
                findFood = i;
        }

        return findFood;
    }

    public Food SearchRemainFoodIndex(int index)
    {
        Food findFood = null;
        foreach (var i in RemainFoodList)
        {
            if (i.index == index)
                findFood = i;
        }

        return findFood;
    }


    #region Public Variable
    public List<Food> CollectFoodList { get { return _collectFoodList; } }
    public List<Food> RemainFoodList { get { return _remainFoodList; } }
    #endregion

    /// 일반 변수들
    #region Private Variable
    /// <summary>
    /// 수집한 컬렉션
    /// </summary>
    List<Food> _collectFoodList;

    /// <summary>
    /// 남은 컬렉션
    /// </summary>
    List<Food> _remainFoodList;

    FoodManager _foodManager = null;

    #endregion

    /// MonoBehaviour 지원 함수
    #region MonoBehaviour Functions
    void Awake()
    {
        GameManager.Instance.FoodCollector = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _collectFoodList = new List<Food>();
        _remainFoodList = new List<Food>();

        if (_foodManager == null) _foodManager = GameManager.Instance.FoodManager;
        var foodcodes = _foodManager.FoodsCode;
        for (int i = 0; i < _foodManager.Foods.Count; ++i)
        {
            Food curFood = foodcodes[i];

            _remainFoodList.Add(curFood);
        }

        StartCoroutine(GameManager.Instance.DBManager.FoodCollectionDownload());
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
