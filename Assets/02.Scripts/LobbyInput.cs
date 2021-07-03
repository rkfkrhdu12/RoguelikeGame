using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyInput : MonoBehaviour
{
    readonly string inGameSceneName = "InGameScene";
    public void LoadIngameScene(InputAction.CallbackContext context)
    {
        GameManager.Instance.SceneController.LoadScene(inGameSceneName);
    }

    PlayerInput _input = null;

    private void Awake()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
        }

        /////////////////////////////////////////////////////////////
        _input.Character.Action.performed += val => LoadIngameScene(val);
    }

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new PlayerInput();
        }
        _input.Enable();
    }
    private void OnDisable() { _input.Disable(); }
}
