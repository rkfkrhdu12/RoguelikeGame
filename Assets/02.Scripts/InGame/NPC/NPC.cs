using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    /// <summary>
    /// NPC와의 상호작용을 위한 가상 함수
    /// </summary>
    public virtual bool Action()
    {
        if (!_isAction) return false;

        _inputManager.ChangeUIMode();
        _isAction = false;
        _isActive = true;

        _myUI?.SetActive(true);

        return true;
    }

    /// 일반 변수
    #region Variable
    InGameInput _inputManager = null;

    /// UI버튼들의 부모 오브젝트
    // UI를 직접 대입 하는것이 아닌 전역 클래스에서 갖고 오는 방법을 적용해야함.
    protected GameObject _myUI = null;

    bool _isActive = false;

    /// 현재 상호작용이 동작하였는가 ?
    protected bool _isAction = false;
    /// <summary>
    /// 상호작용이 동작 시 쿨타임으로 돌아갈 변수
    /// </summary>
    float _coolTime = 0.0f;
    /// <summary>
    /// 쿨타임이 돌아갈 시 초기화를 정할 변수
    /// </summary>
    [SerializeField, Header("쿨타임이 돌아갈 시 초기화를 정할 변수")]
    float _coolInterval = .5f;

    #endregion

    /// MonoBehaviour 지원 함수
    #region Monobehaviour Function

    protected void Awake()
    {
        gameObject.tag = "NPC";

        if (_myUI == null)
            Debug.LogErrorFormat(gameObject.name + " is UI is Null");

        if (_myUI.activeSelf)
            _myUI?.SetActive(false);
    }

    protected void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GameManager.Instance.InGameManager.InGameInput;
        }
    }

    protected void Update()
    {
        // 현재 상호작용을 하였다면 false
        if (!_isAction)
        {
            // 현재 UI모드가 아니라면 true
            if (_inputManager.CurMode != InGameInput.eInputMode.UIMode)
            {
                // 활성화가 중지 되며 UI를 종료한다.
                _isActive = false;

                _myUI?.SetActive(false);
            }

            // 활성화가 중지 되었다면 true
            if (!_isActive)
            {
                // 상호작용 버튼 활성화를 위한 쿨타임
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

    #region Protected Function

    protected void AddItemSlot(string itemName)
    {

    }

    #endregion
}
