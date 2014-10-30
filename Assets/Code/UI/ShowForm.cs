using UnityEngine;
using System.Collections;

public class ShowForm : MonoBehaviour
{
    public GameObject Parent;
    public GameObject WebviewEnabler;

    public void Start()
    {
		WebviewEnabler.SetActive( false );
    }

    private void OnClick()
    {
//        Debug.Log( "Buying with webview" );

		UIGlobalVariablesScript.Singleton.OpenParentalGateway( Parent, WebviewEnabler );
//		LaunchWebview ();

    }
	
}