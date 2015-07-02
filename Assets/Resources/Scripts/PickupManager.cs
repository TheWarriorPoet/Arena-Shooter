using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
	public enum PType
	{
		AMMO,
		HEALTH
	}

	private PlayerProto player;

	public int value;
	public PType type;
	public float rotateSpeed;

	// Use this for initialization
	void Start()
	{
		GameObject p = GameObject.FindGameObjectWithTag("Player");

		player = p.GetComponent<PlayerProto>();
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			switch (type)
			{
			case PType.AMMO:
				player.ammo += value;
				break;
			case PType.HEALTH:
				player.HP += value;
				break;
			}

			this.gameObject.SetActive(false);
		}
	}
}