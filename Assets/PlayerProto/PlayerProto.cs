using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class PlayerProto : Agent {
	
	private Transform _myTransform = null;
	private Animator _myAnimator = null;
	private Rigidbody _myRigidbody = null;
	private CharacterController _myCharacterController = null;
	
	public GameObject BulletPrefab = null;
	public float RotateSpeed = 10.0f;
	public float Invert = -1.0f;
	
	// Use this for initialization
	void Awake ()
	{
		_myTransform = transform;
		_myAnimator = GetComponent<Animator>();
		//_myRigidbody = GetComponent<Rigidbody>();
		_myCharacterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessInput();
	}
	
	void ProcessInput()
	{
		
		float xTranslation = Input.GetAxis ("Horizontal"); 
		float zTranslation = Input.GetAxis ("Vertical"); 
		float rotation = Input.GetAxis ("Horizontal") * RotateSpeed * Invert;
		
		Vector3 movement = new Vector3 (xTranslation, 0f, zTranslation);
		movement = Vector3.ClampMagnitude(movement,1.0f) * MoveSpeed * Time.deltaTime;
		
		_myCharacterController.Move (movement);
		
		rotation *= Time.deltaTime;
		
		//_myTransform.Rotate(0, rotation, 0);
		
		// Temporary animation stuff
		_myAnimator.SetFloat ("yVel", Input.GetAxis ("Horizontal")*Invert);
		_myAnimator.SetFloat ("xVel", Input.GetAxis ("Vertical"));
		
		// bad but be required with final character
		_myTransform.position = new Vector3 (_myTransform.position.x, 0f, _myTransform.position.z);
		
		if (Input.GetMouseButtonDown(0))
		{
			GameObject Bullet = Instantiate(BulletPrefab);
			Bullet bullet = Bullet.GetComponent<Bullet>();
			bullet.Shoot(Input.mousePosition, _myTransform.position);
		}
	}
}