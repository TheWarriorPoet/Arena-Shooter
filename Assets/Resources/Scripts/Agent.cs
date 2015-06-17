using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
	public float MoveSpeed;
	public float HP;
	Stack commandStack;
	// Use this for initialization
	void Start () {
		commandStack = new Stack ();
		commandStack.Push (new Seek (this));
	}
	
	// Update is called once per frame
	void Update () {
		Behaviour tempCommand = (Behaviour)commandStack.Peek ();
		tempCommand.Update ();
	}
}
