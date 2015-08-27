using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class PlayerController : Agent {
	
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
	public float baseFireRate = 0.1f;
    private float fireRate;
    public bool scaleROF = true;
    public float bulletDeviation = 0;
	public float ControllerDeadZone = 0.1f;
	public AudioSource WeaponFire = null;
	public AudioSource WeaponEmpty = null;

	public int lives;
	public int maxLives = 3;
	public bool hasDied = false;

	private float fireCounter = 0;
	private float chargeTimer;
	private bool hasReleased = true;
	private bool usingGamepad = false;
	private Vector3 tempMousePos;

    private SceneManager_MainGame _SceneManager = null;

    void Start()
    {
        _myGameManager = GameManager.instance;
        _SceneManager = SceneManager_MainGame.instance;
        if (_myGameManager != null)
        {
            ammoDecrease = _myGameManager.CurrentLevel.CompoundCalculator(ammoDecrease, _myGameManager.CurrentLevel.PlayerShootCooldown);
        }
    }

	// Use this for initialization
	void Awake ()
	{
        
		spawnPoint = transform.position;
		_myTransform = transform;
		//_myAnimator = transform.GetChild (1).GetComponent<Animator>();
		//_myRigidbody = GetComponent<Rigidbody>();
		_myCharacterController = GetComponent<CharacterController> ();
        fireRate = baseFireRate;
		HP = maxHP;
		ammo = maxAmmo;
		lives = maxLives;
	}

	
	// Update is called once per frame
	void Update ()
	{
        if (_SceneManager != null && _SceneManager.Paused) return;
		fireCounter += Time.deltaTime;

		ProcessInput();
	}
	
	void ProcessInput()
	{
		
		float xTranslation = Input.GetAxis ("Horizontal"); 
		float zTranslation = Input.GetAxis ("Vertical"); 

		// Implement Rotation based on Right Stick Axis
		float dirX = Input.GetAxis("AimH");
		float dirY = Input.GetAxis("AimV");
		
		Vector3 movement = new Vector3 (xTranslation, 0f, zTranslation);
		movement = Vector3.ClampMagnitude(movement,1.0f) * MoveSpeed * Time.deltaTime;
		
		_myCharacterController.Move (movement);
		

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



		if (dirX > ControllerDeadZone || dirX < -ControllerDeadZone || dirY > ControllerDeadZone || dirY < -ControllerDeadZone)
		{
			usingGamepad = true;
			float angle = Mathf.Atan2(dirX, dirY) * Mathf.Rad2Deg;

			Vector3 newEuler = _myTransform.eulerAngles;

			newEuler.y = angle;

			RotateCharacter(newEuler.y);
		}

		//Animation
		Vector2 characterLeaning = new Vector2 (xTranslation * Invert, zTranslation);

		//apply inverse of current player rotation so it's correct when passed to animator
		characterLeaning = Quaternion.Euler (0f,0f,_myTransform.eulerAngles.y*Invert)* characterLeaning;


		_myAnimator.SetFloat ("yVel", characterLeaning.x);
		_myAnimator.SetFloat ("xVel",characterLeaning.y);


		if (Input.GetAxis ("Fire2") != 0) {
			if (hasReleased) {
				hasReleased = false;
				gameObject.transform.GetChild (1).GetComponent<MeleeTrigger> ().Attack ();
			}
		} else {
			hasReleased = true;
		}
		if (Input.GetAxis ("Fire1") > 0.1f)
		{
			if(ammo > 0)
			{
                if(scaleROF)
                { 
                fireRate = baseFireRate * (ammo / maxAmmo);
                }
				if(fireCounter > fireRate)
				{
					Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
					Vector3 dir;
					if(Input.GetAxis ("AimH") != 0 || Input.GetAxis ("AimV") != 0)
					{
                        dir = transform.position + new Vector3(Input.GetAxis("AimH") + Random.Range(-bulletDeviation, bulletDeviation), 0, Input.GetAxis("AimV") + Random.Range(-bulletDeviation, bulletDeviation));
                    }
                    else
                    {
                        dir = Input.mousePosition - objectPos; 
                        dir.z = dir.y;
                        dir.y = 0;
                        dir.x += transform.position.x;
                        dir.z += transform.position.z;

                        dir.x += dir.x * Random.Range(-bulletDeviation, bulletDeviation);
                        dir.z += dir.z * Random.Range(-bulletDeviation, bulletDeviation);
                    }

				    GameObject Bullet = Instantiate(BulletPrefab);
				    Bullet bullet = Bullet.GetComponent<Bullet>();
				    Vector3 mPos = Input.mousePosition;
				    mPos.y = 1;

                    Random.seed = (int)System.DateTime.UtcNow.Ticks;



				    bullet.Shoot(dir, _myTransform.position);
					if (WeaponFire != null)
					{
						WeaponFire.Play ();
					}
				    ammo -= ammoDecrease;

					if (ammo > ammoDecrease)
					{
						chargeTimer = 0;
					}
					else
					{
						chargeTimer = chargeDelay;
					}
				    
				    fireCounter = 0;
				}
			} else if (!WeaponEmpty.isPlaying){WeaponEmpty.Play ();}
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
            if (_SceneManager != null) { _SceneManager.LostGame(); } else Debug.Log("SceneManager is null");
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