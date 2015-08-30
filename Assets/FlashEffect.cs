using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
	public float flashRate = 1;
	public float minAlpha = 0, maxAlpha = 1;

	private MeshRenderer renderer;
	private Text text;
	private bool flash;

	void Start()
	{
		renderer = GetComponent<MeshRenderer>();
		text = GetComponent<Text>();

		
	}
	
	void Update()
	{
		Color c = Color.white;

		if (renderer != null)
		{
			c = renderer.material.color;
		}
		else if (text != null)
		{
			c = text.color;
		}

		if (flash)
		{
			c.a += flashRate * Time.deltaTime;
		}
		else
		{
			c.a -= flashRate * Time.deltaTime;
		}

		if (c.a <= 0)
		{
			flash = true;
		}
		else if (c.a >= maxAlpha)
		{
			flash = false;
		}

		if (renderer != null)
		{
			renderer.material.color = c;
		}
		else if (text != null)
		{
			text.color = c;
		}
	}
}