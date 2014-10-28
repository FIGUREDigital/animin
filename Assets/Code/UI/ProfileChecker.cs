using UnityEngine;
using System.Collections;

public class ProfileChecker : MonoBehaviour 
{
	bool initialStartup;
	void Start()
	{
		initialStartup = PlayerPrefs.GetString("First Login") == "true";
		if(initialStartup)
		{
			PlayerPrefs.SetString("First Login", "true");


		}
		else
		{
			this.gameObject.SetActive(false);
			ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		}
	}
}
