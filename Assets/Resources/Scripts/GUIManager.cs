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
	private float maxHP;

	// Use this for initialization
	void Start()
	{
		p = player.GetComponent<PlayerProto>();
		hRect = health.GetComponent<RectTransform>();
		aRect = ammo.GetComponent<RectTransform>();
		hWidth = hRect.sizeDelta.x;
		aWidth = aRect.sizeDelta.x;

		maxHP = p.HP;
	}
	
	// Update is called once per frame
	void Update()
	{
		Vector2 newSize = hRect.sizeDelta;

		newSize.x = hWidth * p.HP / maxHP;

		hRect.sizeDelta = newSize;
	}
}