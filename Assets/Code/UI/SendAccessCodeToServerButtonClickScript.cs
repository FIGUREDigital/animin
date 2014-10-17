using UnityEngine;
using System.Collections;

public class SendAccessCodeToServerButtonClickScript : MonoBehaviour {

	public SendAccesCodeFromChatboxClickScript SubmitFunction;


	// Use this for initialization
	void Start () 
	{
		RegisterListeners();
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
		ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
	}

#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
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
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseFailedEvent += purchaseUnsuccessful;
	}
	void UnregisterListeners()
	{
		if(Application.isEditor){ return; }
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseFailedEvent -= purchaseUnsuccessful;
	}
}
