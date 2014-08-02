using UnityEngine;
using System.Collections;

public class EvilCharacterPatternMovementScript : MonoBehaviour 
{
	public float Speed;
	public float Lerp;
	public Vector3[] Pattern;
	public int Index;
	public bool ApplyRotation;
	private Quaternion RotateDirectionLookAt;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		float currentRelativeSpeed = Vector3.Distance(Pattern[Index], Pattern[Index + 1]);
		
		float amount = Time.deltaTime * (Speed / currentRelativeSpeed);
		Lerp += amount;
		
		if(Lerp >= 1)
		{
			Index++;
			if(Index >= Pattern.Length - 1)
				Index = 0;
			Lerp = 0;
			//Debug.Log("Change Index: " + Index.ToString());
		}

		this.transform.localPosition = Vector3.Lerp(
			Pattern[Index], 
			Pattern[Index + 1], 
			Lerp);

		if(ApplyRotation)
		{
			RotateDirectionLookAt = Quaternion.LookRotation(Vector3.Normalize(Pattern[Index + 1] - transform.localPosition));
			transform.localRotation = Quaternion.Slerp(transform.localRotation, RotateDirectionLookAt, Time.deltaTime * 6);
		}
		else
		{
			Debug.Log("UPDATING MOVEMENT FOR CUBE");
		}
	}
}
