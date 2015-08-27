using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
	public GameObject prefab;
	public GameObject spawnPoint;
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
				spawnedObject = (GameObject)Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

				timer = 0;
			}
		}
	}
}