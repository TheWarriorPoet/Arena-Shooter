using UnityEngine;
using System.Collections;

public class SceneManager_MainGame : MonoBehaviour {
    public SpawnController SpawnControllerScript = null;
    private GameManager _myGameManager = null;
    public GameObject VictoryScreen = null;

    public GameObject PauseWindow = null;

    public bool Paused = false;

    public int SpawnerActiveCount = 0;
    public int RemainingSpawners = 0;

    // Singleton Instance to provide simple access through other scripts
    private static SceneManager_MainGame _instance = null;
    public static SceneManager_MainGame instance
    {
        get
        {
			//if (_instance == null)
			//{
			//	_instance = (SceneManager_MainGame)FindObjectOfType(typeof(SceneManager_MainGame));
			//}
            return _instance;
        }
    }

	// --Fix for FindObjectOfType error--
	void Awake()
	{
		if (_instance != null)
		{
			// Prohibit more than one instance of this object.
			Destroy(this);
		}
		_instance = this;
	}

	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;
        _myGameManager = GameManager.instance;
        if (_myGameManager != null)
        {
            if (SpawnControllerScript != null)
            {
                SpawnerActiveCount = SpawnControllerScript.ThisLevel.StartingSpawners;
                RemainingSpawners = SpawnControllerScript.ThisLevel.ActiveSpawners;
                _myGameManager.CurrentLevel = SpawnControllerScript.ThisLevel;
                _myGameManager.NextLevelName = SpawnControllerScript.ThisLevel.NextLevel;
            }
            _myGameManager.MainMenu = false;
            if (VictoryScreen != null)
            {
                _myGameManager.VictoryText = VictoryScreen;
            }
        }
	}

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel("MainGame");
    }

    public void Pause()
    {
        if (PauseWindow != null)
        {
            PauseWindow.SetActive(!PauseWindow.activeSelf);
        }
        Paused = !Paused;
        if (Paused)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel("MainMenu");
    }

    
}
