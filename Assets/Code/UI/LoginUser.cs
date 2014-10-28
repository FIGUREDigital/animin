using UnityEngine;
using System.Collections;

public class LoginUser : MonoBehaviour 
{
	void OnClick()
	{
		ProfilesManagementScript.Singleton.SelectProfile.SetActive(false);
		ProfilesManagementScript.Singleton.LoginUser.SetActive(true);
	}
}
