using UnityEngine;
using System.Collections;

public class PurchaseChoiceScreen : MonoBehaviour {

	[SerializeField]
	private GameObject mTitle;
	[SerializeField]
	private GameObject mSocialButtons;

	void OnEnable()
	{
		mTitle.SetActive(false);
		mSocialButtons.SetActive(false);
	}

	void OnDisable()
	{
		mTitle.SetActive(true);
		mSocialButtons.SetActive(true);
	}

}
