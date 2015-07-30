using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public float spawnDelay = 0.5f;
	float spawnCounter = 0.0f;
	public float spawnCap = 200;
    public float spawnHealth = 100.0f;
    private bool spawnActive = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawnActive) return;
        if (spawnHealth <= 0.0f)
        {
            spawnActive = false;
            return;
        }
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnDelay && gameObject.GetComponentInParent<EnemyManager> ().canSpawn) {
			GameObject tempObj =(GameObject)Resources.Load("Prefabs/Enemy");
			GameObject EnemyObj = (GameObject)Instantiate(tempObj, gameObject.transform.position,Quaternion.identity);
			EnemyObj.transform.SetParent(transform.parent);
			gameObject.GetComponentInParent<EnemyManager>().numEnemies += 1;
			spawnCounter = 0;
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
