using UnityEngine;
using System.Collections;

public class SendAccessCodeToServerButtonClickScript : MonoBehaviour {

	public SendAccesCodeFromChatboxClickScript SubmitFunction;


	// Use this for initialization
	void Start () 
	{
		//RegisterListeners();
	}
	void OnClick()
	{
		SubmitFunction.OnSubmit();
		if(Application.isEditor)
		{ 
			ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
			ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
			return; 
		}
		else
		{
			ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
			ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
		}
	}

#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
#elif UNITY_ANDROID
    void purchaseSuccessful(GooglePurchase transaction)
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
#endif
	void purchaseUnsuccessful( string transaction )
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
	void RegisterListeners()
	{
		if(Application.isEditor){ return; }
		#if UNITY_IOS
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseUnsuccessful;
		StoreKitManager.purchaseFailedEvent += purchaseUnsuccessful;
		#elif UNITY_ANDROID
        GoogleIABManager.purchaseSucceededEvent += purchaseSuccessful;
		GoogleIABManager.purchaseCancelledEvent += purchaseUnsuccessful;
        GoogleIABManager.purchaseFailedEvent += purchaseUnsuccessful;
		#endif
	}
	void UnregisterListeners()
	{
		if(Application.isEditor){ return; }
		#if UNITY_IOS
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent -= purchaseUnsuccessful;
		StoreKitManager.purchaseFailedEvent -= purchaseUnsuccessful;
		#elif UNITY_ANDROID
        GoogleIABManager.purchaseSucceededEvent -= purchaseSuccessful;
		GoogleIABManager.purchaseCancelledEvent -= purchaseUnsuccessful;
        GoogleIABManager.purchaseFailedEvent -= purchaseUnsuccessful;
		#endif
	}
}
