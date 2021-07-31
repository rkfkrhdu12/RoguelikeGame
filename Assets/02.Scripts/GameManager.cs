using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager
{
    /// <summary>                   
    /// ������ ���� �÷���
    /// </summary>
    private FoodCollector _foodCollector = null;          public FoodCollector FoodCollector { get { return _foodCollector; } set { if (_foodCollector == null) { _foodCollector = value; } } }

    /// <summary>
    /// ���İ� ������� ������ �Ŵ���
    /// </summary>
    private FoodManager _foodManager = null;                public FoodManager FoodManager { get { return _foodManager; } set { if (_foodManager == null) { _foodManager = value; } } }

    /// <summary>
    /// ��Ƽ�� ������ ������ �Ŵ���
    /// </summary>
    private ActiveItemManager _activeItemManager = null;    public ActiveItemManager ActiveItemManager { get { return _activeItemManager; } set { if (_activeItemManager == null) { _activeItemManager = value; } } }

    /// <summary>
    /// �ΰ��� ������ �Ŵ���
    /// </summary>
    private InGameManager _ingameManager = null;            public InGameManager InGameManager { get { return _ingameManager; } set { if (_ingameManager == null) { _ingameManager = value; } } }

    /// <summary>
    /// �� ��Ʈ�ѷ�
    /// </summary>
    private SceneController _sceneController = null;        public SceneController SceneController { get { return _sceneController; } }

    /// <summary>
    /// UI �޼��� �Ŵ���
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

