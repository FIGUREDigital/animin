using UnityEngine;
using System.Collections;

public class ParentalGateway : MonoBehaviour 
{
	[SerializeField]
	private GatewayButton correctButton;
	private GameObject prevScreen;
	private GameObject nextScreen;

	[SerializeField]
	GameObject EquationScreen;
	
	[SerializeField]
	GameObject PasswordScreen;

	public void Open(GameObject prev, GameObject next)
	{
		prevScreen = prev;
		nextScreen = next;
		prevScreen.SetActive(false);
		this.gameObject.SetActive(true);

		bool passwordSet = (PlayerPrefs.GetString ("ParentalPassword") != null && PlayerPrefs.GetString ("ParentalPassword") != "");
		EquationScreen.SetActive (!passwordSet);
		PasswordScreen.SetActive (passwordSet);
	}

	public void Pass()
	{
		Destroy(this.gameObject, 0);
		nextScreen.SetActive(true);
	}

	public void Fail()
	{
		Destroy(this.gameObject, 0);
		prevScreen.SetActive(true);
	}
}
