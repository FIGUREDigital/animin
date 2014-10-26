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

    private const string SERVER_SEND_URL = "http://terahard.org/Animin/AddData.php";

    public string UniqueID;

    public Account()
    {
        UniqueID = PlayerPrefs.GetString( "PLAYER_ID" );
    }

    public void CreateAccount( string name )
    {

    }

    public IEnumerator WWWSendData( string name, string card = " ", string address = " ", string addressee = " " )
    {
        //if(PlayerPrefs.HasKey( "PLAYER_ID" )) yield break;

        WWWForm webForm = new WWWForm();

        webForm.AddField( "Name", name );

        if( address == " " )
        {
            webForm.AddField( "NewUser", "1" );
        }
        else
        {
            webForm.AddField( "NewUser", "0" );
            webForm.AddField( "ID", UniqueID );
        }

        webForm.AddField( "Address", address );

        webForm.AddField( "Addressee", addressee );

        webForm.AddField( "Device", "" + Application.platform );
        webForm.AddField( "Card", card );

        WWW w = new WWW( SERVER_SEND_URL, webForm );

        yield return w;

        if( w.error != null )
        {
            Debug.Log( w.error );
        }
        else
        {
            UniqueID = w.text;
           // PlayerPrefs.SetString( "PLAYER_ID", UniqueID );


            Debug.Log( w.text );
            Debug.Log( "Finished uploading data" );
        }

    }
}