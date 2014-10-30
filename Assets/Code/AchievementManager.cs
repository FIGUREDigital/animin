using UnityEngine;
using System.Collections;

public class AchievementManager  
{
	public enum Achievements
	{
		EatFruit = 0,
		PlayMinigames,
		PlayMusic,
		ArMode,
		Heal,
		Count
	}

	private int mCompletedAchievements;
	private int[] mCount = new int[(int)Achievements.Count];
	private int[] mRequiredCount = new int[(int)Achievements.Count]{10,5,3,1,3};
	private bool[] mAchievmentsFired = new bool[(int)Achievements.Count];
	private const string mDescription = "Congratulations, you unlocked the achievement \n{0}!"; 
	private const string mFruit = "Devour 10 pieces of juicy fruit"; 
	private const string mGames = "Push yourself to the limit with 5 games"; 
	private const string mMusic = "Create experimental music 3 times"; 
	private const string mArMode = "Use the AR card to unlesh your Animin on the world"; 
	private const string mHeal = "Care for your Animin 3 times"; 


	public int CompletedAchievements
	{
		get
		{
			return mCompletedAchievements;
		}
	}
	public bool Achieved(int i)
	{
		return mAchievmentsFired[i];
	}

	public string Description(int i)
	{
		return Description((Achievements)i);
	}
	public string Description(Achievements item)
	{
		string achievement = "";
		switch(item)
		{
		case Achievements.EatFruit:
			achievement = mFruit;
			break;
		case Achievements.PlayMinigames:
			achievement = mGames;
			break;
		case Achievements.PlayMusic:
			achievement = mMusic;
			break;
		case Achievements.ArMode:
			achievement = mArMode;
			break;
		case Achievements.Heal:
			achievement = mHeal;
			break;
		default:
			break;
			
		}

		return achievement;
	}

	#region Singleton
	
	private static AchievementManager s_Instance;
	
	public static AchievementManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new AchievementManager();
				s_Instance.PopulateAchievments();
			}
			return s_Instance;
		}
	}
	
	#endregion
	private void ResetAchievments()
	{
		mCompletedAchievements = 0;
		PlayerPrefs.SetString("AchievementsActive","false");
		for(int i =0; i < (int)Achievements.Count; i++)
		{
			PlayerPrefs.SetInt("Achievment" + i, 0);
			PlayerPrefs.SetInt("AchievmentFired" + i, 0);
		}
		PlayerPrefs.Save();
	}

	private void PopulateAchievments()
	{
		bool po = "true" == PlayerPrefs.GetString("AchievementsActive");

		if(!po)
		{
			ResetAchievments();
		}

		for(int i =0; i < (int)Achievements.Count; i++)
		{
			mCount[i] = PlayerPrefs.GetInt("Achievment" + i);
			bool fired = PlayerPrefs.GetInt("AchievmentFired" + i) == 1;
			mAchievmentsFired[i] = fired;
			mCompletedAchievements += fired?1:0;
		}

	}

	public void SaveAchievments()
	{
		PlayerPrefs.SetString("AchievementsActive","true");
		for(int i =0; i < (int)Achievements.Count; i++)
		{
			PlayerPrefs.SetInt("Achievment" + i, mCount[i]);
			PlayerPrefs.SetInt("AchievmentFired" + i, mAchievmentsFired[i]?1:0);
		}
		PlayerPrefs.Save();
	}

	public void AddToAchievment(Achievements item)
	{
		mCount[(int)item]++;
		CheckAchievments();
	}

	private void CheckAchievments()
	{
		for(int i = 0; i < (int)Achievements.Count; i++)
		{
			if(mCount[i] >= mRequiredCount[i] && !mAchievmentsFired[i])
			{
				mCompletedAchievements++;
				FireAchievment((Achievements)i);
				mAchievmentsFired[i] = true;
			}
		}
	}


	private void FireAchievment(Achievements item)
	{
	
		string achievement = Description(item);

		AchievementsScript.Singleton.Show(AchievementTypeId.Achievement,1000);
		AchievementsScript.Singleton.Description.text = string.Format(mDescription, achievement);	                                                       ;
	}
}
