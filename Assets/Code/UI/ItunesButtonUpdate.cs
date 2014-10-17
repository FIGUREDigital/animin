using UnityEngine;
using System.Collections;

public class ItunesButtonUpdate : MonoBehaviour 
{
	[SerializeField]
	private UISprite mCharacterIcon;
	[SerializeField]
	private UILabel mCharacterTitle;
	[SerializeField]
	private UISprite[] mIcons;
	[SerializeField]
	private GameObject mPiHeader;
	[SerializeField]
	private GameObject mTboHeader;
	[SerializeField]
	private GameObject mKelseyHeader;
	[SerializeField]
	private GameObject mMandiHeader;



	private Color piColourLight = new Color(206,255,82);
	private Color piColourDark = new Color(42,196,10);
	private Color tboColourLight = new Color(201,224,255);
	private Color tboColourDark = new Color(123,196,244);
	private Color kelseyColourLight = new Color(255,170,239);
	private Color kelseyColourDark = new Color(67,57,123);
	private Color mandiColourLight = new Color(255,255,255);
	private Color mandiColourDark = new Color(252,251,224);

	private string mName;
	private int mId;

	void Start()
	{
		RegisterListeners();
		mCharacterTitle = new UILabel();
	}

	void OnEnable()
	{
		mCharacterTitle = new UILabel();
		mCharacterIcon = new UISprite();
		SetCharacterIcons();
		mCharacterIcon.gameObject.SetActive(true);
		mName = mCharacterIcon.name;

		switch(mId)
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
	}
	void OnDisable()
	{
		mPiHeader.SetActive(false);
		mTboHeader.SetActive(false);
		mKelseyHeader.SetActive(false);
		mMandiHeader.SetActive(false);
		mCharacterIcon.gameObject.SetActive(false);
	}

	void SetCharacterIcons()
	{
		int code = (int)ProfilesManagementScript.Singleton.AniminToUnlockId;
		switch(code)
		{
		case 0:
			mName = "Pi";
			break;
		case 1:
			mName = "Tbo";
			break;
		case 2:
			mName = "Kelsey";
			break;
		case 3:
			mName = "Mandi";
			break;
		default:
			break;

		}
		mId = (int)ProfilesManagementScript.Singleton.AniminToUnlockId;
		mCharacterIcon = mIcons[mId];
		mCharacterTitle.text = mName;
	}

#if UNITY_IOS
	void purchaseSuccessful( StoreKitTransaction transaction )
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
		StoreKitManager.purchaseFailedEvent += purchaseUnsuccessful;
#elif UNITY_ANDROID
        GoogleIABManager.purchaseSucceededEvent += purchaseSuccessful;
		GoogleIABManager.purchaseFailedEvent += purchaseUnsuccessful;
#endif
    }
	void UnregisterListeners()
	{
#if UNITY_IOS
		if(Application.isEditor){ return; }
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseFailedEvent -= purchaseUnsuccessful;
#elif UNITY_ANDROID
		if(Application.isEditor){ return; }
        GoogleIABManager.purchaseSucceededEvent -= purchaseSuccessful;
		GoogleIABManager.purchaseFailedEvent -= purchaseUnsuccessful;
#endif
    }
}
