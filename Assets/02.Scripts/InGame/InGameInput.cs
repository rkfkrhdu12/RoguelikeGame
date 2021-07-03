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

    /// <summary> ���� �۵��� ��带 UI���� �ٲ� </summary>
    public void ChangeUIMode() { _curMode = eInputMode.UIMode; }
    /// <summary> ���� �۵��� ��带 ĳ���͸��� �ٲ� </summary>
    public void ChangeCharMode() { _curMode = eInputMode.CharacterMode; }

    /// ���ٽ����� ���Ǵ� �Լ�
    #region LambdaExp

    public void UpdateMoveX(InputAction.CallbackContext context)
    {
        _moveAxis.x = context.ReadValue<float>();
    }
    public void UpdateMoveY(InputAction.CallbackContext context)
    {
        _moveAxis.y = context.ReadValue<float>();
    }

    public void InputAction(InputAction.CallbackContext context)
    {
        _isAction = Mathf.Approximately(context.ReadValue<float>(), 1.0f);
    }

    public void InputEscape(InputAction.CallbackContext context)
    {
        switch (_curMode)
        {
            case eInputMode.UIMode:
                ChangeCharMode();
                break;
            default:
                break;
        }
    }
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
    #endregion

    /// MonoBehaviour ���� �Լ�
    #region Monobehaviour Functions

    void Awake()
    {
        GameManager.Instance.InGameInput = this;

        if (_input == null)
        {
            _input = new PlayerInput();
            _input.Character.X.performed += val => UpdateMoveX(val);
            _input.Character.Y.performed += val => UpdateMoveY(val);

            _input.Character.Action.performed += val => InputAction(val);
            _input.Any.ESC.performed += val => InputEscape(val);
        }
    }

    private void OnEnable() { _input.Enable(); }
    private void OnDisable() { _input.Disable(); }
    #endregion
}
