using UnityEngine;
using System.Collections;

public class PortalAnimationScript : MonoBehaviour 
{

	public float Speed = 1;
	public bool IsShowing;
	public bool IsHiding;

	// Use this for initialization
	void Start () {
		renderer.material.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.localRotation = Quaternion.Euler(0, 0, Time.time * Speed);
//		Debug.Log(renderer.material.color.ToString());
		if(IsShowing)
		{
			//this.transform.localScale = new Vector3(160,160,160);
			float alpha = renderer.material.color.a + Time.deltaTime * 2;
			if(alpha >= 1)
			{
				alpha = 1;
				IsShowing = false;
			}
			renderer.material.color = new Color(1, 1, 1, alpha);
		}
		else if(IsHiding)
		{
			float alpha = renderer.material.color.a - Time.deltaTime;
			if(alpha <= 0)
			{
				alpha = 0;
				IsHiding = false;
				this.gameObject.transform.parent.gameObject.SetActive(false);
			}
			renderer.material.color = new Color(1, 1, 1, alpha);
			//this.transform.localScale = Vector3.Lerp(new Vector3(160,160,160), new Vector3(20, 20, 20), 1 - alpha);

		}
	 
	}
}
