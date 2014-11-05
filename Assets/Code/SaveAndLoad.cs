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
	public class ProfilesToStore
	{
        public PlayerProfileData PlayerData; 		
	}

	public List<ProfilesToStore> ProfileList; 

	public SaveAndLoad()
	{
		ProfileList = new List<ProfilesToStore> ();
	}

	public void LoadAllData()
	{
		
        File.Delete(Application.persistentDataPath + "/savedGames.anidat");

        if(File.Exists(Application.persistentDataPath + "/savedGames.anidat")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.anidat", FileMode.Open);
			ProfileList = (List<ProfilesToStore>)bf.Deserialize(file);
			file.Close();
			Debug.Log ("Save data loaded");
            RepopulateData();
		}
		else
		{
			Debug.Log ("No save data");

		}
	}

	public void SaveAllData()
	{

        ProfilesToStore tempProfile = new ProfilesToStore();
        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
            tempProfile.PlayerData = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[i];
            ProfileList.Add(tempProfile);
        }
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.anidat");
		bf.Serialize(file, ProfileList);
		file.Close();

	}	

    public void SaveDataToProfile()
    {
        PlayerProfileData profile = PlayerProfileData.ActiveProfile;

        for (int i = 0; i < (int)PersistentData.TypesOfAnimin.Count-1; i++) 
        {
            PlayerProfileData.CharacterData tempCharacter = CollectCharacterData((PersistentData.TypesOfAnimin)i);
            profile.ListOfDataForAnimin.Add(tempCharacter);
        }
    }

    private PlayerProfileData.CharacterData CollectCharacterData(PersistentData.TypesOfAnimin characterType)
	{
        PlayerProfileData.CharacterData tempCharacter = new PlayerProfileData.CharacterData ();

		tempCharacter.Type = characterType;

        tempCharacter.ItemList = new List<PlayerProfileData.ItemDetails> ();

		for (int i = 0; i < PersistentData.Singleton.Inventory.Count; i++) 
		{
            PlayerProfileData.ItemDetails tempItem = new PlayerProfileData.ItemDetails();

			tempItem.ID = PersistentData.Singleton.Inventory[i].Id;

			tempItem.Amount = PersistentData.Singleton.Inventory[i].Count;

			tempCharacter.ItemList.Add(tempItem);
		}
		
        tempCharacter.Achievements = new List<AchievementManager.AchievementDetails>();
        for (int j=0; j < AchievementManager.Instance.ListOfAchievements.Count; j++)
        {
            tempCharacter.Achievements.Add(AchievementManager.Instance.ListOfAchievements[j]);
        }   

        tempCharacter.ZEF = PersistentData.Singleton.ZefTokens;
        tempCharacter.Age = PersistentData.Singleton.Age;
        tempCharacter.EvolutionLevel = PersistentData.Singleton.Evolution; 
        tempCharacter.AnimEvolution = PersistentData.Singleton.AniminEvolutionId;

//		public DateTime NextHappynBonusTimeAt; // TODO: Figure out where these are stored
//		public DateTime LastTimeToilet;		

        tempCharacter.LastSavePerformed = DateTime.Now;

        tempCharacter.Happiness = new PlayerProfileData.HappinessData();

        tempCharacter.Happiness.Happiness = PersistentData.Singleton.Happy;
        tempCharacter.Happiness.Fitness = PersistentData.Singleton.Fitness;
        tempCharacter.Happiness.Health = PersistentData.Singleton.Health;
        tempCharacter.Happiness.Hunger = PersistentData.Singleton.Hungry;

		return tempCharacter;
	}

    public List<PlayerProfileData> LoadProfileData()
    {
        List<PlayerProfileData>  tempProfile = new List<PlayerProfileData> ();

        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
            tempProfile.Add(ProfileList[i].PlayerData);
        }

        return tempProfile;
    }

	public void RepopulateData()
	{
        ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Clear();

        for (int i =0; i< ProfileList.Count; i++)
        {
            ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Add(ProfileList[i].PlayerData);            
        }
	}

}
