using UnityEngine;
using System.Collections;

public class CloseWebView : MonoBehaviour 
{
	void OnClick()
	{
	    Debug.Log( "Closing webview" );
		Debug.Log ("Current Scene Name : ["+Application.loadedLevelName+"];");

		if (Application.loadedLevelName == "Menu") {
			ProfilesManagementScript.Singleton.CloseWebview.SetActive (false);
			ProfilesManagementScript.Singleton.AniminsScreen.SetActive (true);
		} else if (Application.loadedLevelName == "VuforiaTest") {
			transform.parent.gameObject.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
		}
#if UNITY_IOS
      EtceteraBinding.inlineWebViewClose();  
#endif
	}
}
