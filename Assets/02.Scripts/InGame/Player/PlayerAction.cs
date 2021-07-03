using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /// 일반 변수
    #region Variable
    InGameInput _inputManager = null;

    /// <summary>
    /// 현재 상호작용 버튼의 상태
    /// </summary>
    bool _isAction { get { return _inputManager.IsAction; } }
    #endregion

    /// MonoBehaviour 지원 함수
    #region Monobehaviour Function

    private void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GameManager.Instance.InGameInput;
        }
    }

    const string _npcTagName = "NPC";
    const string _unTagName = "Untagged";
    /// NPC와의 상호작용
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_unTagName) || string.IsNullOrEmpty(other.tag)
            || _inputManager.CurMode != InGameInput.eInputMode.CharacterMode) return;

        if (_isAction)
        {
            if (other.CompareTag(_npcTagName))
            {
                other.GetComponent<NPC>().Action();
            }
        }
    } 
    #endregion
}
