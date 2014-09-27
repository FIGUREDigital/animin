using UnityEngine;
using System.Collections;

public class CreateProfileNameScript : MonoBehaviour 
{
	string playerName;
#if UNITY_ANDROID || UNITY_IPHONE
	TouchScreenKeyboard keyboard;
#endif
	public UILabel PlayerNameLabel;

	// Use this for initialization
	void Start () 
	{

	}

	void OnClick()
	{
		ShowKeyboard();
	}
	
	// Update is called once per frame
	void Update () 
	{
#if UNITY_ANDROID || UNITY_IPHONE
		if(keyboard != null)
		{
			playerName = keyboard.text;

			if(keyboard.done)
			{
				// accept
				PlayerNameLabel.text = playerName;
			}
			else if(keyboard.wasCanceled || !keyboard.active)
			{
				// cancel operation
				//PlayerNameLabel.text = "Cancelled";
			}

		}
#endif
	
	}

	public void ShowKeyboard()
	{
#if UNITY_ANDROID || UNITY_IPHONE
		keyboard = TouchScreenKeyboard.Open(playerName, TouchScreenKeyboardType.Default, false, false, false, false, "Enter Name");
#endif
	}
}


