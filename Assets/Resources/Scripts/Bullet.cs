using UnityEngine;
using System.Collections;

public class Bullet : Agent {

    private Transform _myTransform = null;
    private Rigidbody _myRigidbody = null;

	// Use this for initialization
	void Awake () {
        _myTransform = transform;
        _myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Shoot(Vector3 aiming, Vector3 position)
    {
        _myTransform.position = position;
        //Vector3 force = new Vector3();
        aiming = aiming - _myTransform.position;
        aiming = aiming.normalized;
        _myRigidbody.AddForce(aiming * MoveSpeed);
    }
}
