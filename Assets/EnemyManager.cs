using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
	public int numEnemies = 0; //Used to keep track of how many enemies are currently spawned
	public int maxNumEnemies = 0; //Max number of enemies allowed spawned before it stops spawning more
	private int spawnCap = 0;
	public float baseEnemyCount = 0; //The default number of enemies to spawn at a new wave
	public float waveMultiplier = 0; //The multiplier for enemy increase per wave. 1.5 = 50% extra units per wave e.g: Wave 1 = 10, Wave 2 = 15, Wave 3 = 22...ish?
	public float remainingEnemies = 0; //The number of enemies the player must defeat to progress to the next wave
	public float timeToNextWave = 0;
	public int CurrentWave = 1;
    public Text waveText;

	public bool canSpawn = true;
	private float spawnCounter = 0;
	public float waveInterval = 0;
	// Use this for initialization
	void Start () {
		remainingEnemies = baseEnemyCount;
		spawnCap = maxNumEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		
		spawnCounter += Time.deltaTime;
		if (remainingEnemies < maxNumEnemies) {
			spawnCap = (int)remainingEnemies;
		} else {
			spawnCap = maxNumEnemies;
		}
		if(numEnemies >= spawnCap)
		{
			canSpawn = false;
		}
		if (spawnCounter >= waveInterval) {
            CurrentWave += 1;
			spawnCounter = 0;
			baseEnemyCount *= waveMultiplier;
			remainingEnemies = baseEnemyCount;
			//waveInterval *= ((waveMultiplier * CurrentWave) / 2);
			canSpawn = true;
            waveText.text = "Wave: \n" + CurrentWave;
		}
		
		timeToNextWave = waveInterval - spawnCounter;
		
	}
}
