using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterialUI : MonoBehaviour
{
    public void OnUpdate(List<Inventory.ItemFoodMaterial> itemFoodMaterials)
    {
        if (_uiList == null) return;


        
        if (itemFoodMaterials.Count > _uiList.Count)
        { /// 아이템을 추가해야함.

        }
        else
        { /// 아이템을 제거해야함.

        }

    }

    private void Awake()
    {
        if (_uiList == null)
            _uiList = new List<Inventory.ItemFoodMaterial>();

        ObjectPoolCoroutine = UpdateObject();
        StartCoroutine(ObjectPoolCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(ObjectPoolCoroutine);
    }

    List<Inventory.ItemFoodMaterial> _uiList = null;

    [SerializeField] RectTransform _layoutGroupObject = null;
    public int childCount { get { return _layoutGroupObject.childCount; } }

    Queue<GameObject> _remainObjects = null;

    #region Objectpooling

    IEnumerator ObjectPoolCoroutine;
    IEnumerator UpdateObject()
    {
        if (_remainObjects == null)
            _remainObjects = new Queue<GameObject>();

        while (true)
        {
            if(_remainObjects.Count >= 5)
            {

            }


            yield return null;
        }
    }

    #endregion
}
