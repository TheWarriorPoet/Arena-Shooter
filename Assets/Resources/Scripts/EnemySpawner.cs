using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float spawnDelay = 0.5f;
	float spawnCounter = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnDelay) {
			GameObject EnemyObj =(GameObject)Resources.Load("Prefabs/Enemy");
			GameObject.Instantiate(EnemyObj, gameObject.transform.position,Quaternion.identity);
			spawnCounter = 0;
		}
	}
}
