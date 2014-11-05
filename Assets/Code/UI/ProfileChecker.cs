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
		SaveAndLoad.Instance.LoadAllData ();
		if(initialStartup)
		{
			PlayerPrefs.SetString("First Login", "true");
			User.SetActive(false);
		}
		else
		{
            Debug.Log(ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count);
            User.SetActive(true);
            Debug.Log(ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count);
            if (ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count > 0)
            {
                User.GetComponentInChildren<UILabel>().text = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[0].ProfileName;
				Debug.Log("hi");
            }
            else
            {
                User.SetActive(false);
            }
		}
	}
}
