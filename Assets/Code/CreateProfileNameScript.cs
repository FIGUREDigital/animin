using UnityEngine;
using System.Collections;

public class CreateProfileNameScript : MonoBehaviour 
{
	string playerName;
	TouchScreenKeyboard keyboard;
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
	
	}

	public void ShowKeyboard()
	{
		keyboard = TouchScreenKeyboard.Open(playerName, TouchScreenKeyboardType.Default, false, false, false, false, "Enter Name");
	}
}


