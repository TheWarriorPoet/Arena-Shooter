using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SceneManager_MainMenu : SceneManager_Base
{
	public GameObject Menu = null;
	public GameObject ControlsScreen = null;
	public EventSystem EventSys = null;

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

	public void ControlScreen()
	{
		// Toggle GameObject active state
		ControlsScreen.SetActive(!ControlsScreen.activeInHierarchy);
		Menu.SetActive(!Menu.activeInHierarchy); 

		// Select first button in canvases
		if (ControlsScreen.activeInHierarchy)
		{
			EventSys.SetSelectedGameObject(ControlsScreen.GetComponentInChildren<Button>().gameObject);
		}
		else
		{
			EventSys.SetSelectedGameObject(Menu.GetComponentInChildren<Button>().gameObject);
		}
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
