﻿using UnityEngine;
using System.Collections;

public class BuyWithWebView : MonoBehaviour
{
    public GameObject ExitWebViewObject;
    public GameObject FirstNameInput;
    public GameObject LastNameInput;
	public GameObject AddressInput;
	public GameObject AddresseeInput;

	public GameObject AddressContainer;
	public GameObject NameContainer;

    private void OnClick()
    {
        if( Account.Instance.UniqueID != null )
        {
            StartCoroutine( Account.Instance.WWWSendData( Account.Instance.UserName, " ", "empty", LastNameInput.GetComponent<UIInput>().value ) );
        }

        Debug.Log( "Buying with webview" );
        ExitWebViewObject.SetActive( true );

		//ProfilesManagementScript.Singleton.AniminsScreen.SetActive( false );
		//ProfilesManagementScript.Singleton.CloseWebview.SetActive( true );
#if UNITY_IOS
        int width = 360;
        int height = 300;

        if( Screen.width > 960 )
        {
            width *= 2;
            height *= 2;
        }

        EtceteraBinding.inlineWebViewShow( 50, 10, width, height );
        EtceteraBinding.inlineWebViewSetUrl( "http://animin.me/shop" );
#endif

        //UnlockCharacterManager.Instance.BuyCharacter( ProfilesManagementScript.Singleton.AniminToUnlockId, false );

        if( Application.isEditor )
        {
            //ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive( false );
            //ProfilesManagementScript.Singleton.AniminsScreen.SetActive( true );
            return;
        }
        //ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
        //ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
    }
}