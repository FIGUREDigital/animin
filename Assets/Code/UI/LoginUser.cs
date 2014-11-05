using UnityEngine;
using System.Collections;

public class LoginUser : MonoBehaviour 
{
	void OnClick()
	{
        ProfilesManagementScript.Singleton.LoginExistingUser();

        ProfilesManagementScript.Singleton.SelectProfile.SetActive(false);
        ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);

	}
}
