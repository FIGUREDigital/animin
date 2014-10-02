using UnityEngine;
using System.Collections;

public class TrackMusicOnTickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerProfileData.ActiveProfile.Settings.AudioEnabled)
			this.GetComponent<UISprite>().spriteName = "pauseScreenSound";
		else
			this.GetComponent<UISprite>().spriteName = "soundOff";
	}

	void OnClick()
	{
		PlayerProfileData.ActiveProfile.Settings.AudioEnabled = !PlayerProfileData.ActiveProfile.Settings.AudioEnabled;

	}
}
