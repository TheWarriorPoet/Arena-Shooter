using UnityEngine;
using System.Collections;

public class SceneManager_MainMenu : SceneManager_Base
{
	// Use this for initialization
	void Start ()
	{
        Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{

    }

    public void LoadGame()
    {
        if (_myGameManager != null) _myGameManager.MainMenu = false;
        Application.LoadLevel("MainGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
