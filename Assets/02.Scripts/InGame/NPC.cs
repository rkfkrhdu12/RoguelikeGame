using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    /// <summary>
    /// NPC���� ��ȣ�ۿ��� ���� ���� �Լ�
    /// </summary>
    public virtual bool Action()
    {
        if (!_isAction) return false;

        GameManager.Instance.InGameInput.ChangeUIMode();
        _isAction = false;
        _isActive = true;

        _uiParent?.SetActive(true);

        return true;
    }

    /// �Ϲ� ����
    #region Variable
    InGameInput _inputManager = null;

    /// UI��ư���� �θ� ������Ʈ
    [SerializeField]
    protected GameObject _uiParent = null;


    bool _isActive = false;

    /// ���� ��ȣ�ۿ��� �����Ͽ��°� ?
    protected bool _isAction = false;
    /// <summary>
    /// ��ȣ�ۿ��� ���� �� ��Ÿ������ ���ư� ����
    /// </summary>
    float _coolTime = 0.0f;
    /// <summary>
    /// ��Ÿ���� ���ư� �� �ʱ�ȭ�� ���� ����
    /// </summary>
    [SerializeField, Header("��Ÿ���� ���ư� �� �ʱ�ȭ�� ���� ����")]
    float _coolInterval = .5f;

    #endregion

    /// MonoBehaviour ���� �Լ�
    #region Monobehaviour Function

    protected void Awake()
    {
        gameObject.tag = "NPC";

        _uiParent?.SetActive(false);
    }

    protected void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GameManager.Instance.InGameInput;
        }
    }

    protected void Update()
    {
        // ���� ��ȣ�ۿ��� �Ͽ��ٸ� false
        if (!_isAction)
        {
            // ���� UI��尡 �ƴ϶�� true
            if (_inputManager.CurMode != InGameInput.eInputMode.UIMode)
            {
                // Ȱ��ȭ�� ���� �Ǹ� UI�� �����Ѵ�.
                _isActive = false;

                _uiParent?.SetActive(false);
            }

            // Ȱ��ȭ�� ���� �Ǿ��ٸ� true
            if (!_isActive)
            {
                // ��ȣ�ۿ� ��ư Ȱ��ȭ�� ���� ��Ÿ��
                _coolTime += Time.deltaTime;
                if (_coolInterval <= _coolTime)
                {
                    _coolTime = 0.0f;
                    _isAction = true;
                }
            }
        }

    }
    #endregion
}
