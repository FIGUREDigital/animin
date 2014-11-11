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

    public List<PlayerProfileData> ProfileList; 

    public void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
    }

	public SaveAndLoad()
	{
        ProfileList = new List<PlayerProfileData> ();
//		PlayerPrefs.DeleteAll ();
	}

	public void LoadAllData()
	{
		
//        File.Delete(Application.persistentDataPath + "/savedGames.anidat");

        if(File.Exists(Application.persistentDataPath + "/savedGames.anidat")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.anidat", FileMode.Open);
            ProfileList = (List<PlayerProfileData>)bf.Deserialize(file);
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
        File.Delete(Application.persistentDataPath + "/savedGames.anidat");
//        Debug.Log("Save section " + 1);
        ProfileList.Clear();
//        Debug.Log("Save section " + 2);
//        Debug.Log("Save section " + 3);
        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
//            Debug.Log("Save section " + 4);
            PlayerProfileData tempProfile = new PlayerProfileData();
//            Debug.Log("Save section " + 5);
//            Debug.Log("Save section " + 6);
            tempProfile = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[i];
//            Debug.Log("Save section " + 7);
            ProfileList.Add(tempProfile);
//            Debug.Log("Save section " + 8);
        }
//        Debug.Log("Save section " + 9);
//        Debug.Log("Save section " + 10);
		BinaryFormatter bf = new BinaryFormatter();
//        Debug.Log("Save section " + 11);
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.anidat");
//        Debug.Log("Save section " + 12);
		bf.Serialize(file, ProfileList);
//        Debug.Log("Save section " + 13);
//        Debug.Log("Savesize " + ProfileList.Count);
//        Debug.Log("Save section " + 14);
		file.Close();
//        Debug.Log("Save section " + 15);

	}	

    public List<PlayerProfileData> LoadProfileData()
    {
        List<PlayerProfileData>  tempProfile = new List<PlayerProfileData> ();

        for (int i =0; i< ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Count; i++)
        {
            tempProfile.Add(ProfileList[i]);
//            Debug.Log("Profile load "+ i + " " + ProfileList[i].PlayerData.ProfileName);
        }

        return tempProfile;
    }

	public void RepopulateData()
	{
        ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Clear();

        for (int i =0; i< ProfileList.Count; i++)
        {
            ProfilesManagementScript.Singleton.ListOfPlayerProfiles.Add(ProfileList[i]);            
        }

	}
}
