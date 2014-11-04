using UnityEngine;
using System.Collections;

public class ProfileChecker : MonoBehaviour 
{
	GameObject Login;
	GameObject User;
	GameObject NewUser;

	bool initialStartup;
	void Start()
	{
		Login = transform.FindChild("Login").gameObject;
		User = transform.FindChild("User").gameObject;
		NewUser = transform.FindChild("NewUser").gameObject;

		initialStartup = PlayerPrefs.GetString("First Login") != "true";
		if(initialStartup)
		{
			PlayerPrefs.SetString("First Login", "true");
			User.SetActive(false);
		}
		else
		{
			User.SetActive(true);			
            if (ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count > 0)
            {
                User.GetComponentInChildren<UILabel>().text = "Working "+ProfilesManagementScript.Singleton.ListOfPlayerProfiles[0].ProfileName+"";
            }
            else
            {
                User.GetComponentInChildren<UILabel>().text = "Default Dave";
            }
		}
	}
}
