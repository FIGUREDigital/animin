using UnityEngine;
using System.Collections;

public class AchievementPanel : MonoBehaviour 
{

	private AchievementItem[] mItems;
	[SerializeField]
	private UILabel mCountLabel;
	private int mAchievementCount;
	// Use this for initialization
	void Start () 
	{
		mItems = GetComponentsInChildren<AchievementItem>();
		if(mItems.Length > (int)AchievementManager.Achievements.Count)
		{
			Debug.LogError("Error: Incorrect Achievement Length");
		}
	}

	void OnEnable()
	{
		mAchievementCount = 0;
		int i = 0;
		foreach(AchievementItem item in mItems)
		{
			bool achieved = AchievementManager.Instance.Achieved(i);
			mAchievementCount += achieved?1:0;
			item.Achieved = achieved;
			item.Description(AchievementManager.Instance.Description(i));
			i++;
		}
		if(mCountLabel != null)
		{
			mCountLabel.text = mAchievementCount.ToString();
		}
	}

	void OnDisable()
	{
		AchievementManager.Instance.SaveAchievments();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
