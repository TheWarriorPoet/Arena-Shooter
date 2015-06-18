﻿using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// MethodOrClassName
// Block Comment
//-------------------------------------------------------------------------------------

public class GameManager : MonoBehaviour {

    // Singleton Instance to provide simple access through other scripts
    private static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return _instance;
        }
    }

	// Use this for initialization
	void Awake () {
        Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
