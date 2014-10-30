using UnityEngine;
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

	private UIInput _inputField1;
	private UIInput _inputField2;

    private void OnClick()
    {
//        if( Account.Instance.UniqueID != null )
//        {
//            StartCoroutine( Account.Instance.WWWSendData( Account.Instance.UserName, " ", "empty", LastNameInput.GetComponent<UIInput>().value ) );
//        }        

		if (AddressContainer.activeInHierarchy) {
			AddressContainer.SetActive(false);
			NameContainer.SetActive(true);

			_inputField1 = FirstNameInput.transform.parent.GetComponentInChildren<UIInput> ();
			_inputField2 = LastNameInput.transform.parent.GetComponentInChildren<UIInput> ();
			Account.Instance.FirstName = NGUIText.StripSymbols (_inputField1.value);
			Account.Instance.LastName = NGUIText.StripSymbols (_inputField2.value);
				}

		if (NameContainer.activeInHierarchy)
		{
			NameContainer.SetActive(false);
			AddressContainer.SetActive(false);

			_inputField1 = AddressInput.transform.parent.GetComponentInChildren<UIInput> ();
			_inputField2 = AddresseeInput.transform.parent.GetComponentInChildren<UIInput> ();
			Account.Instance.Address = NGUIText.StripSymbols (_inputField1.value);
			Account.Instance.Addressee = NGUIText.StripSymbols (_inputField2.value);

			LaunchWebview();

		}

		//ProfilesManagementScript.Singleton.AniminsScreen.SetActive( false );
		//ProfilesManagementScript.Singleton.CloseWebview.SetActive( true );
//#if UNITY_IOS
//        int width = 360;
//        int height = 300;
//
//        if( Screen.width > 960 )
//        {
//            width *= 2;
//            height *= 2;
//        }
//
//        EtceteraBinding.inlineWebViewShow( 50, 10, width, height );
//		EtceteraBinding.inlineWebViewSetUrl( "http://terahard.org/Teratest/DatabaseAndScripts/AniminCart.html" );
//#endif

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