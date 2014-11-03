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
		public List<CharacterData> CharacterList;	
		
	}

	public ProfileData ProfileSavedData; 

	[System.Serializable]
	public class CharacterData
	{
		public PersistentData.TypesOfAnimin Type;

		public DateTime NextHappynBonusTimeAt;
		public DateTime LastSavePerformed;
		public DateTime LastTimeToilet;

//		public InventoryData Inventory;
		public List<ItemDetails> ItemList;
		public EvolutionStage EvolutionLevel;
		public HappinessData Happiness;
		public AchievementData Achievements;
		public int Age;
		public int ZEF;

	}

	[System.Serializable]
	public class ItemDetails
	{
		public InventoryItemId ID;
		public int Amount;
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

		profileData.CharacterList = new List<CharacterData>();

		for (int i = 0; i < PersistentData.TypesOfAnimin.count-1; i++) 
		{
			CharacterData tempCharacter = CollectCharacterData((PersistentData.TypesOfAnimin)i);
			profileData.CharacterList.Add(tempCharacter);
		}				

		return profileData;

	}

	private CharacterData CollectCharacterData(PersistentData.TypesOfAnimin characterType)
	{
		CharacterData tempCharacter = new CharacterData ();

		tempCharacter.ItemList = new List<ItemDetails> ();

		for (int i = 0; i < InventoryItemId.Count-1; i++) 
		{
			ItemDetails tempItem = new ItemDetails();

			tempItem.ID = InventoryItemId[i];
			tempItem.Amount = PersistentData.Singleton.Inventory[i].Count;

			tempCharacter.ItemList.Add(tempItem);

		}

		return tempCharacter;
	}

	public void RepopulateData()
	{


	}

}
