using UnityEngine;
using System.Collections;

public class SceneManager_MainMenu : SceneManager_Base
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Quick hack to go to game with controller/keyboard
    	if (Input.GetAxis("Submit") > 0)
		{
			LoadGame();
		}
    }

    public void LoadGame()
    {
        Application.LoadLevel("MainGame");
    }
}
