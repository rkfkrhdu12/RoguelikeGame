using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Variable
    InputManager _inputManager = null;
    Rigidbody _rigid = null;

    [SerializeField, Range(40f, 150.0f)]
    float _moveSpeed = 8.0f;

    [SerializeField]
    GameObject _playerObject = null;

    Vector2 _moveAxis { get { return _inputManager.MoveAxis; } }
    #endregion

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
            _inputManager = GameManager.Instance.InputManager;
        }
    }

    private void FixedUpdate()
    {
        _rigid.AddForce(new Vector3(_moveAxis.x, 0, _moveAxis.y).normalized * _moveSpeed);
    } 
    #endregion
}
