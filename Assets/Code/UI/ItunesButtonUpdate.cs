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
