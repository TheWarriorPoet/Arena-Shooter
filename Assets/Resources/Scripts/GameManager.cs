using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ArenaLevel {
    [SerializeField]
    public List<EnemySpawner> SpawnPoints = new List<EnemySpawner>();
    public float SpawnerWinValue = 0.0f;
    public bool LevelOver = false;
    public bool LevelLoaded = false;
    public string NextLevel = "";
    public int StartingSpawners = 0;
    public int ActiveSpawners = 0;
}


//-------------------------------------------------------------------------------------
// GameManager
// Class for managing game state and variables across scenes
// Set to not destroy on scene load. Important to enter game through loadingScreen Scene
//-------------------------------------------------------------------------------------

public class GameManager : MonoBehaviour {
    public ArenaLevel CurrentLevel = null;

    public GameObject VictoryText = null;

    public List<string> LevelNames = new List<string>();
    public string NextLevelName = "";
    public bool MainMenu = false;

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
        MainMenu = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        
        
	}

    public void LevelComplete()
    {
        if (VictoryText != null)
        {
            VictoryText.SetActive(true);
            StartCoroutine("LoadScene");
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(5.0f);
        if (NextLevelName == "")
            Application.LoadLevel("MainMenu");
        else
            Application.LoadLevel(NextLevelName);
        MainMenu = true;
    }
}
