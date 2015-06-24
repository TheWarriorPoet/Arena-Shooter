using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class Player : Agent {
	
	private Transform _myTransform = null;
	private Animator _myAnimator = null;
	private Rigidbody _myRigidbody = null;
	public GameObject BulletPrefab = null;
	
	public float RotateSpeed = 10.0f;
	public float Invert = -1.0f;
	
	// Use this for initialization
	void Awake ()
	{
		_myTransform = transform;
		_myAnimator = GetComponent<Animator>();
		_myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessInput();
	}
	
	void ProcessInput()
	{
		_myRigidbody.velocity = Vector3.zero;
		
		float xTranslation  = Input.GetAxis("Horizontal") * MoveSpeed;
		float zTranslation = Input.GetAxis("Vertical") * MoveSpeed;
		float rotation = Input.GetAxis ("Horizontal") * RotateSpeed * Invert;
		
		xTranslation *= Time.deltaTime;
		zTranslation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		
		_myTransform.Translate(xTranslation, 0, zTranslation);
		//_myTransform.Rotate(0, rotation, 0);
		
		// Temporary animation stuff
		_myAnimator.SetFloat ("yVel", Input.GetAxis ("Horizontal")*Invert);
		_myAnimator.SetFloat ("xVel", Input.GetAxis ("Vertical"));
		
		// bad bad but be aproblem with final character
		_myTransform.position = new Vector3 (_myTransform.position.x, 0f, _myTransform.position.z);
		
		if (Input.GetMouseButtonDown(0))
		{
			GameObject Bullet = Instantiate(BulletPrefab);
			Bullet bullet = Bullet.GetComponent<Bullet>();
			bullet.Shoot(Input.mousePosition, _myTransform.position);
		}
	}
}