
using UnityEngine;
using System.Collections;

public class ConfirmParentalPasswordButton : MonoBehaviour {
	
	UIInput[] mInput;
	[SerializeField]
	UILabel ErrorBox;
	[SerializeField]
	GameObject ParentalControlsScreen;
	void OnClick()
	{
		mInput = gameObject.transform.parent.GetComponentsInChildren<UIInput>();
		
		if (NGUIText.StripSymbols (mInput [0].value) != NGUIText.StripSymbols (mInput [1].value)) {
			if (ErrorBox!=null){
				ErrorBox.gameObject.SetActive(true);
				ErrorBox.text = "Passwords do not match!";
			}
		} else if(NGUIText.StripSymbols (mInput [0].value) == "" && NGUIText.StripSymbols (mInput [1].value)=="") {
			if (ErrorBox!=null){
				ErrorBox.gameObject.SetActive(true);
				ErrorBox.text = "Please enter a password";
			}
		} else {
			Debug.Log ("Passwords Match!");
			string password = NGUIText.StripSymbols (mInput [0].value);
			PlayerPrefs.SetString ("ParentalPassword", password);
			Account.Instance.UserName = password;
			if (ErrorBox!=null)ErrorBox.gameObject.SetActive(false);
			if (ParentalControlsScreen!=null) ParentalControlsScreen.SetActive(true);
			this.transform.parent.gameObject.SetActive(false);
		}
	}
}
