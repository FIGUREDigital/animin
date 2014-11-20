﻿using UnityEngine;
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
        if (SubmitFunction.gameObject.GetComponent<UIInput>().text == "18271425")
        {
            ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
            ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);

            UnlockCharacterManager.Instance.ID = PersistentData.TypesOfAnimin.TboAdult;
            UnlockCharacterManager.Instance.UnlockCharacter();
        }
        else
        {
            /*
            ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
            ProfilesManagementScript.Singleton.LoadingSpinner.SetActive(true);
            SubmitFunction.OnSubmit();
            RegisterListeners();
            */
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
        GoogleIABManager.purchaseFailedEvent -= purchaseUnsuccessful;
		#endif
	}
}
