using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float spawnDelay = 0.5f;
	float spawnCounter = 0f;
	//public float spawnCap = 200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnDelay && gameObject.GetComponentInParent<EnemyManager> ().canSpawn) {
            gameObject.GetComponentInParent<EnemyManager>().numEnemies += 1;
			GameObject tempObj =(GameObject)Resources.Load("Prefabs/Enemy");
			GameObject EnemyObj = (GameObject)Instantiate(tempObj, gameObject.transform.position,Quaternion.identity);
			EnemyObj.transform.SetParent(transform.parent);
			spawnCounter = 0;
		}
	}
}
