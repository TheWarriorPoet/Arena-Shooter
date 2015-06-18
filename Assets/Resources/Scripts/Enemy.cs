using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	// Use this for initialization
	void Awake () {
		commandStack = new Stack ();
		commandStack.Push (new Seek (this));
	}
	
	// Update is called once per frame
	void Update () {
		Behaviour tempCommand = (Behaviour)commandStack.Peek ();
		tempCommand.Update ();
	}
}
