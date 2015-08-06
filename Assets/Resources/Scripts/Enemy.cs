using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	public float Damage;
	public GameObject[] particles;
    public int hpChance = 0;
    public GameObject HPPack;
    private SceneManager_MainGame _SceneManager = null;

	// Use this for initialization
	void Awake () {
        HP += HP * GameObject.FindGameObjectWithTag("SpawnController").GetComponent<EnemyManager>().CurrentWave/10;
        _SceneManager = SceneManager_MainGame.instance;
		commandStack = new Stack ();
		commandStack.Push (new Seek (this));
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
