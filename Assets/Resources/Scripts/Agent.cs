using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// MethodOrClassName
// Block Comment
//-------------------------------------------------------------------------------------

public class Agent : MonoBehaviour {
	public float MoveSpeed;
	public float HP;
	Stack commandStack;

    // To store Gamemanger reference -- AJ
    protected GameManager _myGameManager = null;
	// Use this for initialization
	void Awake () {
        _myGameManager = GameManager.instance;
		commandStack = new Stack ();
		commandStack.Push (new Seek (this));
	}
	
	// Update is called once per frame
	void Update () {
		Behaviour tempCommand = (Behaviour)commandStack.Peek ();
		tempCommand.Update ();
	}
}
