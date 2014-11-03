using UnityEngine;
using System.Collections;

public class InputUsername : MonoBehaviour {

	UIInput mInput;
	void OnClick()
	{
		mInput = gameObject.transform.parent.GetComponentInChildren<UIInput>();
		string name =  NGUIText.StripSymbols(mInput.value);
		PlayerPrefs.SetString("Username", name);
		Account.Instance.UserName = name;
	    StartCoroutine( Account.Instance.WWWSendData( true, name, "","","", "","" ) );

	}
}
