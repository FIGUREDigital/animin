using UnityEngine;
using System.Collections;

public class BackToProfilesClickScript : MonoBehaviour 
{
	public GameObject ProfilesScreen;
	public GameObject AniminScreen;

	void OnClick()
	{
		if (ProfilesScreen.activeInHierarchy){
			AniminScreen.SetActive(true);
			ProfilesScreen.SetActive(false);
		}
		else {
			AniminScreen.SetActive(false);
			ProfilesScreen.SetActive(true);
		}
	}
}
