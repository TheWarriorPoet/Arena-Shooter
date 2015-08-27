using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {


    public GameObject bombPrefab;
    public float BaseBombSpawnTime = 0;
    public float RandomSpawnOffsetTime = 0;
    private float spawnTimer = 0;
    private float offset;
	// Use this for initialization
	void Start () {
        offset = Random.Range(0, RandomSpawnOffsetTime);
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        Debug.Log(spawnTimer + " - " + (BaseBombSpawnTime + offset));
        if(spawnTimer >= BaseBombSpawnTime + offset)
        {
            Instantiate(bombPrefab, gameObject.transform.position, gameObject.transform.rotation);
            spawnTimer = 0;
            offset = Random.Range(0, RandomSpawnOffsetTime);
        }
        
	}
}
