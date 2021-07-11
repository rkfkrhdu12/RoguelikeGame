using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameInput : MonoBehaviour
{
    /// <summary> �����϶� ����� XY�� </summary>
    public Vector2 MoveAxis { get { return _moveAxis; } }
    /// <summary> ��ȣ�ۿ� ��ư </summary>
    public bool IsAction { get { return _isAction; } }
    /// <summary> ���� Input�۵��� ��� </summary>
    public eInputMode CurMode { get { return _curMode; } }

    public Vector2 MouseMoveDelta { get { return _mouseMoveDelta; } }
    public Vector2 MousePosition { get { return _mousePosition; } }
    // public bool IsLeftMouseButtonDown { get { return _isLeftMouseButtonDown; } }

    /// <summary> ���� �۵��� ��带 UI���� �ٲ� </summary>
    public void ChangeUIMode() { _curMode = eInputMode.UIMode; }
    /// <summary> ���� �۵��� ��带 ĳ���͸��� �ٲ� </summary>
    public void ChangeCharMode() { _curMode = eInputMode.CharacterMode; }

    /// ���ٽ����� ���Ǵ� �Լ�
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

    /// �Ϲ� ������
    #region Variable
    /// InputSystem Variable
    PlayerInput _input = null;

    /// <summary>
    /// ���� InputMode���� ����
    /// </summary>
    public enum eInputMode
    {
        CharacterMode,
        UIMode,
        Count,
    }
    /// <summary>
    /// ���� �Է¸��
    /// </summary>
    [SerializeField, Header("���� �Է� ���")]
    eInputMode _curMode = eInputMode.CharacterMode;

    // InputKeys
    [SerializeField, Header("������ XY ���� ����")]
    private Vector2 _moveAxis = Vector2.zero;
    [SerializeField, Header("��ȣ�ۿ� ��ư�� ����")]
    private bool _isAction = false;

    [SerializeField, Header("���� ���콺 �������� Delta")]
    private Vector2 _mouseMoveDelta = Vector2.zero;
    [SerializeField, Header("���� ���콺 ��ǥ")]
    private Vector2 _mousePosition = Vector2.zero;
    #endregion

    /// MonoBehaviour ���� �Լ�
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
