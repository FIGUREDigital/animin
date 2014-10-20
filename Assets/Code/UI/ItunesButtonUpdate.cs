using UnityEngine;
using System.Collections;

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

	void Start()
	{
		RegisterListeners();
	}

	void OnEnable()
	{
		SetCharacterIcons();
		UnlockCharacterManager.Instance.OpenShop();
	}
	
	void OnDisable()
	{
		ShopManager.Instance.EndStore();
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


#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
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
		UnregisterListeners();
	}
	void RegisterListeners()
	{
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
