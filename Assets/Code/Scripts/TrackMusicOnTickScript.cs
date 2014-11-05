using UnityEngine;
using System.Collections;

public class TrackMusicOnTickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(ProfilesManagementScript.Singleton.CurrentProfile.Settings.AudioEnabled)
			this.GetComponent<UISprite>().spriteName = "pauseScreenSound";
		else
			this.GetComponent<UISprite>().spriteName = "soundOff";
	}

	void OnClick()
	{
        ProfilesManagementScript.Singleton.CurrentProfile.Settings.AudioEnabled = !ProfilesManagementScript.Singleton.CurrentProfile.Settings.AudioEnabled;

	}
}
