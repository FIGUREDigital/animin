using UnityEngine;
using System.Collections;

public class MoveTutorialHandScript : MonoBehaviour {

	float Lerp = 0;
	float Cooldown;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Cooldown > 0)
		{
			Cooldown -= Time.deltaTime;
			//this.GetComponent<UISprite>().enabled = false;
			Lerp = 1;
			if(Cooldown <= 0)
			{
				Cooldown = 0;
			}
		}
		else if(Cooldown == 0)
		{
			Cooldown = -1;
			Lerp = 0;
		}
		else
		{
			Lerp += Time.deltaTime * 0.6f;
			if(Lerp >= 1)
			{
				Lerp = 1;
				Cooldown = 0.6f;
			}
			//this.GetComponent<UISprite>().enabled = true;

			this.transform.localPosition = new Vector3(Mathf.Lerp(-376, 530, Lerp), -209.999f, 0);
		}
	}
}
