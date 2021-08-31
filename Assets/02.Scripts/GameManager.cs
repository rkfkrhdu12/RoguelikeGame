using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager
{
    /// ������ ���� �÷���
    private FoodCollector _foodCollector = null;          

    /// ��Ƽ�� ������ ������ �Ŵ���
    private ActiveItemManager _activeItemManager = null;    

    /// �ΰ��� ������ �Ŵ���
    private InGameManager _ingameManager = null;            

    /// ���İ� ������� ������ �Ŵ���
    private FoodManager _foodManager = null;                

    /// �� ��Ʈ�ѷ�
    private SceneController _sceneController = null;        

    /// DB �Ŵ���
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
/// Debug.Log �� �����Ϳ����� �۵��ǰԲ� ó��
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

