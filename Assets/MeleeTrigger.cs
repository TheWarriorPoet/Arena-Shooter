using UnityEngine;
using System.Collections;

public class MeleeTrigger : MonoBehaviour {
	
	public float Damage;
	public float AttackTime;
	private float AttackCounter;
	// Use this for initialization
	void Start () {
		AttackCounter = AttackTime;
		if (Damage == 0) {
			Damage = 2;
		}
		if (AttackTime == 0) {
			AttackTime = 0.2f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (AttackCounter < AttackTime) {
			AttackCounter += Time.deltaTime;
			gameObject.GetComponent<MeshRenderer> ().enabled = true;
			gameObject.GetComponent<BoxCollider> ().enabled = true;
		} else {
			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		print ("Is Something Happening?");
		if (!(other.gameObject.tag == "Player")) 
		{
			if(other.gameObject.tag == "Enemy")
			{
				other.gameObject.GetComponent<Agent>().HP -= Damage;
				other.gameObject.GetComponent<NavMeshAgent>().velocity = ((other.gameObject.transform.position - gameObject.transform.position) * 10);
				print ("Applying Force");
			}
			//Destroy (gameObject);
		}
	}
		public void Attack()
		{
			AttackCounter = 0;
		}
}