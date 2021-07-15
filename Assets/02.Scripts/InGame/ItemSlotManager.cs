using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    public void OnUpdate(List<Inventory.ItemFoodMaterial> itemFoodMaterials, ItemSlot.UIAction action)
    {
        if (_uiList == null) return;

        int listCount = itemFoodMaterials.Count;
        // 현재 인벤토리의 재료갯수(4) - 현재 재료UI갯수(3)
        if (listCount - _uiList.Count > 0)
        { /// 아이템을 추가해야함.
            if (_uiList.Count == 0)
            {
                ItemSlot curSlot = _remainSlotObjects.Dequeue().GetComponent<ItemSlot>();
                if (curSlot == null) return;

                curSlot.FoodMaterial = itemFoodMaterials[0];
                curSlot.curAction += action;
                curSlot.transform.SetParent(_layoutGroupObject);
                curSlot.gameObject.SetActive(true);

                _uiList.Add(curSlot);
            }

            AddItemSlotUI(itemFoodMaterials, action);
        }
        else if (listCount - _uiList.Count < 0)
        { /// 아이템을 제거해야함.
            
        }

    }

    private void Awake()
    {
        _transform = transform;

        if (_uiList == null)
            _uiList = new List<ItemSlot>();

        ObjectPoolCoroutine = UpdateObject();
        StartCoroutine(ObjectPoolCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(ObjectPoolCoroutine);
    }

    Transform _transform = null;

    List<ItemSlot> _uiList = null;

    [SerializeField] Transform _layoutGroupObject = null;
    public int childCount { get { return _layoutGroupObject.childCount; } }

    Queue<GameObject> _remainSlotObjects = null;
    [SerializeField] GameObject _slotPrefab = null;

    #region Private Function
    void AddItemSlotUI(in List<Inventory.ItemFoodMaterial> itemFoodMaterials, in ItemSlot.UIAction action)
    {
        bool _isFind;
        string curFoodMatName;
        ItemSlot curSlot = null;
        int listCount = itemFoodMaterials.Count;

        for (int j = 0; j < listCount; ++j)
        {
            _isFind = false;

            curSlot = _remainSlotObjects.Dequeue().GetComponent<ItemSlot>();
            if (curSlot == null) return;

            curFoodMatName = itemFoodMaterials[j].foodMaterial.name;
            for (int k = 0; k < _uiList.Count; ++k)
            {
                if (_uiList[k].FoodMaterial.Name == curFoodMatName)
                    _isFind = true;
            }

            if (!_isFind)
            {
                curSlot.FoodMaterial = itemFoodMaterials[j];
                curSlot.curAction += action;
                curSlot.transform.SetParent(_layoutGroupObject);
                curSlot.gameObject.SetActive(true);

                _uiList.Add(curSlot);
            }
        }
    }

    void RemoveItemSlotUI(in List<Inventory.ItemFoodMaterial> itemFoodMaterials, in ItemSlot.UIAction action)
    {
        bool _isFind;
        string curFoodMatName;
        ItemSlot selectSlot = null;
        int listCount = itemFoodMaterials.Count;

        for (int j = 0; j < _uiList.Count; ++j)
        {
            _isFind = false;

            curFoodMatName = _uiList[j].FoodMaterial.Name;
            for (int k = 0; k < listCount; ++k)
            {
                if (itemFoodMaterials[k].Name == curFoodMatName)
                    _isFind = true;
            }

            if (!_isFind)
            {
                selectSlot = _uiList[j];
                selectSlot.curAction -= action;
                selectSlot.transform.SetParent(_transform);
                selectSlot.gameObject.SetActive(false);

                _uiList.Remove(_uiList[j]);
            }
        }
    }
    #endregion

    #region Objectpooling

    WaitForSeconds _waitTime = new WaitForSeconds(.1f);
    IEnumerator ObjectPoolCoroutine;
    IEnumerator UpdateObject()
    {
        if (_remainSlotObjects == null)
            _remainSlotObjects = new Queue<GameObject>();

        while (true)
        {
            if (_remainSlotObjects.Count >= 5)
            {
                yield return _waitTime;
            }
            else
            {
                if (_slotPrefab == null) continue;

                GameObject newButton = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity, _transform);

                _remainSlotObjects.Enqueue(newButton);
            }
        }
    }

    #endregion
}
