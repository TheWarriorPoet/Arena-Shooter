using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject target;
	public float velocityRate;

	private CharacterController cc;
	private Vector3 offset;

	// Use this for initialization
	void Start()
	{
		offset = transform.position - target.transform.position;

		cc = target.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector3 vel = cc.velocity;
		vel.y = 0;

		transform.position = target.transform.position + offset - (vel * velocityRate);
	}
}