using UnityEngine;
using System.Collections;

public class SceneManager_MainGame : MonoBehaviour {
    [SerializeField]
    private ArenaLevel ThisLevel = new ArenaLevel();
    private GameManager _myGameManager = null;
    public GameObject VictoryText = null;

	// Use this for initialization
	void Start () {
        _myGameManager = GameManager.instance;
        if (_myGameManager != null)
        {
            _myGameManager.CurrentLevel = ThisLevel;
            _myGameManager.NextLevelName = ThisLevel.NextLevel;
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
