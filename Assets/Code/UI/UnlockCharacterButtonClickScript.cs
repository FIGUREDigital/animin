﻿using UnityEngine;
using System.Collections;

public class UnlockCharacterButtonClickScript : MonoBehaviour 
{

	private const string PI_UNLOCK = "com.apples.animin.characterunlock1";
	private const string KELSEY_UNLOCK = "com.apples.animin.characterunlock2";
	private const string MANDI_UNLOCK = "com.apples.animin.characterunlock3";
	private const string PI_PURCHASE = "com.apples.animin.characterpurchase1";
	private const string KELSEY_PURCHASE = "com.apples.animin.characterpurchase2";
	private const string MANDI_PURCHASE = "com.apples.animin.characterpurchase3";
	private string mBuyItem;
	public AniminId Id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		OpenShop ();
		SelectCharacter ();
		ShopManager.Instance.BuyItem (mBuyItem);
		StartCoroutine ("ClosePopup");
	}

	void SelectCharacter()
	{
		switch(Id)
		{
		case AniminId.Pi:
			mBuyItem = PI_UNLOCK;
			break;
		case AniminId.Kelsey:
			mBuyItem = KELSEY_UNLOCK;
			break;
		case AniminId.Mandi:
			mBuyItem = MANDI_UNLOCK;
			break;
		default:
			mBuyItem = PI_PURCHASE;
			break;

		}
	}
	void OpenShop()
	{
		string[] shopItems = new string[6];
		shopItems [0] = PI_UNLOCK;
		shopItems [1] = KELSEY_UNLOCK;
		shopItems [2] = MANDI_UNLOCK;
		shopItems [3] = PI_PURCHASE;
		shopItems [4] = KELSEY_PURCHASE;
		shopItems [5] = MANDI_PURCHASE;
		ShopManager.Instance.StartStore (shopItems);

	}
	void UnlockCharacter()
	{
		CharacterChoiceItem character = GameObject.Find(Id.ToString()).GetComponent<CharacterChoiceItem>();
		character.ChangeLockedState(true);
	}

	IEnumerator ClosePopup()
	{
		bool complete = false;
		while(!complete)
		{
			Debug.Log(ShopManager.CurrentPurchaseStatus.ToString());
			switch(ShopManager.CurrentPurchaseStatus)
			{
			case ShopManager.PurchaseStatus.Success:
				UnlockCharacter ();
				complete = true;
				break;
			case ShopManager.PurchaseStatus.Fail:
				Debug.Log("Purchase Failed!");
				complete = true;
				break;
			default:
					yield return new WaitForSeconds(0.2f);
				break;
			}
		}
		Debug.Log ("Finish Coroutine!");
		ProfilesManagementScript.Singleton.AniminToUnlockId = Id;
		
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(false);
		ProfilesManagementScript.Singleton.CreateAccessCodeScreen.SetActive(true);
		yield return true;
	}
}
