using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    [SerializeField]
    NPC _curInteractionObject = null;

    private void Awake()
    {
        if(_curInteractionObject == null)
        {
            LogManager.Log("This Object is Null Error !!");

            _curInteractionObject = transform.parent.GetComponent<NPC>();
        }
    }

    public void OnInteraction()
    {
        if (_curInteractionObject == null) return;

        _curInteractionObject.Action();
    }
}
