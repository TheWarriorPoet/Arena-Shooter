using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    public ArenaLevel ThisLevel = new ArenaLevel();
    private SceneManager_MainGame _SceneManager = SceneManager_MainGame.instance;
    private GameManager _myGameManager = null;
	// Use this for initialization
	void Start () {
        _myGameManager = GameManager.instance;
        _SceneManager = SceneManager_MainGame.instance;
	}
	
	// Update is called once per frame
	void Update () {
        // if (_SceneManager != null && _SceneManager.Paused) return;
        foreach (EnemySpawner es in ThisLevel.SpawnPoints)
        {
            if (es.gameObject.activeSelf && es.spawnHealth <= 0.0f)
            {
				es.Die();
				es.gameObject.SetActive(false);
                ThisLevel.ActiveSpawners--;
            }
        }
        if (ThisLevel.ActiveSpawners <= 0)
        {
            ThisLevel.LevelOver = true;
            if (_myGameManager != null)
            {
                _myGameManager.LevelComplete();
            }
        }
	}
}
