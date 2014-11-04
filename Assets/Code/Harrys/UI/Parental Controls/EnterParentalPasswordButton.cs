using UnityEngine;
using System.Collections;

public class EnterParentalPasswordButton : MonoBehaviour {

	UIInput mInput;

	[SerializeField]
	private UILabel ErrorBox;

	void OnClick(){
		mInput = gameObject.transform.parent.GetComponentInChildren<UIInput>();
		if (NGUIText.StripSymbols (mInput.value) == PlayerPrefs.GetString ("ParentalPassword")) {
			GetComponentInParent<ParentalGateway> ().Pass();
		} else {
			GetComponentInParent<ParentalGateway> ().Fail ();
		}
	}
}
