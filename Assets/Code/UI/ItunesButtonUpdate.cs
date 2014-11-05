using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItunesButtonUpdate : MonoBehaviour 
{
	[SerializeField]
	private GameObject mPiHeader;
	[SerializeField]
	private GameObject mTboHeader;
	[SerializeField]
	private GameObject mKelseyHeader;
	[SerializeField]
	private GameObject mMandiHeader;

	private int mId;
	private bool mRegistered;

	void Start()
	{
	}

	void OnEnable()
	{
		RegisterListeners();
		SetCharacterIcons();
	}
	
	void OnDisable()
	{
		//UnregisterListeners();
	}

	void SetCharacterIcons()
	{
		mPiHeader.SetActive(false);
		mTboHeader.SetActive(false);
		mKelseyHeader.SetActive(false);
		mMandiHeader.SetActive(false);

		int code = (int)ProfilesManagementScript.Singleton.AniminToUnlockId;
		switch(code)
		{
		case 0:
			mPiHeader.SetActive(true);
			break;
		case 1:
			mTboHeader.SetActive(true);
			break;
		case 2:
			mKelseyHeader.SetActive(true);
			break;
		case 3:
			mMandiHeader.SetActive(true);
			break;
		default:
			break;

		}
		mId = (int)ProfilesManagementScript.Singleton.AniminToUnlockId;
	}


	void ReturnToMainScreen()
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}

	void GoToAddress()
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AddressInput.SetActive(true);
	}

#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		Debug.Log(string.Format("Purchase of {0} Successful",transaction.productIdentifier));
		UnlockCharacterManager.Instance.UnlockCharacter();
		GoToAddress();
	}

	void purchaseCancelled( string response )
	{
		Debug.Log("Purchase Cancelled. Response "+ response);
		ShopManager.Instance.EndStore();
		ReturnToMainScreen();
	}

#elif UNITY_ANDROID
    void purchaseSuccessful( GooglePurchase transaction)
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
	void purchaseCancelled( string response )
	{
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		UnregisterListeners();
	}
#endif

    void purchaseUnsuccessful( string response )
	{
		Debug.Log("Purchase Unsuccessful, response: " + response);
		ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		ShopManager.Instance.EndStore();
		UnregisterListeners();
	}
	void RegisterListeners()
	{
		if(mRegistered)
		{
			return;
		}
		mRegistered = true;
		Debug.Log("Register Itunes Listeners");
		if(Application.isEditor){ return; }
        
#if UNITY_IOS
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
		StoreKitManager.purchaseFailedEvent += purchaseUnsuccessful;
#elif UNITY_ANDROID
        GoogleIABManager.purchaseSucceededEvent += purchaseSuccessful;
		GoogleIABManager.purchaseCancelledEvent += purchaseCancelled;
		GoogleIABManager.purchaseFailedEvent += purchaseUnsuccessful;
#endif
    }
	void UnregisterListeners()
	{
		if(!mRegistered)
		{
			return;
		}
		mRegistered = false;
		Debug.Log("Unregister Itunes Listeners");
#if UNITY_IOS
		if(Application.isEditor){ return; }
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelled;
		StoreKitManager.purchaseFailedEvent -= purchaseUnsuccessful;
#elif UNITY_ANDROID
		if(Application.isEditor){ return; }
        GoogleIABManager.purchaseSucceededEvent -= purchaseSuccessful;
		GoogleIABManager.purchaseCancelledEvent -= purchaseCancelled;
		GoogleIABManager.purchaseFailedEvent -= purchaseUnsuccessful;
#endif
    }
}
