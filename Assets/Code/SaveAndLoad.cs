using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveAndLoad {

	#region Singleton
	
	private static SaveAndLoad s_Instance;
	
	public static SaveAndLoad Instance
	{
		get
		{
			if( s_Instance == null )
			{
				s_Instance = new SaveAndLoad();
			}
			return s_Instance;
		}
	}
	
	#endregion

	[System.Serializable]
	public class ProfileData
	{
		public CharacterData TBO;
		public CharacterData Kelsi;
		public CharacterData Mandi;
		public CharacterData Pi;	
		
	}

	public ProfileData ProfileSavedData; 

	[System.Serializable]
	public class CharacterData
	{
		public DateTime NextHappynBonusTimeAt;
		public DateTime LastSavePerformed;
		public DateTime LastTimeToilet;

		public InventoryData Inventory;
		public EvolutionStage EvolutionLevel;
		public HappinessData Happiness;
		public AchievementData Achievements;
		public int Age;
		public int ZEF;

	}

	[System.Serializable]
	public class InventoryData
	{
		// Food
		public int JuiceNumber;
		public int BlueberryNumber;
		public int ChipsNumber;
		public int StrawberryNumber;
		public int AvocadoNumber;
		public int CarrotNumber;
		public int SpinachNumber;
		public int BreadJamNumber;


		//items
		public int BoomBoxNumber;
		public int AlarmNumber;
		public int CameraNumber;
		public int SynthNumber;
		public int JunoNumber;
		public int EDMNumber;
		public int FartButtonNumber;
		public int LightbulbNumber;

		//medicine

		public int PillNumber;
		public int SyringeNumber;
		public int PlasterNumber;

	}

	[System.Serializable]
	public class HappinessData
	{
		public float MainHappiness;
		public float Health;
		public float Hunger;
		public float Fitness;

	}

	[System.Serializable]
	public class AchievementData
	{
		public float MainHappiness;
		
	}

	[System.Serializable]
	public enum EvolutionStage
	{
		Child,
		Juvenile,
		Adult		
	}

	public SaveAndLoad()
	{
		ProfileSavedData = new ProfileData ();

	}

	public void LoadAllData()
	{
		if(File.Exists(Application.persistentDataPath + "/savedGames.anidat")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.anidat", FileMode.Open);
			ProfileSavedData = (ProfileData)bf.Deserialize(file);
			file.Close();
			Debug.Log ("Save data loaded");
		}
		else
		{
			Debug.Log ("No save data");

		}
	}

	public void SaveAllData()
	{
		ProfileSavedData = CollectData ();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.anidat");
		bf.Serialize(file, ProfileSavedData);
		file.Close();

	}

	public ProfileData CollectData()
	{
		ProfileData profileData = new ProfileData();

		profileData.TBO = new CharacterData (); 
		profileData.TBO	= CollectCharacterData(profileData.TBO);		

		return profileData;

	}

	private CharacterData CollectCharacterData(CharacterData character)
	{
		return character;
	}

	public void RepopulateData()
	{


	}

}
