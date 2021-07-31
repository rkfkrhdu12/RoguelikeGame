using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager
{
    /// <summary>                   
    /// 수집된 음식 컬렉션
    /// </summary>
    private FoodCollector _foodCollector = null;          public FoodCollector FoodCollector { get { return _foodCollector; } set { if (_foodCollector == null) { _foodCollector = value; } } }

    /// <summary>
    /// 음식과 음식재료 데이터 매니저
    /// </summary>
    private FoodManager _foodManager = null;                public FoodManager FoodManager { get { return _foodManager; } set { if (_foodManager == null) { _foodManager = value; } } }

    /// <summary>
    /// 액티브 아이템 데이터 매니저
    /// </summary>
    private ActiveItemManager _activeItemManager = null;    public ActiveItemManager ActiveItemManager { get { return _activeItemManager; } set { if (_activeItemManager == null) { _activeItemManager = value; } } }

    /// <summary>
    /// 인게임 데이터 매니저
    /// </summary>
    private InGameManager _ingameManager = null;            public InGameManager InGameManager { get { return _ingameManager; } set { if (_ingameManager == null) { _ingameManager = value; } } }

    /// <summary>
    /// 씬 컨트롤러
    /// </summary>
    private SceneController _sceneController = null;        public SceneController SceneController { get { return _sceneController; } }

    /// <summary>
    /// UI 메세지 매니저
    /// </summary>
    private UIMasageManager _uiMsgManager = null;            public UIMasageManager UIMasageManager { get { return _uiMsgManager; } }

    private static void Init()
    {
        _instance = new GameManager();
        _instance._sceneController = new SceneController();

        _instance.FoodManager = new FoodManager();
        _instance.FoodManager.Init();
        _instance.ActiveItemManager = new ActiveItemManager();

        _instance._uiMsgManager = new UIMasageManager();
        _instance._uiMsgManager.Init();
    }

    /// SingleTon
    #region Static Variable

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Init();

            return _instance;
        }

    }
    #endregion
}

/// <summary>
/// Debug.Log 의 에디터에서만 작동되게끔 처리
/// </summary>
public class LogManager
{
    static public void Log(string msg)
    {
        #if UNITY_EDITOR
        Debug.Log(msg);
        #endif
    }
}

