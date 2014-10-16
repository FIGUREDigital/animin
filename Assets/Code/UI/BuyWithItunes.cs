using UnityEngine;
using System.Collections;

public class BuyWithItunes : MonoBehaviour 
{
	void Start()
	{
		RegisterListeners();
	}
	void OnClick()
	{
		Debug.Log("Buying with itunes \n Opening IAP");
		UnlockCharacterManager.Instance.BuyCharacter(ProfilesManagementScript.Singleton.AniminToUnlockId, false);
		if(Application.isEditor)
		{
			ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
			ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
			return;
		}
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
		ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
	}

	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
	void purchaseUnsuccessful( string response )
	{
		Debug.Log("Purchase Unsuccessful, response: " + response);
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
