using UnityEngine;
using System.Collections;

public class OpenInGamePurchaseView : MonoBehaviour
{
    public GameObject Parent;
    public GameObject Form;
    public GameObject ExitWebview;

    private void Start()
    {
        Parent.SetActive( false );
        Form.SetActive( false );
        ExitWebview.SetActive( false );
    }

	void OnClick()
	{
	    Debug.Log( "opening" + name );
        Parent.SetActive( true );
		
		UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive (false);
	}
}
