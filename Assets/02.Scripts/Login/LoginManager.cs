using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    // https://blog.embian.com/69
    // https://velog.io/@jinny_0422/Android-PHP%EC%99%80-MYSQL%EC%9D%84-%EC%9D%B4%EC%9A%A9%ED%95%9C-DB-%EA%B4%80%EB%A6%AC%ED%95%98%EA%B8%B0
    // https://www.youtube.com/watch?v=8ZD9qRjYH90

    // Input 받은 ID,PW로 로그인 시도
    public void Login()
    {
        StartCoroutine(LoginRoutine());
    }
    
    // Input 받은 ID, PW가 계정생성에 유효한지 확인
    public void AccountValidCheck()
    {
        StartCoroutine(AccountValidCheckRoutine());
    }

    // 생성에 필요한 추가적인 작업 진행 및 확인
    public void FindStringValidCheck()
    {
        StartCoroutine(FindStringValidCheckRoutine());
    }

    /// <summary>
    /// Event 에 등록하여 ID string 을 Input받을 함수
    /// </summary>
    public void OnInputID(string id)
    {
        _curAccount.id = id;
    }

    /// <summary>
    /// Event 에 등록하여 PW string 을 Input받을 함수
    /// </summary>
    public void OnInputPW(string pw)
    {
        _curAccount.pw = pw;
    }

    #region Coroutine Function

    /// <summary>
    /// ID, PW 를 통해 로그인 시도 하는 함수
    /// </summary>
    IEnumerator LoginRoutine()
    {
        if (string.IsNullOrWhiteSpace(_curAccount.id) || string.IsNullOrWhiteSpace(_curAccount.pw))
        {
            // _uiManager.ErrorMsg = "0"; // MsgCode 0
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("InputUserID", _curAccount.id);
            form.AddField("InputUserPW", _curAccount.pw);

            UnityWebRequest webrequest = UnityWebRequest.Post(_loginURL, form);
            yield return webrequest.SendWebRequest();

            if (webrequest.downloadHandler.text == "true") ;
            // _uiManager.LoginSuccess();
            else;
                //_uiManager.ErrorMsg = _loginErrorMsg;
        }
    }

    /// <summary>
    /// ID, PW가 새로 생성 가능한지 확인하는 함수
    /// </summary>
    IEnumerator AccountValidCheckRoutine()
    {
        LogManager.Log("CreateAccountRoutine : " + _curAccount.id + "  " + _curAccount.pw);

        // _uiManager.AccountValid();

        yield return null;
    }

    /// <summary>
    /// 새로 생성 가능한 계정의 추가 데이터를 확인, 삽입하는 함수
    /// </summary>
    IEnumerator FindStringValidCheckRoutine()
    {
        // _uiManager.CreateAccountValid();

        yield return null;
    }

    #endregion

    #region Variable

    #region Public 
    // 계정 class
    public class Account
    {
        public string id;
        public string pw;
    }
    #endregion

    #region UI Masage Box
    const string _loginErrorMsg = "잘못된 아이디 혹은 비밀번호입니다.";
    #endregion

    #region Private
    // 현재 Input이 업데이트 되는 계정
    Account _curAccount = null;

    // UI
    LoginUIManager _uiManager = null;

    // php URL
    private const string _loginURL = "kh9413.dothome.co.kr/Login.php";

    #endregion 
    #endregion

    #region Monobehaviour Function

    private void Awake()
    { // Init
        if (_curAccount == null)
            _curAccount = new Account();

        if (_uiManager == null)
            _uiManager = GetComponent<LoginUIManager>();
    } 
    #endregion
}
