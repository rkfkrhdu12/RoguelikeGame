using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBarPro : MonoBehaviour
{
    InGameInput _inputManager = null;

    [SerializeField] RectTransform _rectTransform = null;
    UnityEngine.UI.GridLayoutGroup _listGridLayout = null;

    Vector2 layoutSize = Vector2.zero;
    Vector2 cellSize = Vector2.zero;
    int maxScreenCount = 0;
    int rowMaxCount = 0;
    int columnMaxCount = 0;

    bool _isDragMode = false;
    bool _isLerpStart = false;
    Vector3 _lerpPosition = Vector3.zero;

    public void OnDragStart()
    {
        _isDragMode = true;
    }

    public void OnDragEnd()
    {
        _isDragMode = false;

        // Position Y Lerp Check

        int childCount = _rectTransform.childCount;
        if (maxScreenCount >= childCount) return;
        
        float minY = 0.0f;
        float maxY = Mathf.Ceil((childCount - maxScreenCount) / (float)columnMaxCount) * cellSize.y;

        Vector3 correctPosition = _rectTransform.localPosition;
        correctPosition.y = Mathf.Clamp(correctPosition.y, minY, maxY);

        if (Vector3.Distance(correctPosition, _rectTransform.localPosition) >= .5)
        {
            _isLerpStart = true;
            _lerpPosition = correctPosition;
        }
    }

    private void Start()
    {
        if (_inputManager == null)
            _inputManager = GameManager.Instance.InGameManager.InGameInput;

        if (_rectTransform != null)
            _listGridLayout = _rectTransform.GetComponent<UnityEngine.UI.GridLayoutGroup>();

        if (_listGridLayout != null)
        {
            layoutSize = _rectTransform.sizeDelta;
            cellSize = _listGridLayout.cellSize;

            rowMaxCount = (int)(layoutSize.y / cellSize.y);
            columnMaxCount = (int)(layoutSize.x / cellSize.x);
            maxScreenCount = rowMaxCount * columnMaxCount;
        }
    }

    private void LateUpdate()
    {
        if (_rectTransform == null ||
            _inputManager == null)
            return;

        if (_isDragMode) 
        {
            float mouseMoveDeltaY = _inputManager.MouseMoveDelta.y;

            Vector3 movePosition = _rectTransform.localPosition;
            movePosition.y += mouseMoveDeltaY;
            _rectTransform.localPosition = movePosition;
        }

        if (_isLerpStart)
        {
            _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, _lerpPosition, Time.deltaTime * 4.0f);

            if (Vector3.Distance(_rectTransform.localPosition, _lerpPosition) < .5)
            {
                _isLerpStart = false;
            }
        }
    }
}
