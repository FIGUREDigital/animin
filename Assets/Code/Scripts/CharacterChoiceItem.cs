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
	private GameObject mAgeLabel;
	[SerializeField]
	private bool mUnlocked;
	[SerializeField]
    private PersistentData.TypesOfAnimin mId;

	// Use this for initialization
	void Start () 
	{

		switch(mId)
		{
            case PersistentData.TypesOfAnimin.Pi:
			mUnlocked = PlayerPrefs.GetInt("piUnlocked")==1?true:false;
			break;
            case PersistentData.TypesOfAnimin.Kelsey:
			mUnlocked = PlayerPrefs.GetInt("kelseyUnlocked")==1?true:false;
			break;
            case PersistentData.TypesOfAnimin.Mandi:
			mUnlocked = PlayerPrefs.GetInt("mandiUnlocked")==1?true:false;
			break;
            case PersistentData.TypesOfAnimin.Tbo:
			break;
		default:
			break;
		}
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
	}

	void OnEnable()
	{
		ChangeLockedState(mUnlocked || UnlockCharacterManager.Instance.CheckCharacterPurchased(mId));
//        Debug.Log(mId);
		Invoke ("UpdateAge", 0.1f);
	}

	private void UpdateAge()
	{
		UILabel label = mAgeLabel.GetComponent<UILabel>();
		if(PlayerProfileData.ActiveProfile != null)
		{
			PersistentData pd = PlayerProfileData.ActiveProfile.Characters[(int)mId];
			label.text = "Age " + pd.Age;
		}
		else
		{
			label.text = "";
		}
	}

	public void ChangeLockedState(bool unlocked)
	{
		mUnlocked = unlocked;
		mSprite.SetActive(mUnlocked);
		mDisabledSprite.SetActive(!mUnlocked);
		mLockedButton.SetActive(!mUnlocked);
		mAgeLabel.SetActive(mUnlocked);
	}
	public void UnlockCharacter()
	{
		CharacterChoiceItem character = GameObject.Find(mId.ToString()).GetComponent<CharacterChoiceItem>();
		character.ChangeLockedState(true);
	}
}
