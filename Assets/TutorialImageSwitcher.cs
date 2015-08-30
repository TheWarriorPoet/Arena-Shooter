using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialImageSwitcher : MonoBehaviour {
	public Sprite[] Pages;
	public Image DisplayImage;
	public int PageIndex = 0;
	public string LevelName;

	// Use this for initialization
	void Start () {
		DisplayImage.sprite = Pages [0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			Application.LoadLevel(LevelName);
		}
	}

	public void Nextimage(){

		if (PageIndex + 1 < Pages.Length) {
			PageIndex = PageIndex + 1;
			DisplayImage.sprite = Pages[PageIndex];

		} else {
			Application.LoadLevel (LevelName);
		}


		//PageIndex = PageIndex + 1;
		//Pages.Length

	}
}
