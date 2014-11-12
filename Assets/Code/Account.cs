﻿using UnityEngine;
using System.Collections;

public class Account
{
    #region Singleton

    private static Account s_Instance;

    public static Account Instance
    {
        get
        {
            if( s_Instance == null )
            {
                s_Instance = new Account();
            }
            return s_Instance;
        }
    }

    #endregion

	private const string SERVER_SEND_URL = "http://terahard.org/Teratest/DatabaseAndScripts/AddData.php";

	private const string SERVER_CHECK_URL = "http://terahard.org/Teratest/DatabaseAndScripts/CheckLoginData.php";   

    public string UniqueID;

	public string UserName;

	public string FirstName;

	public string LastName;

	public string Address;

	public string Addressee;

    public int[] DemoCodes =
        {
            789456789456,
        };

	public IEnumerator WWWSendData( bool newUser,string name, string character, string address, string addressee, string firstname, string lastname)
    {
        //if(PlayerPrefs.HasKey( "PLAYER_ID" )) yield break;

        WWWForm webForm = new WWWForm();

        webForm.AddField( "Name", name );        
        if( newUser )
        {
            webForm.AddField( "NewUser", "1" );
        }
        else
        {            
			webForm.AddField( "NewUser", "0" );
            webForm.AddField( "ID", UniqueID );
//			Debug.Log ("test"+UniqueID+"test");
        }

        webForm.AddField( "Address", address );
//		Debug.Log (address);

        webForm.AddField( "Addressee", addressee );
//		Debug.Log (addressee);

		webForm.AddField( "FirstName", firstname );
//		Debug.Log (firstname);

		webForm.AddField( "LastName", lastname );
//		Debug.Log (lastname);

        webForm.AddField( "Device", "" + Application.platform );
//		Debug.Log (Application.platform);

        webForm.AddField( "Character", character );
//		Debug.Log (character);

        WWW w = new WWW( SERVER_SEND_URL, webForm );

        yield return w;

        if( w.error != null )
        {
            Debug.Log( w.error );
        }
        else
        {
            if( newUser )
            {
                UniqueID = w.text;
				UniqueID = UniqueID.Replace(System.Environment.NewLine, "");
//				Debug.Log("test"+UniqueID+"test");
                PlayerPrefs.SetString( "PLAYER_ID", UniqueID );
            }

//            Debug.Log( w.text );
            Debug.Log( "Finished uploading name data" );
        }

		if (newUser) 
		{
			ProfilesManagementScript.Singleton.NewUserProfileAdded();
		}

    }
	public IEnumerator WWWCheckLoginCode( string code )
	{
		
		WWWForm webForm = new WWWForm();

		webForm.AddField( "Code", code );
		
		WWW w = new WWW( SERVER_CHECK_URL, webForm );

		yield return w;
		
		if( w.error != null )
		{
			Debug.Log( w.error );
		}
		else
		{			
			
			Debug.Log( w.text );
			Debug.Log( "Finished uploading data" );

			string[] tempArray = w.text.Split(':');

			bool tempBool = bool.Parse(tempArray[0]);

            if (tempBool)
            {
                UserName = tempArray[1];
            }
			
			ProfilesManagementScript.Singleton.SuccessfulLogin(tempBool,code);						

		}
		
	}

    public IEnumerator CheckPurchaseCode(string code) 
    {

        bool DemoCode = CheckDemoCode(code);

        if (DemoCode)
        {
            //do something

        }
        else
        {
            WWWForm data = new WWWForm();

            data.AddField( "CardNumber", code );
            data.AddField( "UserID", UniqueID );
            data.AddField("Animin", ProfilesManagementScript.Singleton.AniminToUnlockId.ToString());

            var w = new WWW("http://terahard.org/Teratest/DatabaseAndScripts/CheckCardLegitimacy.php", data);

            yield return w;

            if (w.error != null)
            {
                Debug.Log(w.error);
                ProfilesManagementScript.Singleton.OnAccessCodeResult("Something went wrong, please try again in a bit...");
            }

            else
            {           

                Debug.Log(w.text);                              
                       
                ProfilesManagementScript.Singleton.OnAccessCodeResult(w.text);

            }
        }
    }

    public bool CheckDemoCode(string code)
    {

        for (int i=0; i < DemoCodes.Length; i++ )
        {
            if (code == DemoCodes[i])
            {
                return true;
            }
        }
        return false;

    }

    public void ClearAccountClassData()
    {

        UniqueID = "";

        UserName = "";

        FirstName = "";

        LastName = "";

        Address = "";

        Addressee = "";

    }
}
