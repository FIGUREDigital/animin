using UnityEngine;
using System.Collections;

public class PortalAnimationScript : MonoBehaviour 
{

	public float Speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.localRotation = Quaternion.Euler(Time.time * Speed, 0, 0);
	
	}
}
