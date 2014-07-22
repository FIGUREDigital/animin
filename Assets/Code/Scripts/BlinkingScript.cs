using UnityEngine;
using System.Collections;

public class BlinkingScript : MonoBehaviour 
{

	public Texture2D Default;
	public Texture2D BlinkTexture;
	private float NextBlinkCounter;
	private float BlinkingTimer;
	private bool DoBlink;

	// Use this for initialization
	void Start () {
	
		NextBlinkCounter = UnityEngine.Random.Range(3.0f, 6.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(DoBlink)
		{
			BlinkingTimer -= Time.deltaTime;
			if(BlinkingTimer <= 0)
			{
				DoBlink = false;
				NextBlinkCounter = UnityEngine.Random.Range(2.0f, 5.5f);
			}
		}
		else if(NextBlinkCounter <= 0)
		{
			DoBlink = true;
			BlinkingTimer = UnityEngine.Random.Range(0.1f, 0.18f);
			renderer.material.mainTexture = BlinkTexture;
		}
		else
		{
			NextBlinkCounter -= Time.deltaTime;
			renderer.material.mainTexture = Default;
		}


	
	}
}
