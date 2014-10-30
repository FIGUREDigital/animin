using UnityEngine;
using System.Collections;

public class SendAddress : MonoBehaviour 
{
	void OnClick()
	{

		AddressScreen screen = transform.parent.GetComponent<AddressScreen>();
		screen.Send();
		ProfilesManagementScript.Singleton.AddressInput.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
