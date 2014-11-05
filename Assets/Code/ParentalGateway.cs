using UnityEngine;
using System.Collections;

public class ParentalGateway : MonoBehaviour 
{
	private GatewayButton[] buttons;
	private GameObject prevScreen;
	private GameObject nextScreen;

	[SerializeField]
	GameObject EquationScreen;
	
	[SerializeField]
	GameObject PasswordScreen;

	[SerializeField]
	UILabel Question;

	private bool mLogPurchase;

	public void Open(GameObject prev, GameObject next, bool LogPurchase = false)
	{
		mLogPurchase = LogPurchase;
		prevScreen = prev;
		nextScreen = next;
		prevScreen.SetActive(false);
		this.gameObject.SetActive(true);

		bool passwordSet = (PlayerPrefs.GetString ("ParentalPassword") != null && PlayerPrefs.GetString ("ParentalPassword") != "");
		EquationScreen.SetActive (!passwordSet);
		PasswordScreen.SetActive (passwordSet);

		buttons = GetComponentsInChildren<GatewayButton> ();

		int result = Random.Range (100, 900);
		int first = Random.Range (0, result);
		int second = result - first;

		int correct = Random.Range (0, buttons.Length);
		for (int i =0; i < buttons.Length; i++){
			if (i == correct){
				buttons[i].GetComponentInChildren<UILabel>().text = second.ToString();
				buttons[i].Active = true;
			} else {
				buttons[i].GetComponentInChildren<UILabel>().text = (Random.Range(100,900).ToString());
				buttons[i].Active = false;
			}
		}
		Question.text = (first.ToString () + " + X = " + result.ToString ());
	}

	public void Pass()
	{
		if(mLogPurchase)
		{
		}
		Destroy(this.gameObject, 0);
		nextScreen.SetActive(true);
	}

	public void Fail()
	{
		Destroy(this.gameObject, 0);
		prevScreen.SetActive(true);
	}
}
