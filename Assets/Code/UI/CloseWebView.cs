using UnityEngine;
using System.Collections;

public class CloseWebView : MonoBehaviour 
{
	void OnClick()
	{
	    Debug.Log( "Closing webview" );
        

		ProfilesManagementScript.Singleton.CloseWebview.SetActive( false );
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive( true );
#if UNITY_IOS
      EtceteraBinding.inlineWebViewClose();  
#endif
	}
}
