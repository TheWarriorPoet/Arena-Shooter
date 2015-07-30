using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public GameObject player;
	public GameObject health;
	public GameObject ammo;
	public GameObject[] life;

	private PlayerProto p;
	private RectTransform hRect;
	private RectTransform aRect;
	private float hWidth;
	private float aWidth;

	// Use this for initialization
	void Start()
	{
		p = player.GetComponent<PlayerProto>();
		hRect = health.GetComponent<RectTransform>();
		aRect = ammo.GetComponent<RectTransform>();
		hWidth = hRect.sizeDelta.x;
		aWidth = aRect.sizeDelta.x;

		p.maxHP = p.HP;
		p.maxAmmo = p.ammo;
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector2 newSize;

		// Health
		newSize = hRect.sizeDelta;

		newSize.x = hWidth * p.HP / p.maxHP;

		hRect.sizeDelta = newSize;

		// Ammo
		newSize = aRect.sizeDelta;
		
		newSize.x = aWidth * p.ammo / p.maxAmmo;
		
		aRect.sizeDelta = newSize;
	}
}