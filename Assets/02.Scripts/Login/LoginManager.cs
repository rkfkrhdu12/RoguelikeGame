using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    // https://blog.embian.com/69
    // https://velog.io/@jinny_0422/Android-PHP%EC%99%80-MYSQL%EC%9D%84-%EC%9D%B4%EC%9A%A9%ED%95%9C-DB-%EA%B4%80%EB%A6%AC%ED%95%98%EA%B8%B0
    // https://www.youtube.com/watch?v=8ZD9qRjYH90

    #region Function

    // Input 받은 ID,PW로 로그인 시도
    public void Login()
    {
        StartCoroutine(LoginRoutine());
    }

    // 생성에 필요한 추가적인 작업 진행 및 확인
    public void CreateAccountValidCheck()
    {
        StartCoroutine(CreateAccountValidCheckRoutine());
    }

    public void OnInputID(string id)
    {
        _curAccount.id = id;
    }
    public void OnInputPW(string pw)
    {
        _curAccount.pw = pw;
    }
    public void OnInputPW2(string pw2)
    {
        _checkPW = pw2;
    }
    public void OnInputQuestion(string q)
    {
        _curAccount.question = q;
    }
    public void OnInputAnswer(string a)
    {
        _curAccount.answer = a;
    }

    public void ResetInputData()
    {
        _curAccount.id = "";
        _curAccount.pw = "";
        _checkPW = "";
        _curAccount.question = "";
        _curAccount.answer = "";
    }

    #region Coroutine Function

    /// <summary>
    /// ID, PW 를 통해 로그인 시도 하는 함수
    /// </summary>
    IEnumerator LoginRoutine()
    {
        if (!(string.IsNullOrWhiteSpace(_curAccount.id) || string.IsNullOrWhiteSpace(_curAccount.pw)))
        {
            WWWForm form = new WWWForm();
            form.AddField("InputUserID", _curAccount.id);
            form.AddField("InputUserPW", _curAccount.pw);

            UnityWebRequest webrequest = UnityWebRequest.Post(_loginURL, form);
            yield return webrequest.SendWebRequest();

            if (webrequest.downloadHandler.text == "true")
                LoginSuccess();
            else
            {
                LogManager.Log(webrequest.downloadHandler.text);
                _uiManager.LoginFailed();
            }
        }
        else
            _uiManager.LoginFailed();
    }

    /// <summary>
    /// 새로 생성 가능한 계정의 추가 데이터를 확인, 삽입하는 함수
    /// </summary>
    IEnumerator CreateAccountValidCheckRoutine()
    {
        // (id 혹은 pw 가 비어있거나) && (question 혹은 answer이 비어있으면) false
        if ((!(string.IsNullOrWhiteSpace(_curAccount.id) || string.IsNullOrWhiteSpace(_curAccount.pw)) &&
            !(string.IsNullOrWhiteSpace(_curAccount.question) || string.IsNullOrWhiteSpace(_curAccount.answer))) &&
            _checkPW == _curAccount.pw)
        {
            WWWForm form = new WWWForm();
            form.AddField("InputUserID", _curAccount.id);
            form.AddField("InputUserPW", _curAccount.pw);
            form.AddField("InputFindQuestion", _curAccount.question);
            form.AddField("InputFindAnswer", _curAccount.answer);

            UnityWebRequest webrequest = UnityWebRequest.Post(_createAccountURL, form);
            yield return webrequest.SendWebRequest();

            if (webrequest.downloadHandler.text == "true")
                _uiManager.CreateAccountValidSuccess();
            else
            {
                LogManager.Log(webrequest.downloadHandler.text);

                _uiManager.CreateAccountValidFailed();
            }
        }
        else
            _uiManager.CreateAccountValidFailed();

        yield return null;
    }

    #endregion

    #region Private Function
    void LoginSuccess()
    {
        GameManager.Instance.DBManager.Init(_curAccount.id);

        _uiManager.LoginSuccess();
        GameManager.Instance.SceneController.LoadScene("GameLobbyScene");
    }

    #endregion

    #endregion

    #region Variable

    #region Public 
    // 계정 class
    public class Account
    {
        public string id;
        public string pw;

        public string question;
        public string answer;
    }
    #endregion

    #region Private
    // 현재 Input이 업데이트 되는 계정
    Account _curAccount = null;

    // UI
    LoginUIManager _uiManager = null;

    string _checkPW = "";

    // php URL
    private const string _loginURL = DBManager.dbDomain + "Login.php";
    private const string _createAccountURL = DBManager.dbDomain + "CreateAccount.php";

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
