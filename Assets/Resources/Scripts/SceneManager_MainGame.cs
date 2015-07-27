using UnityEngine;
using System.Collections;

public class SceneManager_MainGame : MonoBehaviour {
    public SpawnController SpawnControllerScript = null;
    private GameManager _myGameManager = null;
    public GameObject VictoryText = null;

    public int SpawnerActiveCount = 0;
    public int RemainingSpawners = 0;

	// Use this for initialization
	void Start () {
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
            if (VictoryText != null)
            {
                _myGameManager.VictoryText = VictoryText;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
