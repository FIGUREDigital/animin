using UnityEngine;
using System.Collections;

public class ShowForm : MonoBehaviour
{ 
    public GameObject Form;

    public void Start()
    {
        Form.SetActive( false );
    }

    private void OnClick()
    {
        Debug.Log( "Buying with webview" );

        Form.SetActive( true );
        UIGlobalVariablesScript.Singleton.OpenParentalGateway( gameObject, Form );
    }

}