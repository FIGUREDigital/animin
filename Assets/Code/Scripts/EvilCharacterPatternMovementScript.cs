using UnityEngine;
using System.Collections;

public class EvilCharacterPatternMovementScript : MonoBehaviour 
{
	public float Speed;
	public float Lerp;
	public Vector3[] Pattern;
	public int Index;

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

		RotateDirectionLookAt = Quaternion.LookRotation(Vector3.Normalize(Pattern[Index + 1] - transform.position));

		/*if(Pattern[Index + 1].x > Pattern[Index].x)
		{
			//Debug.Log("greater x:" + (Pattern[Index + 1] - Pattern[Index]).normalized.ToString());
			this.transform.rotation = Quaternion.Euler(0, 90, 0);
		}
		else if(Pattern[Index + 1].x < Pattern[Index].x)
		{
			//Debug.Log("less x:" + (Pattern[Index + 1] - Pattern[Index]).normalized.ToString());
			this.transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else if(Pattern[Index + 1].y > Pattern[Index].y)
		{
			//Debug.Log("greater y:" + (Pattern[Index + 1] - Pattern[Index]).normalized.ToString());
			this.transform.rotation = Quaternion.Euler(0, 270, 0);
		}
		else if(Pattern[Index + 1].y < Pattern[Index].y)
		{

			this.transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		Debug.Log("normal:" + (Pattern[Index + 1] - Pattern[Index]).normalized.ToString());
		*/
		this.transform.position = Vector3.Lerp(
			Pattern[Index], 
			Pattern[Index + 1], 
			Lerp);


		transform.rotation = Quaternion.Slerp(transform.rotation, RotateDirectionLookAt, Time.deltaTime * 6);
		
		//Debug.Log(Pattern[Index].ToString() + " " + Pattern[Index + 1].ToString() + " " + Lerp.ToString() + " " + amount.ToString());
		
		/*float d = Vector3.Distance(ObjectRef.transform.position, CharacterRef.transform.position);
		if(d <= 16)
		{
			StarsOwned -= 3;
			//ResetCharacter();
			//Debug.Log("EVIL KILL");
			//CharacterRef.GetComponent<CharacterControllerScript>().
		}*/
			

	
	}
}
