using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager
{
    /// <summary>
    /// ���� �÷���
    /// </summary>
    public FoodCollection FoodCollection { get { return _foodCollection; } set { if (_foodCollection == null) { _foodCollection = value; } } }
    private FoodCollection _foodCollection = null;

    /// <summary>
    /// �Է� ���� �Ŵ���
    /// </summary>
    public InGameInput InGameInput { get { return _inGameInput; } set { if (_inGameInput == null) { _inGameInput = value; } } }
    private InGameInput _inGameInput = null;

    public SceneController SceneController { get { return _sceneController; } }
    private SceneController _sceneController = null;

    private static void Init()
    {
        _instance = new GameManager();
        _instance._sceneController = new SceneController();
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
