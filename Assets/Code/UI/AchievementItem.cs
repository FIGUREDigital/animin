using UnityEngine;
using System.Collections;

public class AchievementItem : MonoBehaviour 
{
	[SerializeField]
	private UISprite mTick;
	[SerializeField]
	private UISprite mDot;

	public UILabel mDescription;

	public bool Achieved;

	private AchievementManager.Achievements mAchievement;

	void Start()
	{
		mTick = gameObject.transform.FindChild("Tick").gameObject.GetComponent<UISprite>();
		if(mTick == null){Debug.Log("Error: tick not found");}
		mDot = gameObject.transform.FindChild("Dot").gameObject.GetComponent<UISprite>();
		if(mTick == null){Debug.Log("Error: dot not found");}
		mDescription = gameObject.transform.FindChild("Description").gameObject.GetComponent<UILabel>();
		if(mDescription == null){Debug.Log("Error: tick not found");};
	}

	void OnEnable()
	{
		mTick.gameObject.SetActive(Achieved?true:false);
		mDot.gameObject.SetActive(Achieved?false:true);
	}

	public void Description(string text)
	{
		mDescription.text = text;
	}


}
