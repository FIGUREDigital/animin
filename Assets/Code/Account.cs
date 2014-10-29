using UnityEngine;
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

    private const string SERVER_SEND_URL = "http://animin.me/wp-admin/DatabaseAndScripts/AddData.php";

	private const string SERVER_CHECK_URL = "http://animin.me/wp-admin/DatabaseAndScripts/CheckLoginData.php";

    public string UniqueID;

	public string UserName;

	public string FirstName;

	public string LastName;

	public string Address;

	public string Addressee;

    public Account()
    {
        UniqueID = PlayerPrefs.GetString( "PLAYER_ID" );
    }

    public void CreateAccount( string name )
    {

    }

    public IEnumerator WWWSendData( string name, string card, string address, string addressee, string firstname, string lastname)
    {
        //if(PlayerPrefs.HasKey( "PLAYER_ID" )) yield break;

        WWWForm webForm = new WWWForm();

        webForm.AddField( "Name", name );
        bool newUser = address == " ";
        if( newUser )
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

		webForm.AddField( "FirstName", firstname );
		
		webForm.AddField( "LastName", lastname );

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
            if( newUser )
            {
                UniqueID = w.text;
                PlayerPrefs.SetString( "PLAYER_ID", UniqueID );
            }


            Debug.Log( w.text );
            Debug.Log( "Finished uploading data" );
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
			
			ProfilesManagementScript.Singleton.SuccessfulLogin(tempBool,code);

			if (tempBool)
			{
				UserName = tempArray[1];
			}
			Debug.Log (UserName);

		}
		
	}
}