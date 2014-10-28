using UnityEngine;
using System.Collections;

public class BuyWithWebView : MonoBehaviour
{
    private void OnClick()
    {
        Debug.Log( "Buying with webview" );

#if UNITY_IOS
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive( false );
		ProfilesManagementScript.Singleton.CloseWebview.SetActive( true );
        int width = 260;
        int height = 300;

        if( Screen.width > 960 )
        {
            width *= 2;
            height *= 2;
        }

        EtceteraBinding.inlineWebViewShow( 50, 10, width, height );
        EtceteraBinding.inlineWebViewSetUrl( "http://animin.me/shop" );
#endif

        UnlockCharacterManager.Instance.BuyCharacter( ProfilesManagementScript.Singleton.AniminToUnlockId, false );

        if( Application.isEditor )
        {
            ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive( false );
            ProfilesManagementScript.Singleton.AniminsScreen.SetActive( true );
            return;
        }
        //ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
        //ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
    }
}