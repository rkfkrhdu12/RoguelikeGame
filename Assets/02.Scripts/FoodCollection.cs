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
            _collectFoods.Add(foodName, _allFoods[foodName]);

            UpdateSerializeData();
        }
    }

    /// <summary>
    /// Inspector�� �����ֱ����� �����͸� ����ȭ��Ų ������ *UnityEditor
    /// </summary>
    #region Serialize Variables

#if UNITY_EDITOR
    [SerializeField]
    List<string> _sAllFoods = new List<string>();

    [SerializeField]
    List<string> _sCollectFoods = new List<string>();

#endif
    #endregion

    /// �Ϲ� ������
    #region Variables

    /// <summary>
    /// ��ü �÷���
    /// </summary>
    Dictionary<string, CollectFood> _allFoods = new Dictionary<string, CollectFood>();

    /// <summary>
    /// ������ �÷���
    /// </summary>
    Dictionary<string, CollectFood> _collectFoods = new Dictionary<string, CollectFood>();
    #endregion

    /// <summary>
    /// MonoBehaviour ���� �Լ�
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

    /// Private �Լ�
    #region Private Functions

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
            _sAllFoods.Add(food.Value.name);
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
