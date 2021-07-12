using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public void OnDown()
    {
        if (_inputManager == null) return;

        _prevMousePosition = _inputManager.MousePosition;
    }

    public void OnUp()
    {
        if (_inputManager == null) return;

        float mouseDistance = Vector2.Distance(_inputManager.MousePosition, transform.position);
        float prefMouseDistance = Vector2.Distance(_inputManager.MousePosition, _prevMousePosition);
        if (prefMouseDistance >= 12.0f || mouseDistance >= _rectTransform.sizeDelta.x / 2) 
        {
            _isAction = true;
        }
    }

    public void OnClick()
    {
        if (!_isAction) return;
        _isAction = false;

        if (curAction == null) return;
        if (FoodMaterial == null) return;
        curAction(FoodMaterial);
    }

    #region Variable

    RectTransform _rectTransform = null;

    public delegate void UIAction(Inventory.ItemFoodMaterial foodMaterial);
    public UIAction curAction;
    private Inventory.ItemFoodMaterial _foodMaterial = null;
    public Inventory.ItemFoodMaterial FoodMaterial { get => _foodMaterial; set => _foodMaterial = value; }

    bool _isAction = true;

    Vector2 _prevMousePosition = Vector2.zero;

    InGameInput _inputManager = null;


    #endregion

    #region Monobehaviour Function
    private void Start()
    {
        if (_inputManager == null)
            _inputManager = GameManager.Instance.InGameManager.InGameInput;

        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    } 
    #endregion
}
