using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class PlayerProto : Agent {
	
	private Transform _myTransform = null;
	private Animator _myAnimator = null;
	//private Rigidbody _myRigidbody = null;
	private CharacterController _myCharacterController = null;
	
	public GameObject BulletPrefab = null;
	public float RotateSpeed = 10.0f;
	public float Invert = -1.0f;
	public float ammo = 100;
	public float ammoDecrease = 10;
	public float chargeDelay = 1;
	public float chargeRate = 1;
	public float fireRate = 0.1f;

	private float fireCounter = 0;
	private float chargeTimer;
	private bool hasReleased = true;

	// Use this for initialization
	void Awake ()
	{
		_myTransform = transform;
		_myAnimator = transform.GetChild (1).GetComponent<Animator>();
		//_myRigidbody = GetComponent<Rigidbody>();
		_myCharacterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		fireCounter += Time.deltaTime;
		print (transform.position);
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
		

		

		
		// bad but be required with final character
		_myTransform.position = new Vector3 (_myTransform.position.x, 0f, _myTransform.position.z);
	
		// Gets the player to look towards the mouse
		Vector3 v3T = Input.mousePosition;
		v3T.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
		v3T = Camera.main.ScreenToWorldPoint(v3T);
		v3T.y = 0;
		_myTransform.LookAt(v3T);

		// Implement Rotation based on Right Stick Axis
		float dirX = Input.GetAxis("AimH");
		float dirY = Input.GetAxis("AimV");

		if (dirX != 0 || dirY != 0)
		{
			float angle = Mathf.Atan2(dirX, dirY) * Mathf.Rad2Deg;

			Vector3 newEuler = _myTransform.eulerAngles;

			newEuler.y = angle;

			_myTransform.eulerAngles = newEuler;
		}

		//Animation
		Vector2 characterLeaning = new Vector2 (Input.GetAxis ("Horizontal") * Invert, Input.GetAxis ("Vertical"));

		//apply inverse of current player rotation so it's correct when passed to animator
		characterLeaning = Quaternion.Euler (0f,0f,_myTransform.eulerAngles.y*Invert)* characterLeaning;


		_myAnimator.SetFloat ("yVel", characterLeaning.x);
		_myAnimator.SetFloat ("xVel",characterLeaning.y);


		if (Input.GetAxis ("Fire2") != 0) {
			if (hasReleased) {
				hasReleased = false;
				gameObject.transform.GetChild (0).GetComponent<MeleeTrigger> ().Attack ();
			}
		} else {
			hasReleased = true;
		}
		if (Input.GetAxis ("Fire1") > 0.1f)
		{
			if(ammo > 0)
			{
				if(fireCounter > fireRate)
				{
					Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
					Vector3 dir = Input.mousePosition - objectPos; 
					if(Input.GetAxis ("AimH") != 0 || Input.GetAxis ("AimV") != 0)
					{
						dir = transform.position + new Vector3( Input.GetAxis ("AimH"),0, Input.GetAxis ("AimV"));
					}
					dir.z = dir.y;
					dir.y = 0;
			
				GameObject Bullet = Instantiate(BulletPrefab);
				Bullet bullet = Bullet.GetComponent<Bullet>();
				Vector3 mPos = Input.mousePosition;
				mPos.y = 1;
				bullet.Shoot(dir, _myTransform.position);
				ammo -= ammoDecrease;
				chargeTimer = 0;
				fireCounter = 0;
				}
			}
		}
		else
		{
			// Charge
			if (chargeTimer >= chargeDelay)
			{
				ammo += chargeRate;
			}
			else
			{
				chargeTimer += Time.deltaTime;
			}
		}
	}
}