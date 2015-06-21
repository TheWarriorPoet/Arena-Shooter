using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class PlayerProto : Agent {

    private Transform _myTransform = null;

    public GameObject BulletPrefab = null;
	private Animator _myAnimator;

    public float RotateSpeed = 10.0f;
    public float Invert = -1.0f;

	// Use this for initialization
	void Awake () {
        _myTransform = transform;
		_myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
    {
		_myAnimator.SetFloat ("yVel", Input.GetAxis ("Horizontal")*Invert);
		_myAnimator.SetFloat ("xVel", Input.GetAxis ("Vertical"));

		float translation = Input.GetAxis("Vertical") * MoveSpeed;
        float rotation = Input.GetAxis("Horizontal") * RotateSpeed * Invert;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        _myTransform.Translate(Input.GetAxis("Horizontal") * MoveSpeed*Time.deltaTime, 0f, translation);
        //_myTransform.Rotate(0f, rotation,0f);
		_myTransform.position = new Vector3 (_myTransform.position.x, 0f, _myTransform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(BulletPrefab);
            Bullet bullet = Bullet.GetComponent<Bullet>();
            bullet.Shoot(Input.mousePosition, _myTransform.position);
        }
    }
}
