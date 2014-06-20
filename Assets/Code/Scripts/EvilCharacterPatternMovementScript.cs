using UnityEngine;
using System.Collections;

public class EvilCharacterPatternMovementScript : MonoBehaviour 
{
	public float Speed;
	public float Lerp;
	public Vector3[] Pattern;
	public int Index;

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
