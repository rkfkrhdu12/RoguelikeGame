using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBarPro : MonoBehaviour
{
    InGameInput _inputManager = null;

    [SerializeField] RectTransform _scrollObjectTransform = null;

    bool _isDragMode = false;
    bool _isLerpStart = false;
    Vector3 _lerpPosition = Vector3.zero;

    public void OnDragStart()
    {
        Debug.Log("DragMode On");
        _isDragMode = true;
    }

    public void OnDragEnd()
    {
        _isDragMode = false;

        RectTransform[] itemList = _scrollObjectTransform.GetComponentsInChildren<RectTransform>();
        RectTransform lastItemObjectTransform = itemList[_scrollObjectTransform.childCount - 1];

        float lastItemObjectPositionY = lastItemObjectTransform.localPosition.y;
        float lastItemObjectHeight = lastItemObjectTransform.sizeDelta.y;

        float minY = 0.0f;
        float maxY = -(lastItemObjectPositionY - lastItemObjectHeight / 2);

        Debug.Log("MaxY : " + maxY);

        Vector3 correctPosition = _scrollObjectTransform.localPosition;
        correctPosition.y = Mathf.Clamp(correctPosition.y, minY, maxY);

        if (Vector3.Distance(correctPosition, _scrollObjectTransform.localPosition) >= .5)
        {
            _isLerpStart = true;
            _lerpPosition = correctPosition;
        }
    }

    private void Start()
    {
        if (_inputManager == null)
            _inputManager = GameManager.Instance.InGameManager.InGameInput;
    }

    private void LateUpdate()
    {
        if (_scrollObjectTransform == null ||
            _inputManager == null)
            return;

        if (_isDragMode) 
        {
            float mouseMoveDeltaY = _inputManager.MouseMoveDelta.y;

            Vector3 movePosition = _scrollObjectTransform.localPosition;
            movePosition.y += mouseMoveDeltaY;
            _scrollObjectTransform.localPosition = movePosition;

            // Debug.Log(movePosition.y);
        }

        if (_isLerpStart)
        {
            _scrollObjectTransform.localPosition = Vector3.Lerp(_scrollObjectTransform.localPosition, _lerpPosition, Time.deltaTime * 4.0f);

            if (Vector3.Distance(_scrollObjectTransform.localPosition, _lerpPosition) < .5)
            {
                _isLerpStart = false;
            }
        }
    }
}
