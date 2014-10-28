using UnityEngine;
using System.Collections;

public class ParentalGateway : MonoBehaviour 
{
	[SerializeField]
	private GatewayButton correctButton;
	private GameObject prevScreen;
	private GameObject nextScreen;

	public void Open(GameObject prev, GameObject next)
	{
		prevScreen = prev;
		nextScreen = next;
		prevScreen.SetActive(false);
		this.gameObject.SetActive(true);
	}

	public void Pass()
	{
		Destroy(this, 1);
		nextScreen.SetActive(true);
	}

	public void Fail()
	{
		Destroy(this, 1);
		prevScreen.SetActive(true);
	}
}
