using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager
{
    public void Init()
    {
        InitMaterial();
        InitFoods();
    }

    #region Public Variable

    public Dictionary<string, FoodMaterial> FoodMaterials { get { return _materials; } }
    public Dictionary<int, FoodMaterial> FoodMaterialsCode { get { return _materialsCode; } }

    public Dictionary<string, Food> Foods { get { return _Foods; } }
    public Dictionary<int, Food> FoodsCode { get { return _FoodsCode; } } 
    #endregion

    #region Private Variable
    Dictionary<string, FoodMaterial> _materials = new Dictionary<string, FoodMaterial>();
    Dictionary<int, FoodMaterial> _materialsCode = new Dictionary<int, FoodMaterial>();

    Dictionary<string, Food> _Foods = new Dictionary<string, Food>();
    Dictionary<int, Food> _FoodsCode = new Dictionary<int, Food>();

    readonly int _foodMatMaxCount = 5;
    void InitMatNameArray(FoodCollectionSheet.Food foodInfo, out string[] matNames)
    {
        matNames = new string[_foodMatMaxCount];
        matNames[0] = foodInfo.material0Name;
        matNames[1] = foodInfo.material1Name;
        matNames[2] = foodInfo.material2Name;
        matNames[3] = foodInfo.material3Name;
        matNames[4] = foodInfo.material4Name;
    }
    #endregion

    #region Private Function
    private void InitMaterial()
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

            _materials.Add(newData.name, newData);
            _materialsCode.Add(newData.index, newData);
        }
    }

    private void InitFoods()
    {
        UnityGoogleSheet.Load<FoodCollectionSheet.Food>();

        foreach (var line in FoodCollectionSheet.Food.FoodList)
        {
            if (_Foods.ContainsKey(line.name)) continue;

            Food newData = new Food()
            {
                name = line.name,
                index = line.index,
                price = line.price,
                foodPrice = line.foodprice,

                materials = new List<FoodMaterial>(),
            };

            string[] matNames;
            InitMatNameArray(line, out matNames);

            for (int i = 0; i < _foodMatMaxCount; ++i)
            {
                string curMatName = matNames[i];

                if (!string.IsNullOrEmpty(curMatName) &&
                    FoodMaterials.ContainsKey(curMatName))
                    newData.AddFoodMaterial(FoodMaterials[curMatName]);
            }

            _Foods.Add(line.name, newData);
            _FoodsCode.Add(newData.index, newData);
        }
    } 
    #endregion
}
