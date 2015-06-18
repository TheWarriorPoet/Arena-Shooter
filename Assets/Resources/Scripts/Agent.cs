using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// MethodOrClassName
// Block Comment
//-------------------------------------------------------------------------------------

public class Agent : MonoBehaviour {
	public float MoveSpeed;
	public float HP;
	protected Stack commandStack;

    // To store Gamemanger reference -- AJ
    protected GameManager _myGameManager = null;
	// Use this for initialization
	void Awake () {
        _myGameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
