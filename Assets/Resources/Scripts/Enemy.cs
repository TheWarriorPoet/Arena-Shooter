using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	public float Damage;
	// Use this for initialization
	void Awake () {
		commandStack = new Stack ();
		commandStack.Push (new Seek (this));
	}
	
	// Update is called once per frame
	void Update () {
		Behaviour tempCommand = (Behaviour)commandStack.Peek ();
		tempCommand.Update ();
		if (HP <= 0) {
			gameObject.transform.GetComponentInParent<EnemyManager> ().numEnemies -= 1;
			Destroy (gameObject);
		}
	}

}
