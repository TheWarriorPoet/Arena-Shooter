using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------------------
// Player
// Class to control player input
//-------------------------------------------------------------------------------------

public class Player : Agent {

    private Transform _myTransform = null;

    public GameObject BulletPrefab = null;

    public float RotateSpeed = 10.0f;
    public float Invert = -1.0f; // Not needed at the moment

	// Use this for initialization
	void Awake () {
        _myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    void ProcessInput()
    {
		float xTranslation = Input.GetAxis("Horizontal") * MoveSpeed;
		float zTranslation = Input.GetAxis("Vertical") * MoveSpeed;
		float rotation = Input.GetAxis ("Horizontal") * RotateSpeed;// * Invert;

		xTranslation *= Time.deltaTime;
		zTranslation *= Time.deltaTime;
        rotation *= Time.deltaTime;

		_myTransform.Translate(xTranslation, 0, zTranslation);
		//_myTransform.Rotate(0, rotation, 0);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(BulletPrefab);
            Bullet bullet = Bullet.GetComponent<Bullet>();
            bullet.Shoot(Input.mousePosition, _myTransform.position);
        }
    }
}
