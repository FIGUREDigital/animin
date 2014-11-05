using UnityEngine;
using System.Collections;

public class ShowForm : MonoBehaviour
{
    public GameObject Parent;
    private GameObject PurchaseAniminViaPaypal;

    public void Start()
    {
        PurchaseAniminViaPaypal = (GameObject)GameObject.Instantiate (Resources.Load("NGUIPrefabs/UI - PaypalCharacterSelect"));
        PurchaseAniminViaPaypal.SetActive( false );
    }

    private void OnClick()
    {
//        Debug.Log( "Buying with webview" );

        UIGlobalVariablesScript.Singleton.OpenParentalGateway( Parent, PurchaseAniminViaPaypal );
//		LaunchWebview ();

    }
	
}