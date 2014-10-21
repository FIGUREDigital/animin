using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class EvolutionManager : MonoBehaviour 
{
	[SerializeField]
	private const string FILENAME = "Assets/Resources/MarkerGuide.xml";
	private const int MARKER_RATE = 10;
	private const int BABY_EVOLVE_THRESHOLD = 200;
	private const int KID_EVOLVE_THRESHOLD = 600;
	private const int ADULT_EVOLVE_THRESHOLD = 600;
	private int mNextMarker;
	private int mZefProgress;
	private int mCurrentMarker;
	private string mReward;
	private AniminEvolutionStageId mCorrectStage;

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
	public void Init()
	{
	}
	public void AddZef()
	{
		PersistentData.Singleton.ZefTokens++;
	}
	public void AddZef(int amount)
	{
		PersistentData.Singleton.ZefTokens += amount;
	}

	public void RemoveZef()
	{
		PersistentData.Singleton.ZefTokens--;
		if(PersistentData.Singleton.ZefTokens < 0)
		{
			PersistentData.Singleton.ZefTokens = 0;
		}
	}

	public void RemoveZef(int amount)
	{
		PersistentData.Singleton.ZefTokens -= amount;
	}

	public void UpdateEvo () 
	{
		if(PersistentData.Singleton.ZefTokens >= mNextMarker)
		{
			mNextMarker += MARKER_RATE;
			//mReward = mMarkers[mCurrentMarker];
			mCurrentMarker++;
		}

		CheckEvolution();
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
