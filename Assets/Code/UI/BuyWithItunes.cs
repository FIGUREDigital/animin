﻿using UnityEngine;
using System.Collections;

public class BuyWithItunes : MonoBehaviour 
{
	void OnClick()
	{
		Debug.Log("Buying with itunes \n Opening IAP");
		UnlockCharacterManager.Instance.BuyCharacter(ProfilesManagementScript.Singleton.AniminToUnlockId, false);
	}
}
