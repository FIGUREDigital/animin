using UnityEngine;
using System.Collections;

public class AchievementCount : MonoBehaviour 
{
	UILabel label;
	void Start()
	{
		label = GetComponent<UILabel>();
		label.text = AchievementManager.Instance.CompletedAchievements.ToString();
	}
	void OnEnable()
	{
		if(label == null)
		{
			label = GetComponent<UILabel>();
		}
		label.text = AchievementManager.Instance.CompletedAchievements.ToString();
	}

}
