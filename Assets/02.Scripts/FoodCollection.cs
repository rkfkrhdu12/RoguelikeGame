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
            _collectFoods.Add(foodName, _allFoods[foodName]);

            UpdateSerializeData();
        }
    }

    /// <summary>
    /// Inspector에 보여주기위한 데이터를 직렬화시킨 변수들 *UnityEditor
    /// </summary>
    #region Serialize Variables

#if UNITY_EDITOR
    [SerializeField]
    List<string> _sAllFoods = new List<string>();

    [SerializeField]
    List<string> _sCollectFoods = new List<string>();

#endif
    #endregion

    /// 일반 변수들
    #region Variables

    /// <summary>
    /// 전체 컬렉션
    /// </summary>
    Dictionary<string, CollectFood> _allFoods = new Dictionary<string, CollectFood>();

    /// <summary>
    /// 수집한 컬렉션
    /// </summary>
    Dictionary<string, CollectFood> _collectFoods = new Dictionary<string, CollectFood>();
    #endregion

    /// <summary>
    /// MonoBehaviour 지원 함수
    /// </summary>
    #region MonoBehaviour Functions

    void Awake()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Data>();

        foreach (var line in FoodCollectionSheet.Data.DataList)
        {
            CollectFood newData = new CollectFood();
            newData.index = line.index;
            newData.name = line.name;

            _allFoods.Add(line.name, newData);
        }

        #region Serialize Variable

        InitSerializeVariable();

        #endregion
    }

    #endregion

    /// Private 함수
    #region Private Functions

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
            _sAllFoods.Add(food.Value.name);
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
            _sCollectFoods.Add(food.Value.name);
#endif
    }
    #endregion

    #endregion
}

public class CollectFood
{
    public Food food = new Food();

    public int index    { get { return food.index; } set { food.index = value; } }
    public string name  { get { return food.name; } set { food.name = value; } }
}

public class Food
{
    public int index;
    public string name;
}
