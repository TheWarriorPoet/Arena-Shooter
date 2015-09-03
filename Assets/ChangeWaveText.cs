using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeWaveText : MonoBehaviour
{
	public float time;

	private float timer;
	private Text txt;

	void Start()
	{
		txt = GetComponent<Text>();
	}
	
	void Update()
	{
		if (txt.text == "WAVE INBOUND")
		{
			timer += Time.deltaTime;
		}

		if (timer >= time)
		{
			txt.text = "";
			timer = 0;
		}
	}
}