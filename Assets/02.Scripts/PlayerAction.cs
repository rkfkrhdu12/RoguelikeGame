using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    InputManager _inputManager = null;

    bool _isAction { get { return _inputManager.IsAction; } }

    private void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GameManager.Instance.InputManager;
        }
    }

    const string _npcTagName = "NPC";
    const string _unTagName = "Untagged";
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_unTagName) || string.IsNullOrEmpty(other.tag)) return;

        if (_isAction)
        {
            if (other.CompareTag(_npcTagName))
            {
                Debug.Log("Who r u ?!!");
                other.GetComponent<NPC>().Action();
            }
        }
    }
}
