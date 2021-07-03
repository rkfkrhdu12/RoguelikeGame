using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    /// �Ϲ� ����
    #region Variable
    InGameInput _inputManager = null;

    /// <summary>
    /// ���� ��ȣ�ۿ� ��ư�� ����
    /// </summary>
    bool _isAction { get { return _inputManager.IsAction; } }
    #endregion

    /// MonoBehaviour ���� �Լ�
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
    /// NPC���� ��ȣ�ۿ�
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
