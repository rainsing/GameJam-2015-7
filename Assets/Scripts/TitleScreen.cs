using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScreen : MonoBehaviour 
{
	public Image bgImage;
	public Sprite titleTexture;
	public Sprite introTexture;

	private enum MyScreen
	{
		SCREEN_LOGO,
		SCREEN_TITLE
	}

	private MyScreen m_CurrentScreen = MyScreen.SCREEN_LOGO;

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			switch (m_CurrentScreen)
			{
			case MyScreen.SCREEN_LOGO:
				bgImage.sprite = titleTexture;
				m_CurrentScreen = MyScreen.SCREEN_TITLE;
				break;

			case MyScreen.SCREEN_TITLE:
				Application.LoadLevel("wall");
				break;
			}
		}
	}
}
