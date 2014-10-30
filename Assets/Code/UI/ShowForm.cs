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

        UIGlobalVariablesScript.Singleton.OpenParentalGateway( Parent, Form );
//		LaunchWebview ();

    }

	public void LaunchWebview()
	
}