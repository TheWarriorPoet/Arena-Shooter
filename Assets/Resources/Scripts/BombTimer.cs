using UnityEngine;
using System.Collections;

public class BombTimer : MonoBehaviour {
	public GameObject explosion;
	public float detTimer = 3;
	float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > detTimer) {
			Instantiate(explosion,gameObject.transform.position , gameObject.transform.rotation);
			Destroy (gameObject);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            timer = detTimer + 1;
        }
    }
}
