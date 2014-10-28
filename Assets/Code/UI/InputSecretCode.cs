using UnityEngine;
using System.Collections;

public class InputSecretCode : MonoBehaviour 
{
	void OnClick()
	{
		ProfilesManagementScript.Singleton.LoginUser.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
