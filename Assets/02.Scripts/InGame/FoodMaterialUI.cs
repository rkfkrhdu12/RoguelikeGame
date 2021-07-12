using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMaterialUI : MonoBehaviour
{
    public void OnUpdate(List<Inventory.ItemFoodMaterial> itemFoodMaterials, ItemSlot.UIAction action)
    {
        if (_uiList == null) return;

        int uiCount = _uiList.Count;
        int listCount = itemFoodMaterials.Count;
        // ���� �κ��丮�� ��᰹��(4) - ���� ���UI����(3)
        int count = listCount - uiCount;

        if (count > 0)
        { /// �������� �߰��ؾ���.
            for (int i = 0; i < count; ++i)
            {
                ItemSlot curSlot = _remainSlotObjects.Dequeue().GetComponent<ItemSlot>();
                if (curSlot == null) return;
                
                for (int j = 0; j < listCount; ++j)
                {
                    bool _isFind = false;
                    string curFoodMatName = itemFoodMaterials[j].foodMaterial.name;
                    for (int k = 0; k < uiCount; ++k)
                    {
                        if (_uiList[k].foodMaterial.name == curFoodMatName)
                            _isFind = true;
                    }

                    if (!_isFind)
                    {
                        curSlot.FoodMaterial = itemFoodMaterials[j];
                        curSlot.curAction += action;
                        curSlot.transform.SetParent(_layoutGroupObject.gameObject.transform);
                        curSlot.gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (count < 0)
        { /// �������� �����ؾ���.

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

    Queue<GameObject> _remainSlotObjects = null;
    [SerializeField] GameObject _slotPrefab = null;

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

                GameObject newButton = Instantiate(_slotPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);

                _remainSlotObjects.Enqueue(newButton);
            }
        }
    }

    #endregion
}
