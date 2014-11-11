using UnityEngine;
using System.Collections;

public class AgeScript : MonoBehaviour {

	void OnEnable()
	{
		UILabel label = gameObject.GetComponent<UILabel>();
		label.text = "Age "+ ProfilesManagementScript.Singleton.CurrentAnimin.Age;
	}
}
