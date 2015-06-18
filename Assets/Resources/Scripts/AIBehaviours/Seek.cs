using UnityEngine;
using System.Collections;

public class Seek : Behaviour {
	Vector3 targetPos;
	// Use this for initialization
	public Seek(Agent aAgent) : base(aAgent) {
		targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	public override void Update () {

		targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;

		agent.gameObject.GetComponent<NavMeshAgent> ().destination = targetPos;
	}
}
