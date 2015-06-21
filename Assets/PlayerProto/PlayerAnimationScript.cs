using UnityEngine;
using System.Collections;

public class PlayerAnimationScript : MonoBehaviour {

	// Use this for initialization
	private Animator animator;
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (animator != null) {
	
			animator.SetFloat ("xVel", Input.GetAxis ("Horizontal"));
			animator.SetFloat ("yVel", Input.GetAxis ("Vertical"));
		} else
			print ("fail");
	}
}
