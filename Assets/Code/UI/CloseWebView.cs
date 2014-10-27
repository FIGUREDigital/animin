using UnityEngine;
using System.Collections;

public class CloseWebView : MonoBehaviour 
{
	void OnClick()
	{
	    Debug.Log( "Closing webview" );
        
#if UNITY_IOS
      EtceteraBinding.inlineWebViewClose();  
#endif
	}
}
