using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginUIManager : MonoBehaviour
{
    #region 주석
    //public void OnMainButtonClick()
    //{
    //    switch (_curState)
    //    {
    //        case eLoginUIState.Default:
    //            _curState = eLoginUIState.Login;
    //            _loginManager.Login();
    //            break;
    //        case eLoginUIState.AccountValidCheck:
    //            _loginManager.AccountValidCheck();
    //            break;
    //    }
    //}

    //public void OnExit() 
    //{
    //    if (_curState != eLoginUIState.FindStringValidCheck ||
    //        _curState != eLoginUIState.AccountValidCheck) return;

    //    _curState = eLoginUIState.Default;
    //    _loginUIObject.SetActive(true);
    //    _createAccountUIObject.SetActive(false);
    //    _idInputField.text = _pwInputField.text = "";
    //}

    //public void OnStartCreateAccount() 
    //{
    //    if (_curState != eLoginUIState.Default) return;

    //    _curState = eLoginUIState.AccountValidCheck;
    //    _loginUIObject.SetActive(false);
    //    _createAccountUIObject.SetActive(true);
    //    _idInputField.text = "";
    //    _pwInputField.text = "";

    //    _loginManager.AccountValidCheck();
    //}

    //public void AccountValid()
    //{
    //    if (_curState != eLoginUIState.AccountValidCheck) return;

    //    _curState = eLoginUIState.FindStringValidCheck;
    //}

    //public void CreateAccountValid()
    //{
    //    _curState = eLoginUIState.Default;

    //    // 
    //}

    //public void Failed()
    //{
    //    switch (_curState)
    //    {
    //        case eLoginUIState.AccountValidCheck:
    //            AccountInValid();
    //            break;
    //        case eLoginUIState.FindStringValidCheck:

    //            break;
    //        case eLoginUIState.Login:
    //            LoginFaild();
    //            break;
    //    }
    //}

    //public void LoginSuccess()
    //{
    //    _curState = eLoginUIState.Login;

    //    _loginUIObject.SetActive(false);
    //    _createAccountUIObject.SetActive(false);
    //    _idInputField.text = "";
    //    _pwInputField.text = "";

    //    _msgUI.text = "로그인 성공";
    //}

    //#region Variable

    //#region Public 

    //public string ErrorMsg
    //{
    //    set
    //    {
    //        _msgUI.text = value;
    //        StartCoroutine(UpdateErrorMasage());
    //    }
    //    get { return _msgUI.text; }
    //}

    //#endregion

    //#region Serialize 

    //[SerializeField, Header("실제 로그인관련 함수들의 클래스")]
    //private LoginManager _loginManager = null;

    //[SerializeField, Header("아이디 입력창")]
    //private TMP_InputField _idInputField = null;

    //[SerializeField, Header("비밀번호 입력창")]
    //private TMP_InputField _pwInputField = null;

    //[SerializeField, Header("로그인을 위한 UI Object")]
    //private GameObject _loginUIObject = null;

    //[SerializeField, Header("ID를 만들기 위한 UI Object")]
    //private GameObject _createAccountUIObject = null;

    //[SerializeField, Header("현재 UI 모드")]
    //eLoginUIState _curState = eLoginUIState.Default;

    //[SerializeField, Header("메세지 UI")]
    //private TMP_Text _msgUI = null;
    //#endregion

    //#region Enum
    //enum eLoginUIState
    //{
    //    AccountValidCheck,
    //    FindStringValidCheck,

    //    Default,
    //    Login,
    //}
    //#endregion

    //#region Private

    //private readonly WaitForSeconds _errMsgWaitTime = new WaitForSeconds(2.0f);
    //#endregion 

    //#endregion

    //#region Monobehaivour Function

    //private void Start()
    //{
    //    if (_loginManager == null) { LogManager.Log("class Login : _loginManager is null !!"); return; }

    //    _curState = eLoginUIState.Default;

    //    if (!_loginUIObject.activeSelf) _loginUIObject.SetActive(true);
    //    if (_createAccountUIObject.activeSelf) _createAccountUIObject.SetActive(false);

    //    // Init IDInput
    //    if (_idInputField != null)
    //    {
    //        var submitEvent = new TMP_InputField.SubmitEvent();
    //        submitEvent.AddListener(_loginManager.OnInputID);
    //        _idInputField.onEndEdit = submitEvent;
    //    }
    //    else LogManager.Log("class Login : _idInputField is null");

    //    // Init PWInput
    //    if (_pwInputField != null)
    //    {
    //        var submitEvent = new TMP_InputField.SubmitEvent();
    //        submitEvent.AddListener(_loginManager.OnInputPW);
    //        _pwInputField.onEndEdit = submitEvent;
    //    }
    //    else LogManager.Log("class Login : _pwInputField is null");
    //}
    //#endregion

    //#region Coroutine Function

    //IEnumerator UpdateErrorMasage()
    //{
    //    if (_msgUI != null)
    //    {
    //        GameObject uiobj = _msgUI.gameObject;

    //        if (!uiobj.activeSelf) uiobj.SetActive(true);

    //        yield return _errMsgWaitTime;

    //        if (uiobj.activeSelf) uiobj.SetActive(false);
    //    }
    //}
    //#endregion

    //#region Private Function

    //private void AccountInValid()
    //{

    //}

    //private void LoginFaild()
    //{

    //}

    //#endregion 
    #endregion

    #region Function

    #region Event Function

    public void OnMainButtonClick()
    {
        if (_curState == eState.Default)
        { // 로그인 시도
            Login();
        }
        else if (_curState == eState.AccountValid)
        { // 계정이 유효한지 확인 후 FindString 유효한지 시도

        }
        else if (_curState == eState.FindStringValid)
        { // FindString 까지 유효하면 계정 생성

        }
    }
    #endregion

    #region Public Function

    public void LoginSuccess()
    {
        UpdateState(eState.Success);

    }

    public void LoginFailed()
    {
        UpdateState(eState.Default, false);

    }

    #endregion 

    #endregion

    #region Private Function

    // UI들 Init
    private void InitUINode()
    {
        LoginManagerUINode defaultNode = new DefaultUINode();
        LoginManagerUINode AccountValidNode = new AccountValidUINode();
        LoginManagerUINode FindStringValidNode = new FindStringValidUINode();
        LoginManagerUINode LoginNode = new LoginUINode();
        LoginManagerUINode SuccessNode = new SuccessUINode();

        defaultNode.Start(eState.Default, ref _nextNode);
        AccountValidNode.Start(eState.AccountValid, ref _nextNode);
        FindStringValidNode.Start(eState.FindStringValid, ref _nextNode);
        LoginNode.Start(eState.Login, ref _nextNode);
        SuccessNode.Start(eState.Login, ref _nextNode);

        AccountValidNode.prevNode = defaultNode;
        FindStringValidNode.prevNode = AccountValidNode;
        LoginNode.prevNode = defaultNode;

        _uiNodes.Add(eState.Default, defaultNode);
        _uiNodes.Add(eState.AccountValid, AccountValidNode);
        _uiNodes.Add(eState.FindStringValid, FindStringValidNode);
        _uiNodes.Add(eState.Login, LoginNode);
        _uiNodes.Add(eState.Success, SuccessNode);
    }

    private void Login()
    {
        UpdateState(eState.Login);

        _loginManager.Login();
    }

    private void UpdateState(eState state, bool isValid = true)
    {
        _curState = state;
        _nextNode.curNode = _uiNodes[_curState];
        _curNode = isValid ? _curNode.Valid() : _curNode.Invalid();
    }
    #endregion

    #region Variable

    #region enum
    public enum eState
    {
        Default,

        AccountValid,
        FindStringValid,

        Login,
        Success,
        LAST
    }
    #endregion

    #region Serialize Variable

    [SerializeField, Header("실제 로그인관련 함수들의 클래스")]
    private LoginManager _loginManager = null;

    [SerializeField, Header("아이디 입력창")]
    private TMP_InputField _idInputField = null;

    [SerializeField, Header("비밀번호 입력창")]
    private TMP_InputField _pwInputField = null;

    [SerializeField, Header("로그인을 위한 UI Object")]
    private GameObject _loginUIObject = null;

    [SerializeField, Header("ID를 만들기 위한 UI Object")]
    private GameObject _createAccountUIObject = null;

    [SerializeField,Header("현재 UI 상태")]
    eState _curState = eState.Default;

    [SerializeField, Header("메세지 UI")]
    private TMP_Text _msgUI = null;
    #endregion

    #region Private Variable

    Dictionary<eState, LoginManagerUINode> _uiNodes = new Dictionary<eState, LoginManagerUINode>();

    UINodeRef _nextNode;
    UINode _curNode;
    #endregion

    #endregion

    #region Monobehaviour Function

    private void Awake()
    {
        _curNode = new UINode();
        _nextNode = new UINodeRef();
    }

    private void Start()
    {
        InitUINode();

        _curState = eState.Default;
        _nextNode.curNode = _uiNodes[_curState];
        _curNode = _uiNodes[_curState];
    }
    #endregion
}

class LoginManagerUINode : UINode
{
    LoginUIManager.eState _myState;

    public virtual void Start(LoginUIManager.eState mystate, ref UINodeRef refNode)
    {
        base.Start(ref refNode);

        _myState = mystate;
    }
}

class DefaultUINode : LoginManagerUINode
{
    
}

class AccountValidUINode : LoginManagerUINode
{

}

class FindStringValidUINode : LoginManagerUINode
{

}

class LoginUINode : LoginManagerUINode
{

}

class SuccessUINode : LoginManagerUINode
{

}