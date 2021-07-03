using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// 일반 변수
    #region Variable
    InGameInput _inputManager = null;
    Rigidbody _rigid = null;

    /// <summary>
    /// 이동 속도
    /// </summary>
    [SerializeField, Range(40f, 120.0f), Header("현재 캐릭터의 이동속도")]
    float _moveSpeed = 8.0f;

    /// <summary>
    /// 현재 방향키의 상태
    /// </summary>
    Vector2 _moveAxis { get { return _inputManager.MoveAxis; } }
    #endregion

    /// MonoBehaviour 지원 함수
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
