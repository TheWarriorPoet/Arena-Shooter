﻿using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	public float Damage;
	public GameObject[] particles;
    public int hpChance = 0;
    public GameObject HPPack;
    public int EnemyType = 0;
    private SceneManager_MainGame _SceneManager = null;

    void Start()
    {
        _myGameManager = GameManager.instance;
        // On scene start, calculate current values
        if (_myGameManager != null)
        {
            MoveSpeed = _myGameManager.CurrentLevel.CompoundCalculator(MoveSpeed, _myGameManager.CurrentLevel.EnemySpeedMultiplier);
            Damage = _myGameManager.CurrentLevel.CompoundCalculator(Damage, _myGameManager.CurrentLevel.EnemyDamageMultiplier);
            HP = _myGameManager.CurrentLevel.CompoundCalculator(HP, _myGameManager.CurrentLevel.EnemyHealthMultiplier);
        }
    }

	// Use this for initialization
	void Awake () {

        HP += HP * GameObject.FindGameObjectWithTag("SpawnController").GetComponent<EnemyManager>().CurrentWave/10;
        //pathingType = Random.Range(0, 3);
        // GetComponent<NavMeshAgent>().speed += Random.Range(1f, 5f); 
        _SceneManager = SceneManager_MainGame.instance;
		commandStack = new Stack ();
        if(EnemyType == 0)
        {
            commandStack.Push(new Seek(this));
        }
        else if (EnemyType == 1)
        {
            commandStack.Push(new Intercept(this));
        }
	}

	
	// Update is called once per frame
	void Update () {
        // if (_SceneManager != null && _SceneManager.Paused) return; 
		Behaviour tempCommand = (Behaviour)commandStack.Peek ();
		tempCommand.Update ();
		if (HP <= 0) {
            gameObject.transform.GetComponentInParent<EnemyManager>().remainingEnemies -= 1;
			gameObject.transform.GetComponentInParent<EnemyManager> ().numEnemies -= 1;
			foreach (var p in particles)
			{
				Instantiate(p, transform.position, transform.rotation);
			}
            Random.seed = (int)System.DateTime.UtcNow.Ticks;
            int i = Random.Range(0, 100);
            if(i < hpChance)
            {
                Vector3 tempPos = transform.position;
                tempPos.y = 0.5f;
                Instantiate(HPPack, tempPos, transform.rotation);
            }
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter(Collider Other)
	{
		if (Other.gameObject.tag == "Player") {
			Other.gameObject.GetComponent<Agent>().HP -= Damage;
            Other.gameObject.GetComponent<CharacterController>().Move((Other.gameObject.transform.position - gameObject.transform.position));
		}
	}
}
