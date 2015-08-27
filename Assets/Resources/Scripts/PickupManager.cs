using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
	public GameObject prefab;
	public float spawnTime;

	private GameObject spawnedObject;
	private float timer;

	void Start()
	{
		timer = spawnTime;
	}
	
	void Update()
	{
		if (spawnedObject == null)
		{
			timer += Time.deltaTime;

			if (timer >= spawnTime)
			{
				spawnedObject = (GameObject)Instantiate(prefab, transform.position, transform.rotation);

				timer = 0;
			}
		}
	}
}