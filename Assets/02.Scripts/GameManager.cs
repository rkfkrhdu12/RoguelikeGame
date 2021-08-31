using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager
{
    /// 수집된 음식 컬렉션
    private FoodCollector _foodCollector = null;          

    /// 액티브 아이템 데이터 매니저
    private ActiveItemManager _activeItemManager = null;    

    /// 인게임 데이터 매니저
    private InGameManager _ingameManager = null;            

    /// 음식과 음식재료 데이터 매니저
    private FoodManager _foodManager = null;                

    /// 씬 컨트롤러
    private SceneController _sceneController = null;        

    /// DB 매니저
    private DBManager _dbManager = null;                    

    #region Public Variable

    public FoodCollector FoodCollector { get { return _foodCollector; } set { if (_foodCollector == null) { _foodCollector = value; } } }
    public ActiveItemManager ActiveItemManager { get { return _activeItemManager; } set { if (_activeItemManager == null) { _activeItemManager = value; } } }
    public InGameManager InGameManager { get { return _ingameManager; } set { if (_ingameManager == null) { _ingameManager = value; } } }
    public FoodManager FoodManager { get { return _foodManager; } }
    public SceneController SceneController { get { return _sceneController; } }
    public DBManager DBManager { get { return _dbManager; } }
    #endregion

    private static void Init()
    {
        _instance = new GameManager();
        _instance._sceneController = new SceneController();

        _instance._foodManager = new FoodManager();
        _instance._foodManager.Init();
        _instance.ActiveItemManager = new ActiveItemManager();

        _instance._dbManager = new DBManager();
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

