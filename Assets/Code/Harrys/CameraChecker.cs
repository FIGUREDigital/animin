using UnityEngine;
using System.Collections;

public class CameraChecker : MonoBehaviour {

	private QCARBehaviour script;

	// Use this for initialization
	void Start () {
		script = this.GetComponent<QCARBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		script.enabled = (WebCamTexture.devices.Length != 0);
	}
}
