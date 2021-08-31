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

    // Input ���� ID,PW�� �α��� �õ�
    public void Login()
    {
        StartCoroutine(LoginRoutine());
    }

    // ������ �ʿ��� �߰����� �۾� ���� �� Ȯ��
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
    /// ID, PW �� ���� �α��� �õ� �ϴ� �Լ�
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
    /// ���� ���� ������ ������ �߰� �����͸� Ȯ��, �����ϴ� �Լ�
    /// </summary>
    IEnumerator CreateAccountValidCheckRoutine()
    {
        // (id Ȥ�� pw �� ����ְų�) && (question Ȥ�� answer�� ���������) false
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
    // ���� class
    public class Account
    {
        public string id;
        public string pw;

        public string question;
        public string answer;
    }
    #endregion

    #region Private
    // ���� Input�� ������Ʈ �Ǵ� ����
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
