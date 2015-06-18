using UnityEngine;
using System.Collections;

public class SceneManager_Base : MonoBehaviour {

    protected GameManager _myGameManager = null;

	// Use this for initialization
	void Awake () {
        _myGameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
