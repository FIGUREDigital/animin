using UnityEngine;
using System.Collections;

public class CharacterChoiceItem : MonoBehaviour 
{
	[SerializeField]
	private GameObject mSprite;
	[SerializeField]
	private GameObject mDisabledSprite;
	[SerializeField]
	private GameObject mLockedButton;
	[SerializeField]
	private bool mUnlocked;
	[SerializeField]
	private AniminId mId;

	// Use this for initialization
	void Start () 
	{
		if(mSprite == null)
		{
			Debug.LogWarning("CANNOT FIND SPRITE FOR CHARACTER. ATTEMPTING TO RESOLVE.");
			mSprite = transform.FindChild("Sprite").gameObject;
			if(mSprite == null)
			{
				Debug.LogError("SPRITE NOT SET FOR CHARACTER");
			}
			else
			{
				Debug.Log("Sprite found!");
			}
		}

		if(mDisabledSprite == null)
		{
			Debug.LogWarning("CANNOT FIND DISABLED SPRITE FOR CHARACTER. ATTEMPTING TO RESOLVE.");
			mDisabledSprite = transform.FindChild("Disabled Sprite").gameObject;
			if(mDisabledSprite == null)
			{
				Debug.LogError("DISABLED SPRITE NOT SET FOR CHARACTER");
			}
			else
			{
				Debug.Log("Disabled sprite found!");
			}
		}

		if(mLockedButton == null)
		{
			Debug.LogWarning("CANNOT FIND LOCK BUTTON FOR CHARACTER. ATTEMPTING TO RESOLVE.");
			mLockedButton = transform.FindChild("Unlock Button").gameObject;
			if(mLockedButton == null)
			{
				Debug.LogError("LOCK BUTTON NOT SET FOR CHARACTER");
			}
			else
			{
				Debug.Log("Lock button found!");
			}
		}

		ChangeLockedState(mUnlocked || CheckCharacterPurchased());


	}

	private bool CheckCharacterPurchased()
	{
		string s1 = "";
		string s2 = "";
		switch(mId)
		{
		case AniminId.Pi:
			s1 = "com.apples.animin.characterunlock1";
			s2 = "com.apples.animin.characterpurchase1";
			break;
		case AniminId.Kelsey:
			s1 = "com.apples.animin.characterunlock2";
			s2 = "com.apples.animin.characterpurchase2";
			break;
		case AniminId.Mandi:
			s1 = "com.apples.animin.characterunlock3";
			s2 = "com.apples.animin.characterpurchase3";
			break;

		default:
		case AniminId.Tbo:
			break;
		}
		return ShopManager.Instance.HasBought(s1) || ShopManager.Instance.HasBought(s2);
	}

	public void ChangeLockedState(bool unlocked)
	{
		mUnlocked = unlocked;
		mSprite.SetActive(mUnlocked);
		mDisabledSprite.SetActive(!mUnlocked);
		mLockedButton.SetActive(!mUnlocked);
	}
}
