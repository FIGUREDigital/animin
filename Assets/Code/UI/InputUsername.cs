using UnityEngine;
using System.Collections;

public class InputUsername : MonoBehaviour {

	UIInput mInput;
	void OnClick()
	{
		mInput = gameObject.transform.parent.GetComponentInChildren<UIInput>();
		string name =  NGUIText.StripSymbols(mInput.value);
		PlayerPrefs.SetString("Username", name);
	    StartCoroutine( Account.Instance.WWWSendData( name ) );
		ProfilesManagementScript.Singleton.NewUser.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
