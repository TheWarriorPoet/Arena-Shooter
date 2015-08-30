using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ArenaLevel {
    [SerializeField]
    public List<EnemySpawner> SpawnPoints = new List<EnemySpawner>();
    
    public bool LevelOver = false;
    public bool LevelLoaded = false;
    public string NextLevel = "";
    
    // Win Conditions
    public int StartingSpawners = 0;
    public int ActiveSpawners = 0;
    public float SpawnerWinValue = 0.0f;

    // Difficulty Settings
    public int LevelNumber = 1;
    public float EnemyHealthMultiplier = 1.1f;
    public float EnemySpeedMultiplier = 1.1f;
    public float EnemyDamageMultiplier = 1.2f;
    public float SpawnerHealthMultiplier = 1.5f;
    public float PlayerShootCooldown = 1.05f;

    public float CompoundCalculator(float a_fBaseFValue, float a_fIncreaseRate)
    {
        return a_fBaseFValue * Mathf.Pow(a_fIncreaseRate, LevelNumber);
    }
    
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
        if (!MainMenu)
        {
            Application.LoadLevel("MainMenu");
        }
        MainMenu = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        
        
	}

    public void LevelComplete()
    {
        if (VictoryText != null)
        {
            Time.timeScale = 0.0f;
            VictoryText.SetActive(true);

			// Select first button in canvas
			FindObjectOfType<EventSystem>().SetSelectedGameObject(VictoryText.GetComponentInChildren<Button>().gameObject);

            //StartCoroutine("LoadScene");
        }
    }

    

    /*IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(5.0f);
        if (NextLevelName == "")
        {
            Application.LoadLevel("MainMenu");
        }
        else
            Application.LoadLevel(NextLevelName);
        Time.timeScale = 1.0f;
    }*/
}
