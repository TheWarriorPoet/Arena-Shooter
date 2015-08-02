using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject target;
	public float velocityRate;

	private CharacterController cc;
	private Vector3 offset;


	public Transform Target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = new Vector3(0f,0f,0f);





	// Use this for initialization
	void Start()
	{
		offset = transform.position - target.transform.position;

		cc = target.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		/*Vector3 vel = cc.velocity;
		vel.y = 0;

		transform.position = target.transform.position + offset - (vel * velocityRate);*/


		Vector3 targetPosition = cc.transform.position + offset;
		Vector3 newPosition = Vector3.SmoothDamp (transform.position,targetPosition, ref velocity,smoothTime);
		transform.position = newPosition;
		


	}
}