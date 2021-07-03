using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// �Ϲ� ����
    #region Variable
    InGameInput _inputManager = null;
    Rigidbody _rigid = null;

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    [SerializeField, Range(40f, 120.0f), Header("���� ĳ������ �̵��ӵ�")]
    float _moveSpeed = 8.0f;

    /// <summary>
    /// ���� ����Ű�� ����
    /// </summary>
    Vector2 _moveAxis { get { return _inputManager.MoveAxis; } }
    #endregion

    /// MonoBehaviour ���� �Լ�
    #region Monobehaviour Function

    private void Awake()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GameManager.Instance.InGameInput;
        }
    }

    private void FixedUpdate()
    {
        if (_inputManager.CurMode != InGameInput.eInputMode.CharacterMode) return;

        _rigid.AddForce(new Vector3(_moveAxis.x, 0, _moveAxis.y).normalized * _moveSpeed);
    } 
    #endregion
}
