﻿using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// MethodOrClassName
// Block Comment
//-------------------------------------------------------------------------------------

public class Agent : MonoBehaviour {
	public float MoveSpeed;
	public float HP;
	public float maxHP;
	protected Stack commandStack;

    // To store Gamemanger reference -- AJ
    protected GameManager _myGameManager = null;
	// Use this for initialization
	void Start()
	{
        _myGameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update()
	{

	}
}
