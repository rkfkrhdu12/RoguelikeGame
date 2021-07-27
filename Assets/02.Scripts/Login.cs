using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Login : MonoBehaviour
{
    //https://youtu.be/l1ZOj-ufZzc?t=568
    //https://yourpresence.tistory.com/87

    #region Private Function
    [SerializeField, Header("아이디 입력창")]
    private TMP_InputField _idInputField = null;

    [SerializeField, Header("비밀번호 입력창")]
    private TMP_InputField _pwInputField = null;

    #endregion

    #region Monobehaivour Function

    private void Start()
    {
        if (_idInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(OnSetID);
            _idInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _idInputField is null");

        if (_pwInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(OnSetPW);
            _pwInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _pwInputField is null");
    }
    #endregion

    #region Private Function
    private void OnSetID(string id)
    {
        LogManager.Log("id : " + id);
    }

    private void OnSetPW(string pw)
    {
        LogManager.Log("pw : " + pw);
    } 
    #endregion
}
