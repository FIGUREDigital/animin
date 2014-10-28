using UnityEngine;
using System.Collections;

public class OpenInGamePurchaseView : MonoBehaviour
{
    public GameObject Parent;
    private void Start()
    {
        Parent.SetActive( false );
    }

	void OnClick()
	{
	    Debug.Log( "opening" + name );
        Parent.SetActive( true );
	}
}
