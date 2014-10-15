using UnityEngine;
using System.Collections;

public class UnlockCharacterButtonClickScript : MonoBehaviour 
{

	private const string PI_UNLOCK = "com.apples.animin.characterunlock1";
	private const string KELSEY_UNLOCK = "com.apples.animin.characterunlock2";
	private const string MANDI_UNLOCK = "com.apples.animin.characterunlock3";
	private const string PI_PURCHASE = "com.apples.animin.characterpurchase1";
	private const string KELSEY_PURCHASE = "com.apples.animin.characterpurchase2";
	private const string MANDI_PURCHASE = "com.apples.animin.characterpurchase3";
	public AniminId Id;

	void OnClick()
	{
		ProfilesManagementScript.Singleton.AniminToUnlockId = Id;
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(false);
		ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(true);
		UnlockCharacterManager.Instance.OpenShop();
	}

}
