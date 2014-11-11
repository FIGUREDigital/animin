using UnityEngine;
using System.Collections;

public class SendAddress : MonoBehaviour 
{
	AddressScreen screen;
	void OnClick()
	{
		screen = transform.parent.GetComponent<AddressScreen>();
		if(!Application.isEditor)
		{
			StartCoroutine("SendEmail");
		}
		ProfilesManagementScript.Singleton.AddressInput.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}

	private IEnumerator SendEmail()
	{
		screen.Send();
		return null;
	}
}
