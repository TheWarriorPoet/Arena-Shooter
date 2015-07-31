using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class PlayerProto : Agent {
	
	private Transform _myTransform = null;
	private Vector3 spawnPoint;
	public Animator _myAnimator = null;
	//private Rigidbody _myRigidbody = null;
	private CharacterController _myCharacterController = null;
	
	public GameObject BulletPrefab = null;
	public float RotateSpeed = 10.0f;
	public float Invert = -1.0f;
	public float ammo;
	public float maxAmmo = 100;
	public float ammoDecrease = 10;
	public float chargeDelay = 1;
	public float chargeRate = 1;
	public float fireRate = 0.1f;
	public float ControllerDeadZone = 0.1f;

	public int lives;
	public int maxLives = 3;
	public bool hasDied = false;

	private float fireCounter = 0;
	private float chargeTimer;
	private bool hasReleased = true;
	private bool usingGamepad = false;
	private Vector3 tempMousePos;

	// Use this for initialization
	void Awake ()
	{
		spawnPoint = transform.position;
		_myTransform = transform;
		//_myAnimator = transform.GetChild (1).GetComponent<Animator>();
		//_myRigidbody = GetComponent<Rigidbody>();
		_myCharacterController = GetComponent<CharacterController> ();

		HP = maxHP;
		ammo = maxAmmo;
		lives = maxLives;
	}
	
	// Update is called once per frame
	void Update ()
	{
		fireCounter += Time.deltaTime;

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
		if (tempMousePos != Input.mousePosition) {
			usingGamepad = false;
		}
		
		if (!usingGamepad) {
			
			Vector3 v3T = Input.mousePosition;
			tempMousePos = v3T;
			
			v3T.z = Mathf.Abs(Camera.main.transform.position.y - transform.position.y);
			v3T = Camera.main.ScreenToWorldPoint(v3T);
			v3T.y = 0;
			_myTransform.LookAt (v3T);
		}

		// Implement Rotation based on Right Stick Axis
		float dirX = Input.GetAxis("AimH");
		float dirY = Input.GetAxis("AimV");

		if (dirX > ControllerDeadZone || dirX < -ControllerDeadZone || dirY > ControllerDeadZone || dirY < -ControllerDeadZone)
		{
			usingGamepad = true;
			float angle = Mathf.Atan2(dirX, dirY) * Mathf.Rad2Deg;

			Vector3 newEuler = _myTransform.eulerAngles;

			newEuler.y = angle;

			RotateCharacter(newEuler.y);
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
                    else
                    {
                        dir.z = dir.y;
                        dir.y = 0;
                        /* ARE YOU READY FOR SOME HACKY CODE!!!!!!!!!! LET ME HEAR YOU SCREAM!!!!!!!!!!!!!! */
                        dir.x += transform.position.x;
                        dir.z += transform.position.z;
                        //LEGENDARY!!!!!!!!!!!!!! OMG and it actually works.... DISGUSTING!!!!!!
                    }

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
			if (ammo < maxAmmo)
			{
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

		// Limits
		if (ammo < 0)
		{
			ammo = 0;
		}

		if (ammo > maxAmmo)
		{
			ammo = maxAmmo;
		}

		if (HP < 0)
		{
			--lives;
			hasDied = true;
		}
		
		if (HP > maxHP)
		{
			HP = maxHP;
		}

		if (lives < 0)
		{
			lives = 0;
		}

		if (lives > maxLives)
		{
			lives = maxLives;
		}

		// Lives
		if (hasDied && lives > 0)
		{
			Respawn();
		}
		else if (hasDied && lives == 0) // fully dead m8
		{
			gameObject.SetActive(false);
		}
	}

	public void Respawn()
	{
		_myTransform.position = spawnPoint;// new Vector3(-0.9f, 0, 0.3f);
		HP = maxHP;
		ammo = maxAmmo;

		hasDied = false;
	}


	void RotateCharacter (float y)
	{
		if (y < 0) {
			y += 360;
		}
		Vector3 newEuler = _myTransform.eulerAngles;
		float tempY = y - newEuler.y;
		if (tempY > 180)
			tempY -= 360;
		if (tempY < -180)
			tempY += 360;
		
		tempY = Mathf.Clamp (tempY, -RotateSpeed*Time.deltaTime, RotateSpeed*Time.deltaTime);
		
		newEuler.y += tempY;
		_myTransform.eulerAngles = newEuler;
	}
}