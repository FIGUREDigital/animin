using UnityEngine;
using System.Collections;

public class InputSecretCode : MonoBehaviour 
{
	UIInput mInput;

	void OnClick()
	{
		if (!ProfilesManagementScript.Singleton.LoginCheckingDialogue.activeInHierarchy) {
						mInput = gameObject.transform.parent.GetComponentInChildren<UIInput> ();
						string accessCode = NGUIText.StripSymbols (mInput.value);

						ProfilesManagementScript.Singleton.CheckProfileLoginPasscode (accessCode);
						ProfilesManagementScript.Singleton.LoginCheckingDialogue.SetActive (true);
						ProfilesManagementScript.Singleton.NoSuchUserCodeDialogue.SetActive(false);
				}

	}
}
