using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MoveAxis     { get { return _moveAxis; } }
    public bool IsAction        { get { return _isAction; } }

    private eInputMode CurMode { get => _curMode; set => _curMode = value; }

    /// 람다식으로 사용되는 함수
    #region LambdaExp

    public void UpdateMoveX(InputAction.CallbackContext context)
    {
        if (CurMode == eInputMode.CharacterMode)
            _moveAxis.x = context.ReadValue<float>();
    }
    public void UpdateMoveY(InputAction.CallbackContext context)
    {
        if (CurMode == eInputMode.CharacterMode)
            _moveAxis.y = context.ReadValue<float>();
    }

    public void UpdateAction(InputAction.CallbackContext context)
    {
        if (CurMode == eInputMode.CharacterMode)
            _isAction = Mathf.Approximately(context.ReadValue<float>(), 1.0f);
    }
    #endregion

    /// 일반 변수들
    #region Variable
    PlayerInput _input;

    public enum eInputMode
    {
        CharacterMode,
        UIMode,
        Count,
    }
    eInputMode _curMode = eInputMode.CharacterMode;

    // InputKeys
    [SerializeField]
    private Vector2 _moveAxis = Vector2.zero;
    [SerializeField]
    private bool _isAction = false;
    #endregion


    /// MonoBehaviour 지원 함수
    #region Monobehaviour Functions

    void Awake()
    {
        GameManager.Instance.InputManager = this;

        _input = new PlayerInput();
        _input.Character.X.performed += val => UpdateMoveX(val);
        _input.Character.Y.performed += val => UpdateMoveY(val);

        _input.Character.Action.performed += val => UpdateAction(val);
    }

    private void OnEnable()     { _input.Enable(); }
    private void OnDisable()    { _input.Disable(); } 
    #endregion
}
