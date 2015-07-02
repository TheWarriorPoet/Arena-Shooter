using UnityEngine;
using System.Collections;

public class BulletTrigger : MonoBehaviour {

	public float Damage;
	// Use this for initialization
	void Start () {
	if (Damage == 0) {
			Damage = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (!(other.gameObject.tag == "Player")) 
		{
			if(other.gameObject.tag == "Enemy")
			{
				other.gameObject.GetComponent<Agent>().HP -= Damage;
			}
			Destroy (gameObject);
		}
	}
}
