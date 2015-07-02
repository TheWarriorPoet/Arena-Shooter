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
	public int ammo = 1;
	public float fireRate = 0.1f;
	private float fireCounter = 0;
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
		fireCounter += Time.deltaTime;
		//print ("AimH:" + Input.GetAxis ("AimH"));
		//print ("AimV:" + Input.GetAxis ("AimV"));
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
		
		if (Input.GetAxis ("Fire1") > 0)
		{
			if(ammo > 0)
			{
				if(fireCounter > fireRate)
				{
					Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
					Vector3 dir = Input.mousePosition - objectPos; 
					if(Input.GetAxis ("AimH") != 0 || Input.GetAxis ("AimV") != 0)
					{
						dir = objectPos - transform.position;
						dir.x *= Input.GetAxis ("AimH");
						dir.y *= Input.GetAxis ("AimV");

						// ^ WTF THIS ACTUALLY WORKS?!?!?!?!? I surprise myself sometimes
					}

				Vector3 tempDir = dir;
				tempDir.y = 0;
				tempDir.z = dir.y;
				GameObject Bullet = Instantiate(BulletPrefab);
				Bullet bullet = Bullet.GetComponent<Bullet>();
				Vector3 mPos = Input.mousePosition;
				mPos.y = 1;
				bullet.Shoot(tempDir, _myTransform.position);
				//ammo -=1;
					fireCounter = 0;
				}
			}
		}
	}
}