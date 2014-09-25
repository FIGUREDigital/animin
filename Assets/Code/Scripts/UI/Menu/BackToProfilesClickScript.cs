using UnityEngine;
using System.Collections;

public class BackToProfilesClickScript : MonoBehaviour 
{
	public GameObject ProfilesScreen;
	public GameObject AniminScreen;

	void OnClick()
	{
		AniminScreen.SetActive(false);
		ProfilesScreen.SetActive(true);
	}
}
