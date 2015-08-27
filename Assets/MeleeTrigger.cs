using UnityEngine;
using System.Collections;

public class MeleeTrigger : MonoBehaviour {
	
	public float Damage;
	public float AttackTime;
	private float AttackCounter;
	private bool isAttack = false;
	private bool canAttack = true;
	private float tempRotat = 0;
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
		if (tempRotat > 150) {
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.Rotate (new Vector3(0,-75,0));
			isAttack = false;
			canAttack = true;
		}
		if (isAttack) {
			gameObject.transform.Rotate(new Vector3(0,1000*Time.deltaTime,0));
			tempRotat += 1000*Time.deltaTime;
			//AttackCounter += 100*Time.deltaTime;
			gameObject.transform.GetChild(1).GetComponent<MeshRenderer> ().enabled = true;
			gameObject.GetComponent<BoxCollider> ().enabled = true;
		} else {
			gameObject.transform.GetChild(1).GetComponent<MeshRenderer> ().enabled = false;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
			canAttack = true;
			tempRotat = 0;
		}


	}
	
	void OnTriggerEnter(Collider other)
	{
		
		if (!(other.gameObject.tag == "Player")) 
		{
			if(other.gameObject.tag == "Enemy")
			{
				other.gameObject.GetComponent<Agent>().HP -= Damage;
				other.gameObject.GetComponent<NavMeshAgent>().velocity = ((other.gameObject.transform.position - gameObject.transform.position) * 10);
				print ("Applying Force");
			}else if(other.gameObject.tag == "Bomb")
            {
                Debug.Log("Melee Collider has hit Bomb");
                other.gameObject.GetComponent<Rigidbody>().velocity = ((other.gameObject.transform.position - gameObject.transform.position) * 10);
            }
			//Destroy (gameObject);
		}
	}
		public void Attack()
		{
			if (canAttack) {
				isAttack = true;
			}
		}
}