using UnityEngine;
using System.Collections;

public class CubeAnimatonScript : MonoBehaviour 
{
	public Vector3 ValueNext;
	public float Delay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		Delay -= Time.deltaTime;
		

		if(Delay <= 0)
		{
				
			this.transform.position = Vector3.Lerp(this.transform.position, ValueNext, Time.deltaTime * 11);

			if(Vector3.Distance(this.transform.position, ValueNext) <= 0.1f)
			{
				this.transform.position = ValueNext;
				Destroy(this);
			}

		}

	
	}
}
