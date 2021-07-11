using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameInput : MonoBehaviour
{
    /// <summary> 움직일때 사용할 XY축 </summary>
    public Vector2 MoveAxis { get { return _moveAxis; } }
    /// <summary> 상호작용 버튼 </summary>
    public bool IsAction { get { return _isAction; } }
    /// <summary> 현재 Input작동할 모드 </summary>
    public eInputMode CurMode { get { return _curMode; } }

    public Vector2 MouseMoveDelta { get { return _mouseMoveDelta; } }
    public Vector2 MousePosition { get { return _mousePosition; } }
    // public bool IsLeftMouseButtonDown { get { return _isLeftMouseButtonDown; } }

    /// <summary> 현재 작동할 모드를 UI모드로 바꿈 </summary>
    public void ChangeUIMode() { _curMode = eInputMode.UIMode; }
    /// <summary> 현재 작동할 모드를 캐릭터모드로 바꿈 </summary>
    public void ChangeCharMode() { _curMode = eInputMode.CharacterMode; }

    /// 람다식으로 사용되는 함수
    #region LambdaExp

    public void UpdateMoveX(InputAction.CallbackContext context) { _moveAxis.x = context.ReadValue<float>(); }
    public void UpdateMoveY(InputAction.CallbackContext context) { _moveAxis.y = context.ReadValue<float>(); }
    public void InputAction(InputAction.CallbackContext context) { _isAction = Mathf.Approximately(context.ReadValue<float>(), 1.0f); }

    public void InputEscape(InputAction.CallbackContext context)
    {
        switch (_curMode)
        {
            case eInputMode.UIMode:
                ChangeCharMode();
                break;
        }
    }

    public void InputMouseMoveDelta(InputAction.CallbackContext context)    { _mouseMoveDelta = context.ReadValue<Vector2>(); }
    public void InputMousePosition(InputAction.CallbackContext context)     { _mousePosition = Mouse.current.position.ReadValue(); }

    #endregion

    /// 일반 변수들
    #region Variable
    /// InputSystem Variable
    PlayerInput _input = null;

    /// <summary>
    /// 현재 InputMode들의 열거
    /// </summary>
    public enum eInputMode
    {
        CharacterMode,
        UIMode,
        Count,
    }
    /// <summary>
    /// 현재 입력모드
    /// </summary>
    [SerializeField, Header("현재 입력 모드")]
    eInputMode _curMode = eInputMode.CharacterMode;

    // InputKeys
    [SerializeField, Header("움직일 XY 축의 상태")]
    private Vector2 _moveAxis = Vector2.zero;
    [SerializeField, Header("상호작용 버튼의 상태")]
    private bool _isAction = false;

    [SerializeField, Header("현재 마우스 움직임의 Delta")]
    private Vector2 _mouseMoveDelta = Vector2.zero;
    [SerializeField, Header("현재 마우스 좌표")]
    private Vector2 _mousePosition = Vector2.zero;
    #endregion

    /// MonoBehaviour 지원 함수
    #region Monobehaviour Functions

    void Awake()
    {
        GetComponent<InGameManager>().InGameInput = this;

        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Character.X.performed += val => UpdateMoveX(val);
            _input.Character.Y.performed += val => UpdateMoveY(val);

            _input.Character.Action.performed += val => InputAction(val);
            _input.Any.ESC.performed += val => InputEscape(val);

            _input.Any.MouseMoveDelta.performed += val => InputMouseMoveDelta(val);
            _input.Any.MouseLeftDown.performed += val => InputMousePosition(val);
            _input.Any.MouseLeftUp.performed += val => InputMousePosition(val);
        }
    }

    private void OnEnable()     { if (_input == null) return; _input.Enable(); }
    private void OnDisable()    { if (_input == null) return; _input.Disable(); }
    #endregion
}
