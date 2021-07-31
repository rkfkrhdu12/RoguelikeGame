using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    // https://blog.embian.com/69
    // https://velog.io/@jinny_0422/Android-PHP%EC%99%80-MYSQL%EC%9D%84-%EC%9D%B4%EC%9A%A9%ED%95%9C-DB-%EA%B4%80%EB%A6%AC%ED%95%98%EA%B8%B0
    // https://www.youtube.com/watch?v=8ZD9qRjYH90

    // Input ���� ID,PW�� �α��� �õ�
    public void Login()
    {
        StartCoroutine(LoginRoutine());
    }
    
    // Input ���� ID, PW�� ���������� ��ȿ���� Ȯ��
    public void AccountValidCheck()
    {
        StartCoroutine(AccountValidCheckRoutine());
    }

    // ������ �ʿ��� �߰����� �۾� ���� �� Ȯ��
    public void FindStringValidCheck()
    {
        StartCoroutine(FindStringValidCheckRoutine());
    }

    /// <summary>
    /// Event �� ����Ͽ� ID string �� Input���� �Լ�
    /// </summary>
    public void OnInputID(string id)
    {
        _curAccount.id = id;
    }

    /// <summary>
    /// Event �� ����Ͽ� PW string �� Input���� �Լ�
    /// </summary>
    public void OnInputPW(string pw)
    {
        _curAccount.pw = pw;
    }

    #region Coroutine Function

    /// <summary>
    /// ID, PW �� ���� �α��� �õ� �ϴ� �Լ�
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
    /// ID, PW�� ���� ���� �������� Ȯ���ϴ� �Լ�
    /// </summary>
    IEnumerator AccountValidCheckRoutine()
    {
        LogManager.Log("CreateAccountRoutine : " + _curAccount.id + "  " + _curAccount.pw);

        // _uiManager.AccountValid();

        yield return null;
    }

    /// <summary>
    /// ���� ���� ������ ������ �߰� �����͸� Ȯ��, �����ϴ� �Լ�
    /// </summary>
    IEnumerator FindStringValidCheckRoutine()
    {
        // _uiManager.CreateAccountValid();

        yield return null;
    }

    #endregion

    #region Variable

    #region Public 
    // ���� class
    public class Account
    {
        public string id;
        public string pw;
    }
    #endregion

    #region UI Masage Box
    const string _loginErrorMsg = "�߸��� ���̵� Ȥ�� ��й�ȣ�Դϴ�.";
    #endregion

    #region Private
    // ���� Input�� ������Ʈ �Ǵ� ����
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
