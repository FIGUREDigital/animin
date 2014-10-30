using UnityEngine;
using System.Collections;

public class ShowForm : MonoBehaviour
{
    public GameObject Parent;
    public GameObject Form;
	public GameObject ExitWebViewObject;

    public void Start()
    {
        Form.SetActive( false );
    }

    private void OnClick()
    {
//        Debug.Log( "Buying with webview" );

//        UIGlobalVariablesScript.Singleton.OpenParentalGateway( Parent, Form );
		LaunchWebview ();

    }

	public void LaunchWebview()
	{
		Debug.Log( "Buying with webview" );
		StartCoroutine( Account.Instance.WWWSendData( Account.Instance.UserName, " ", Account.Instance.Address, Account.Instance.Addressee, Account.Instance.FirstName, Account.Instance.LastName ));
		ExitWebViewObject.SetActive( true );
		
		#if UNITY_IOS
		int width = 360;
		int height = 300;
		
		if( Screen.width > 960 )
		{
			width *= 2;
			height *= 2;
		}
		
		EtceteraBinding.inlineWebViewShow( 50, 10, width, height );
		EtceteraBinding.inlineWebViewSetUrl( "http://terahard.org/Teratest/DatabaseAndScripts/AniminCart.html" );
		#endif
		
	}
}