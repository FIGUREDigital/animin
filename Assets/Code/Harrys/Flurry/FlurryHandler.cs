using UnityEngine;
using System.Collections;

public class FlurryHandler : MonoBehaviour {

	void Start () {
		FlurryLogger.Instance.Init ();
	}

	void OnApplicationQuit(){
		FlurryLogger.Instance.EndSession ();
	}
}
