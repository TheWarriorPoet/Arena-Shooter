using UnityEngine;
using System.Collections;

public class Seek : Behaviour {
	Vector3 targetPos;
	// Use this for initialization
	public Seek(Agent aAgent) : base(aAgent) {
		targetPos = Vector3.zero;
	}
	
	// Update is called once per frame
	public override void Update () {
		Vector3 moveVec = targetPos - agent.gameObject.transform.position;
		moveVec.Normalize ();
		agent.gameObject.transform.Translate (moveVec * agent.MoveSpeed);
	}
}
