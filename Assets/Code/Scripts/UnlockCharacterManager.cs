using UnityEngine;
using System.Collections;

public class UnlockCharacterManager
{
	public const string PI_UNLOCK = "com.apples.animin.characterunlock1";
	public const string KELSEY_UNLOCK = "com.apples.animin.characterunlock2";
	public const string MANDI_UNLOCK = "com.apples.animin.characterunlock3";
	public const string PI_PURCHASE = "com.apples.animin.characterpurchase1";
	public const string KELSEY_PURCHASE = "com.apples.animin.characterpurchase2";
	public const string MANDI_PURCHASE = "com.apples.animin.characterpurchase3";
    public const string TBOADULT_PURCHASE = "com.apples.animin.characterpurchase4";

	private static string mBuyItem;
    private static PersistentData.TypesOfAnimin m_CurrentCharacterFocus;
	public PersistentData.TypesOfAnimin ID
	{
		get
		{
			return m_CurrentCharacterFocus;
		}
	}


	#region Singleton
	
	private static UnlockCharacterManager s_Instance;
	
	public static UnlockCharacterManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new UnlockCharacterManager();
			}
			return s_Instance;
		}
	}
	
	#endregion

    public void BuyCharacter(PersistentData.TypesOfAnimin type, bool free)
	{
        m_CurrentCharacterFocus = type;
        switch(m_CurrentCharacterFocus)
		{
            case PersistentData.TypesOfAnimin.Pi:
			mBuyItem = free ? PI_UNLOCK : PI_PURCHASE;
			break;
            case PersistentData.TypesOfAnimin.Tbo:
			Debug.LogError("TBO should be unlocked initially. What are you changing?");
			break;
            case PersistentData.TypesOfAnimin.Kelsey:
			mBuyItem = free ? KELSEY_UNLOCK : KELSEY_PURCHASE;
			break;
            case PersistentData.TypesOfAnimin.Mandi:
			mBuyItem = free ? MANDI_UNLOCK : MANDI_PURCHASE;
			break;
		default:
			Debug.LogError("No animin ID supplied");
			break;
			
		}
		ShopManager.Instance.BuyItem (mBuyItem);
	}

	public void OpenShop()
	{
		Debug.Log("Opening Shop");
		string[] shopItems = new string[6];
		shopItems [0] = PI_UNLOCK;
		shopItems [1] = KELSEY_UNLOCK;
		shopItems [2] = MANDI_UNLOCK;
		shopItems [3] = PI_PURCHASE;
		shopItems [4] = KELSEY_PURCHASE;
		shopItems [5] = MANDI_PURCHASE;
		ShopManager.Instance.StartStore (shopItems);
	}

	private IEnumerator WaitForResponse()
	{
		Debug.Log ("Start Coroutine!");
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
			case ShopManager.PurchaseStatus.Cancel:
				Debug.Log("Purchase Failed!");
				complete = true;
				break;
			default:
				yield return new WaitForSeconds(0.2f);
				break;
			}
		}
		if(ShopManager.CurrentPurchaseStatus == ShopManager.PurchaseStatus.Success)
		{
			ShopManager.CurrentPurchaseStatus = ShopManager.PurchaseStatus.Idle;
		}
		Debug.Log ("Finish Coroutine!");
		
		yield return true;
	}   

    public bool CheckCharacterPurchased(PersistentData.TypesOfAnimin Id)
	{
		string s1 = "";
		string s2 = "";
		switch(Id)
		{
            case PersistentData.TypesOfAnimin.Pi:
                s1 = PI_UNLOCK;
                s2 = PI_PURCHASE;
			break;
            case PersistentData.TypesOfAnimin.Kelsey:
                s1 = KELSEY_UNLOCK;
                s2 = KELSEY_PURCHASE;
			break;
            case PersistentData.TypesOfAnimin.Mandi:
                s1 = MANDI_UNLOCK;
                s2 = MANDI_PURCHASE;
			break;
			case PersistentData.TypesOfAnimin.TboAdult:
                s2 = TBOADULT_PURCHASE;
			break;
			
		default:
            case PersistentData.TypesOfAnimin.Tbo:
			break;
		}
		return ShopManager.Instance.HasBought(s1) || ShopManager.Instance.HasBought(s2);
	}

    public void UnlockCharacter()
	{  	

        switch(m_CurrentCharacterFocus)
		{
            case PersistentData.TypesOfAnimin.Pi:
            //PlayerPrefs.SetInt("piUnlocked", 1);
			break;
            case PersistentData.TypesOfAnimin.Kelsey:
            //PlayerPrefs.SetInt("kelseyUnlocked", 1);
			break;
            case PersistentData.TypesOfAnimin.Mandi:
            //PlayerPrefs.SetInt("mandiUnlocked", 1);
			break;
            case PersistentData.TypesOfAnimin.TboAdult:
            break;
		default:
			break;
		}	

        CharacterChoiceManager.Instance.UnlockCharacterPortrait(m_CurrentCharacterFocus);
        ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins.Add(m_CurrentCharacterFocus);
        SaveAndLoad.Instance.SaveAllData();
		Debug.Log("just saved...unlock");
        ShopManager.Instance.EndStore();        
	}

	public void CheckInitialCharacterUnlock()
	{
        Debug.Log("Unlock started...");
        for (int i = 0; i < ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins.Count; i++)
        {   
            PersistentData.TypesOfAnimin typeToUnlock = ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins[i];
            Debug.Log("Unlock Character " + typeToUnlock);
            CharacterChoiceManager.Instance.UnlockCharacterPortrait(typeToUnlock);
        }

	}

	void OnApplicationPause()
	{
		ShopManager.Instance.EndStore();
		PlayerPrefs.Save();
	}
	void OnApplicationResume()
	{
		OpenShop();
	}
}
