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

    private SelectCharacterClickScript characterClickScript;

	// Use this for initialization
	void Start () 
	{

	    for (int i = 0; i < ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins.Count; i++)
	    {
	        if (ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins[i] == PersistentData.TypesOfAnimin.TboAdult)
	        {
	            mId = PersistentData.TypesOfAnimin.TboAdult;
	            characterClickScript = GetComponentInChildren<SelectCharacterClickScript>();
                characterClickScript.Animin = PersistentData.TypesOfAnimin.TboAdult;
	        }

            if (mId == ProfilesManagementScript.Singleton.CurrentProfile.UnlockedAnimins[i])
	        {
	            mUnlocked = true;
	            break;
	        }
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
        if(ProfilesManagementScript.Singleton.CurrentProfile != null)
		{
            PersistentData pd = ProfilesManagementScript.Singleton.CurrentProfile.Characters[(int)mId];
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
