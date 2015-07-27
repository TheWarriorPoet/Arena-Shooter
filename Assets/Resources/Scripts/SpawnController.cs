using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    public ArenaLevel ThisLevel = new ArenaLevel();

    private GameManager _myGameManager = null;
	// Use this for initialization
	void Start () {
        _myGameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (EnemySpawner es in ThisLevel.SpawnPoints)
        {
            if (es.gameObject.activeSelf && es.spawnHealth <= 0.0f)
            {
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
