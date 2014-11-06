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
//		PlayerPrefs.DeleteAll ();
	}

	public void LoadAllData()
	{
		
//        File.Delete(Application.persistentDataPath + "/savedGames.anidat");

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
//        File.Delete(Application.persistentDataPath + "/savedGames.anidat");
        ProfileList.Clear();
        ProfilesToStore tempProfile;
        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
            tempProfile = new ProfilesToStore();
            tempProfile.PlayerData = new PlayerProfileData();
            tempProfile.PlayerData = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[i];
            ProfileList.Add(tempProfile);
//            Debug.Log ("Save "+i+ " "+ ProfileList[ProfileList.Count-1].PlayerData.ProfileName);
//            for (int j =0; j< ProfileList.Count; j++)
//            {
//                Debug.Log ("Save prof "+i+ " "+j+" "+ ProfileList[i].PlayerData.ProfileName);
//            }
        }
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.anidat");
		bf.Serialize(file, ProfileList);
//        Debug.Log("Savesize " + ProfileList.Count);
		file.Close();

	}	

    public List<PlayerProfileData> LoadProfileData()
    {
        List<PlayerProfileData>  tempProfile = new List<PlayerProfileData> ();

        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
            tempProfile.Add(ProfileList[i].PlayerData);
//            Debug.Log("Profile load "+ i + " " + ProfileList[i].PlayerData.ProfileName);
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
