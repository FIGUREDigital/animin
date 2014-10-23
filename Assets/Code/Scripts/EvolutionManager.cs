﻿using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class EvolutionManager 
{
	enum HappinessState
	{
		NotSet,
		Failing,
		Normal,
		Winning,
	}
	[SerializeField]
	private const string FILENAME = "Assets/Resources/MarkerGuide.xml";
	private const int MARKER_RATE = 10;
	private const int BABY_EVOLVE_THRESHOLD = 200;
	private const int KID_EVOLVE_THRESHOLD = 600;
	private const int ADULT_EVOLVE_THRESHOLD = 1400;
	private const int HAPPINESS_FAIL_THRESHOLD = 30;
	private const int HAPPINESS_WIN_THRESHOLD = 80;
	private const int HAPPINESS_BIG_WIN_THRESHOLD = 110;
	private const float TIME_FOR_REWARD = 1200;
	private int mNextMarker;
	private int mZefProgress;
	private int mCurrentMarker;
	private string mReward;
	private AniminEvolutionStageId mCorrectStage;
	private HappinessState mHappinessState;
	private HappinessState mPrevHappinessState;
	private float mTimeInHappinessState;

	public static List<string> mMarkers = new List<string>();

	public int BabyEvolutionThreshold
	{
		get
		{
			return BABY_EVOLVE_THRESHOLD;
		}
	}
	public int KidEvolutionThreshold
	{
		get
		{
			return KID_EVOLVE_THRESHOLD;
		}
	}
	public int AdultEvolutionThreshold
	{
		get
		{
			return ADULT_EVOLVE_THRESHOLD;
		}
	}

	#region Singleton
	
	private static EvolutionManager s_Instance;
	
	public static EvolutionManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new EvolutionManager();
			}
			return s_Instance;
		}
	}
	
	#endregion
	public void UpdateEvo()
	{
		CheckHappiness();
	}
	public void AddZef()
	{
		AddZef(1);
	}
	public void AddZef(int amount)
	{
		PersistentData.Singleton.ZefTokens += amount;
		ZefChanged();
	}

	public void RemoveZef()
	{
		RemoveZef(1);
	}

	public void RemoveZef(int amount)
	{
		PersistentData.Singleton.ZefTokens -= amount;
		if(PersistentData.Singleton.ZefTokens < 0)
		{
			PersistentData.Singleton.ZefTokens = 0;
		}
		ZefChanged();
	}

	private void ZefChanged () 
	{
		if(PersistentData.Singleton.ZefTokens >= mNextMarker)
		{
			mNextMarker += MARKER_RATE;
			if(mMarkers.Count > 0)
			{
				mReward = mMarkers[mCurrentMarker];
			}
			mCurrentMarker++;
		}

		CheckEvolution();
		UpdateEvoBar();

	}

	private void UpdateEvoBar()
	{
		int min =0;
		int max =0;
		int curZef =0;
		curZef = PersistentData.Singleton.ZefTokens;
		AniminEvolutionStageId stage = PersistentData.Singleton.AniminEvolutionId;
		switch(stage)
		{
		case AniminEvolutionStageId.Baby:
			min = 0;
			max = BABY_EVOLVE_THRESHOLD;
			break;
		case AniminEvolutionStageId.Kid:
			min = BABY_EVOLVE_THRESHOLD;
			max = KID_EVOLVE_THRESHOLD;
			break;
		case AniminEvolutionStageId.Adult:
			min = KID_EVOLVE_THRESHOLD;
			max = ADULT_EVOLVE_THRESHOLD;
			break;
		default:
			break;
		}

		curZef -= min;
		int diff = max - min;
		float percentage = (curZef * 100f) / (diff * 100f);

		PersistentData.Singleton.Evolution = percentage;
	}

	private void CheckHappiness()
	{
		float happiness = PersistentData.Singleton.Happy;
		if(happiness < HAPPINESS_FAIL_THRESHOLD)
		{
			mHappinessState = HappinessState.Failing;
		}
		else if(happiness < HAPPINESS_WIN_THRESHOLD)
		{
			mHappinessState = HappinessState.NotSet;
		}
		else if(happiness < HAPPINESS_BIG_WIN_THRESHOLD)
		{
			mHappinessState = HappinessState.Normal;
		}
		else if(happiness > HAPPINESS_BIG_WIN_THRESHOLD)
		{
			mHappinessState = HappinessState.Winning;
		}

		if(mHappinessState != mPrevHappinessState)
		{
			mTimeInHappinessState = 0;
		}
		mTimeInHappinessState += Time.deltaTime;

		if(mTimeInHappinessState > TIME_FOR_REWARD)
		{
			mTimeInHappinessState = 0;
			switch(mHappinessState)
			{
			case HappinessState.Failing:
				RemoveZef(1);
				break;
			case HappinessState.Normal:
				AddZef(1);
				break;
			case HappinessState.Winning:
				AddZef(3);
				break;
				
			case HappinessState.NotSet:
			default:
				break;
			}
		}
	}

	private void CheckEvolution()
	{
		int zef = PersistentData.Singleton.ZefTokens;
		AniminEvolutionStageId stage = PersistentData.Singleton.AniminEvolutionId;
		AniminEvolutionStageId correctStage = AniminEvolutionStageId.Count;
		if(zef < BABY_EVOLVE_THRESHOLD)
		{
			correctStage = AniminEvolutionStageId.Baby;
		}
		else if (zef < KID_EVOLVE_THRESHOLD)
		{
			correctStage = AniminEvolutionStageId.Kid;
		}
		else if(zef < ADULT_EVOLVE_THRESHOLD)
		{
			correctStage = AniminEvolutionStageId.Adult;
		}
		else
		{
			correctStage = AniminEvolutionStageId.Adult;
		}
		if(stage != correctStage)
		{
			mCorrectStage = correctStage;
			if(stage < correctStage)
			{
				Evolve();
			}
			if(stage > correctStage)
			{
				Devolve();
			}
		}
	}
	private void Evolve()
	{
		PersistentData.Singleton.AniminEvolutionId = mCorrectStage;
		UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterSwapManagementScript>().LoadCharacter(PersistentData.Singleton.PlayerAniminId, mCorrectStage);
		AchievementsScript.Singleton.Show(AchievementTypeId.Evolution, 100);
	}
	private void Devolve()
	{
		PersistentData.Singleton.AniminEvolutionId = mCorrectStage;
		UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterSwapManagementScript>().LoadCharacter(PersistentData.Singleton.PlayerAniminId, mCorrectStage);
	}

	public List<string> Deserialize()
	{
		List<string> data = null;
		
		XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
		
		StreamReader reader = new StreamReader(FILENAME);
		data = (List<string>)serializer.Deserialize(reader);
		reader.Close();
		
		return data;
	}

	public void Serialize()
	{
		XmlSerializer ser = new XmlSerializer(typeof(List<string>));
		TextWriter writer = new StreamWriter(FILENAME);
		ser.Serialize(writer, mMarkers);
		writer.Close();
	}
}
