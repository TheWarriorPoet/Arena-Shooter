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
    public float Invert = -1.0f;

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
        float translation = Input.GetAxis("Vertical") * MoveSpeed;
        float rotation = Input.GetAxis("Horizontal") * RotateSpeed * Invert;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        _myTransform.Translate(0, translation, 0);
        _myTransform.Rotate(0, 0, rotation);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(BulletPrefab);
            Bullet bullet = Bullet.GetComponent<Bullet>();
            bullet.Shoot(Input.mousePosition, _myTransform.position);
        }
    }
}
