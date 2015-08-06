﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float spawnDelay = 0.5f;
	float spawnCounter = 0.0f;
	public float spawnCap = 200;
    public float spawnHealth = 100.0f;
    private bool spawnActive = true;
	public GameObject enemyPrefab;
	public GameObject spawnPoint;
	public Animator door = null;
	public AudioSource DeathSound;
	private  EnemyManager em;

    private SceneManager_MainGame _SceneManager = null;
    private GameManager _myGameManager = null;
	// Use this for initialization
	void Start () {
        _myGameManager = GameManager.instance;
        _SceneManager = SceneManager_MainGame.instance;
		if (enemyPrefab == null) {
			enemyPrefab = (GameObject)Resources.Load("Prefabs/Enemy");
		}
		em = gameObject.GetComponentInParent<EnemyManager> ();

        if (_myGameManager != null)
        {
            spawnHealth = _myGameManager.CurrentLevel.CompoundCalculator(spawnHealth, _myGameManager.CurrentLevel.SpawnerHealthMultiplier);
        }
        else Debug.Log("Gamemanager is null");
	}

	
	// Update is called once per frame
	void Update () {
        // if (_SceneManager != null && _SceneManager.Paused) return;
		if (!spawnActive) {

			return;
		}

		if (spawnHealth <= 0.0f) {
			spawnActive = false;
			return;
		}
		spawnCounter += Time.deltaTime;
		if (em.canSpawn) {
			if  (door != null) door.SetBool ("open", true);
			if (spawnCounter >= spawnDelay) {
				//GameObject tempObj =(GameObject)Resources.Load("Prefabs/Enemy");
				GameObject EnemyObj = (GameObject)Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
				EnemyObj.transform.SetParent (transform.parent);
				gameObject.GetComponentInParent<EnemyManager> ().numEnemies += 1;
				spawnCounter = 0;
			}
		} else {
			if (door != null) door.SetBool ("open",false);
		}
	}

	public void Die(){
		door.SetBool ("Dead", true);
			if (DeathSound != null)
		{
			DeathSound.Play();
		}
	}

    /*void OnTriggerEnter(Collision other)
    {
        Debug.Log("Spawner Collision Detected");
        if (other.gameObject.tag == "Bullet")
        {
            spawnHealth -= other.gameObject.GetComponent<BulletTrigger>().Damage;
        }
    }*/
}
