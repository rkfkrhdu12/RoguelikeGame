using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public virtual bool Action()
    {
        if (!_isAction) return false;

        _isAction = false;

        return true;
    }

    [SerializeField]
    protected bool _isAction = false;
    [SerializeField]
    float _coolTime = 0.0f;
    [SerializeField]
    float _coolInterval = .5f;

    private void Awake()
    {
        gameObject.tag = "NPC";
    }

    protected void Update()
    {
        if (!_isAction)
        {
            _coolTime += Time.deltaTime;
            if (_coolInterval <= _coolTime)
            {
                _coolTime = 0.0f;
                _isAction = true;
            }
        }
    }
}
