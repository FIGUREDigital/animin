using UnityEngine;
using System.Collections;

public class ProgressBarUVScript : MonoBehaviour 
{
	public GameObject ZefToken;

	float HappyAverage;
	const float SamplesPerTrigger = 60.0f;
	private float Accumulator;
	private float Timer;
	private int SamplesTaken;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("UPDATING!!!");
		CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

		Timer -= Time.deltaTime;
		if(Timer <= 0)
		{
			Timer = 1;
			Accumulator += script.Happy;
			SamplesTaken++;

			if(SamplesTaken >= SamplesPerTrigger)
			{
				int TokensPerHappyAverage = (int)((Accumulator / (CharacterProgressScript.MaxHappy * SamplesTaken)) * 6);
				for(int i=0;i<TokensPerHappyAverage;++i)
				{
					GameObject gameObject = GameObject.Instantiate(ZefToken) as GameObject;
					gameObject.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveWorld.transform;

					gameObject.transform.localPosition = new Vector3(
						Random.Range(-0.9f, 0.9f),
						0.0f,
						Random.Range(-0.9f, 0.9f));
					gameObject.transform.localRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
				
					script.GroundItems.Add(gameObject);
				}


				SamplesTaken = 0;
				Accumulator = 0;
			}
		}
	}
}
