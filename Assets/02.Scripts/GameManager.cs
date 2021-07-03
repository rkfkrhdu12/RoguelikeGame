using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SingleTon
public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    private static GameObject _staticObject = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Init();

            return _instance;
        }
    }

    private static void Init()
    {
        if (_staticObject == null)
        {
            _staticObject = GameObject.Instantiate(, Vector3.zero, Quaternion.identity, null);

        }
    }

}
