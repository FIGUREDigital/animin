using UnityEngine;
using System.Collections;

public class SendAddress : MonoBehaviour 
{
	void OnClick()
	{
		ProfilesManagementScript.Singleton.AddressInput.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
