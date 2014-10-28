using UnityEngine;
using System.Collections;

public class Toggleable : MonoBehaviour {
	
	[SerializeField]
	private GameObject ToggleableOn;
	[SerializeField]
	private GameObject ToggleableOff;

	void OnClick(){
		if (ToggleableOn.activeInHierarchy) {
			ToggleableOn.SetActive (false);
			ToggleableOff.SetActive (true);
		} else  {
			ToggleableOn.SetActive (true);
			ToggleableOff.SetActive (false);
		}
	}
}
