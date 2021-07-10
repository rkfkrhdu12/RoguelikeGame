using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public void OnPause()
    {
        if (_ingameManager != null)
            _inputManager.ChangeUIMode();

        if (_pauseModeOverlayUIObject != null)
            _pauseModeOverlayUIObject.SetActive(true);


        if (_inventoryUI != null)
            _inventoryUI.OnUpdate();
    }

    public void OnStart()
    {
        _inputManager.ChangeCharMode();

        if (_pauseModeOverlayUIObject != null)
            _pauseModeOverlayUIObject.SetActive(false);
    }

    InGameManager _ingameManager = null;
    InGameInput _inputManager = null;

    [SerializeField] GameObject _pauseModeOverlayUIObject = null; 

    [SerializeField] InventoryUI _inventoryUI = null;

    private void Start()
    {
        if (_ingameManager == null)
        {
            _ingameManager = GameManager.Instance.InGameManager;
        }

        if (_inputManager == null)
        {
            _inputManager = _ingameManager.InGameInput;
        }

        if (_inventoryUI != null)
        {
            _inventoryUI.Init(ref _ingameManager.GetFoodList(),ref _ingameManager.GetFoodMaterialList());
        }

        if (_pauseModeOverlayUIObject.activeSelf)
        {
            _pauseModeOverlayUIObject.SetActive(false);
        }
    }

}
