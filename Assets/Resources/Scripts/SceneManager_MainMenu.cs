using UnityEngine;
using System.Collections;

public class SceneManager_MainMenu : SceneManager_Base
{
	// Use this for initialization
	void Start ()
	{
        Time.timeScale = 1.0f;
        if (_myGameManager != null) { _myGameManager.MainMenu = true; } else Debug.Log("Gamemanager is null");
	}

    void OnDisable()
    {
        if (_myGameManager != null) { _myGameManager.MainMenu = false; } else Debug.Log("Gamemanager is null");
    }

	// Update is called once per frame
	void Update ()
	{

    }

    public void LoadGame()
    {
        if (_myGameManager != null)
        {
            _myGameManager.CurrentLevel.LevelNumber = 0;
        }
        Application.LoadLevel("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
