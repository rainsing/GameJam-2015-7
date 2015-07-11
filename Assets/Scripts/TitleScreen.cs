using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour 
{
	public Image bgImage;
	public Sprite introTexture;

	private enum MyScreen
	{
		SCREEN_TITLE,
		SCREEN_INTRO
	}

	private MyScreen m_CurrentScreen = MyScreen.SCREEN_TITLE;

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			switch (m_CurrentScreen)
			{
			case MyScreen.SCREEN_TITLE:
				bgImage.sprite = introTexture;
				m_CurrentScreen = MyScreen.SCREEN_INTRO;
				break;
			
			case MyScreen.SCREEN_INTRO:
				Debug.Log("Load the main level now!");
				break;
			}
		}
	}
}
