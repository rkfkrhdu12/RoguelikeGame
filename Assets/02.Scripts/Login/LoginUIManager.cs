using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginUIManager : MonoBehaviour
{
    #region Function

    #region Event Function

    public void OnLoginButton()
    {
        if (_curState != eState.Default) return;

        Login();
    }

    public void OnCreateAccountButton()
    {
        if (_curState == eState.CreateAccount)
        { // 계정이 유효한지 확인 후 FindString 유효한지 시도
            CreateAccountValidCheck();
        }
    }

    public void OnExitButton()
    {
        UpdateState(eState.Default);
    }

    public void OnCreateAccountModeButton()
    {
        _loginManager.ResetInputData();

        UpdateState(eState.CreateAccount);
    }
    #endregion

    #region Public Function

    public void LoginSuccess()
    {
        UpdateState(eState.Success);
    }

    public void LoginFailed()
    {
        StartCoroutine(UpdateMsg(_loginErrorMsg));

        UpdateState(eState.Default);
    }

    public void CreateAccountValidSuccess()
    {
        _loginManager.ResetInputData();
        UpdateState(eState.Default);
    }

    public void CreateAccountValidFailed()
    {
        StartCoroutine(UpdateMsg(_overlapIDErrorMsg));
    }
    #endregion  

    #region Private Function

    private IEnumerator UpdateMsg(string msg)
    {
        if (!string.IsNullOrWhiteSpace(msg))
        {
            _msgUI.text = msg;
            _msgUI.gameObject.SetActive(true);

            yield return _msgWaitTime;

            _msgUI.gameObject.SetActive(false);
        }
    }

    // UI들 Init
    private void InitUINode()
    {
        LoginManagerUINode defaultNode = new DefaultUINode();
        LoginManagerUINode CreateAccountNode = new CreateAccountValidUINode();
        LoginManagerUINode LoginNode = new LoginUINode();
        LoginManagerUINode SuccessNode = new SuccessUINode();

        defaultNode.Start(eState.Default                    , ref _nextNode);
        CreateAccountNode.Start(eState.CreateAccount          , ref _nextNode);
        LoginNode.Start(eState.Login                        , ref _nextNode);
        SuccessNode.Start(eState.Login                      , ref _nextNode);

        _uiNodes.Add(eState.Default, defaultNode);
        _uiNodes.Add(eState.CreateAccount, CreateAccountNode);
        _uiNodes.Add(eState.Login, LoginNode);
        _uiNodes.Add(eState.Success, SuccessNode);

        _uiNodes[eState.Success].AddUIObject(_pcUIObject);

        _uiNodes[eState.CreateAccount].AddUIObject(_createAccountUIObject);

        _createAccountUIObject.SetActive(false);
    }

    private void InitInputFieldEvent()
    {
        // Login

        if (_idInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputID);
            _idInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _idInputField is null");

        if (_pwInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputPW);
            _pwInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _pwInputField is null");

        // CreateAccount

        if (_createIDInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputID);
            _createIDInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _createIDInputField is null");
        if (_createPWInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputPW);
            _createPWInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _createPWInputField is null");

        if (_createPW2InputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputPW2);
            _createPW2InputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _createPW2InputField is null");
        if (_createQuestionInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputQuestion);
            _createQuestionInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _createQuestionInputField is null");
        if (_createAnswerInputField != null)
        {
            var submitEvent = new TMP_InputField.SubmitEvent();
            submitEvent.AddListener(_loginManager.OnInputAnswer);
            _createAnswerInputField.onEndEdit = submitEvent;
        }
        else LogManager.Log("class Login : _createAnswerInputField is null");
    }

    private void Login()
    {
        UpdateState(eState.Login);

        _loginManager.Login();
    }

    private void CreateAccountValidCheck()
    {
        _loginManager.CreateAccountValidCheck();
    }

    private void UpdateState(eState state)
    {
        _curState = state;
        _nextNode.curNode = _uiNodes[state];

        _curNode = _curNode.NextNodeEnable();
    }
    #endregion
    #endregion

    #region Variable

    #region UI Masage Box
    const string _loginErrorMsg = "잘못된 아이디 혹은 비밀번호입니다.";
    const string _overlapIDErrorMsg = "중복되거나 잘못된 아이디입니다.";
#endregion
    
    #region enum
    public enum eState
    {
        Default,

        CreateAccount,

        Login,
        Success,
        LAST
    }
#endregion
    
    #region Serialize Variable
    
    [SerializeField, Header("실제 로그인관련 함수들의 클래스 및 UI오브젝트")]
    private LoginManager _loginManager = null;
    
    [SerializeField]
    private GameObject _createAccountUIObject = null;
    
    [SerializeField]
    private GameObject _pcUIObject = null;
    
    [Header("로그인")]
    [SerializeField]
    private TMP_InputField _idInputField = null;
    
    [SerializeField]
    private TMP_InputField _pwInputField = null;
    
    [Header("ID 만들기")]
    [SerializeField]
    private TMP_InputField _createIDInputField = null;
    
    [SerializeField]
    private TMP_InputField _createPWInputField = null;

    [SerializeField]
    private TMP_InputField _createPW2InputField = null;

    [SerializeField]
    private TMP_InputField _createQuestionInputField = null;
    
    [SerializeField]
    private TMP_InputField _createAnswerInputField = null;
    
    [Header("현재 상태를 표시할 UI")]
    [SerializeField]
    private TMP_Text _msgUI = null;
    
    [Header("관찰을 위한 값")]
    [SerializeField]
    eState _curState = eState.Default;
    
    #endregion
    
    #region Private Variable

    WaitForSeconds _msgWaitTime = new WaitForSeconds(2.0f);

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
        InitInputFieldEvent();

        _curState = eState.Default;
        _nextNode.curNode = _uiNodes[_curState];
        _curNode = _uiNodes[_curState];

    }
#endregion
}

class LoginManagerUINode : UINode
{
    LoginUIManager.eState _myState;
    public LoginUIManager.eState CurState { get { return _myState; } }

    public List<GameObject> _uiObjects = null;

    public void AddUIObject(GameObject obj)
    {
        if (_uiObjects == null) return;

        _uiObjects.Add(obj);
    }

    public virtual void Start(LoginUIManager.eState mystate, ref UINodeRef refNode)
    {
        base.Start(ref refNode);

        _uiObjects = new List<GameObject>();
        _myState = mystate;
    }

    public override void Update()
    {
        base.Update();
    }

    public override UINode Enable()
    {
        if (_uiObjects != null && _uiObjects.Count != 0)
        {
            foreach (var i in _uiObjects)
                i.SetActive(true);
        }

        return base.Enable();
    }

    public override UINode NextNodeEnable()
    {
        if (_uiObjects != null && _uiObjects.Count != 0)
        {
            foreach (var i in _uiObjects)
                i.SetActive(false);
        }

        moveNode.curNode.Enable();

        return base.NextNodeEnable();
    }
}

class DefaultUINode : LoginManagerUINode
{

}

class CreateAccountValidUINode : LoginManagerUINode
{

}

class LoginUINode : LoginManagerUINode
{

}

class SuccessUINode : LoginManagerUINode
{
    public override UINode Enable()
    {
        UINode retVal = base.Enable();

        if (_uiObjects != null && _uiObjects.Count != 0)
        {
            foreach (var i in _uiObjects)
                i.SetActive(false);
        }

        return retVal;
    }
}