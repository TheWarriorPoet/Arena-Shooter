using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
	public GameObject player;
	public GameObject spawnManager;
	public GameObject health;
	public GameObject ammo;
	public GameObject[] life;
	public GameObject wave;

	private PlayerProto p;
	private EnemyManager em;
	private RectTransform hRect;
	private RectTransform aRect;
	private Text wText;
	private float hWidth;
	private float aWidth;

	// Use this for initialization
	void Start()
	{
		p = player.GetComponent<PlayerProto>();
		em = spawnManager.GetComponent<EnemyManager>();
		hRect = health.GetComponent<RectTransform>();
		aRect = ammo.GetComponent<RectTransform>();
		wText = wave.GetComponent<Text>();
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

		// Lives
		for (int i = 0; i < p.maxLives; ++i)
		{
			if (p.lives >= i + 1)
			{
				life[i].SetActive(true);
			}
			else
			{
				life[i].SetActive(false);
			}
		}

		// Wave
		wText.text = "WAVE\n" + em.CurrentWave.ToString();
	}
}