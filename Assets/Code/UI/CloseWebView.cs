using UnityEngine;
using System.Collections;

public class CloseWebView : MonoBehaviour 
{
	void OnClick()
	{
	    Debug.Log( "Closing webview" );
        
#if UNITY_IOS
		ProfilesManagementScript.Singleton.CloseWebview.SetActive( false );
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive( true );
      EtceteraBinding.inlineWebViewClose();  
#endif
	}
}
