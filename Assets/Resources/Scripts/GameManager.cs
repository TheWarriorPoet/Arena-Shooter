using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// GameManager
// Class for managing game state and variables across scenes
// Set to not destroy on scene load. Important to enter game through loadingScreen Scene
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
        Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
