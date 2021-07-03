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
    public InputManager InputManager { get { return _inputManager; } set { if (_inputManager == null) { _inputManager = value; } } }
    private InputManager _inputManager = null;

    /// SingleTon
    #region Static Variable

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = new GameManager();

            return _instance;
        }

    }
    #endregion
}
