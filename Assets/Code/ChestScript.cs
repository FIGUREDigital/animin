using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour 
{
	private enum AnimationStateId
	{
		None = 0,
		StartingUp,
		OpeningLid,
		LidOpened,
		ThrowItemsOut,
		FadeOut,
	}

	private AnimationStateId State;
	private float Timer = 6.0f;

	public GameObject[] Coins;

	// Use this for initialization
	void Start () 
	{
		//State = AnimationStateId.StartingUp;

	}

//	void LateUpdate()
//	{
//
//	}
	
	// Update is called once per frame
	void Update () 
	{

		if(State == AnimationStateId.None)
		{
			if(Input.GetButtonUp("Fire1"))
			{
				RaycastHit hitInfo;
				bool hadRayCollision = false;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hitInfo))
				{
					if(hitInfo.collider == this.collider)
					{
						State = AnimationStateId.StartingUp;
					}
				}
			}
		}

		//this.GetComponent<Animator>().SetBool("Open", false);

		switch(State)
		{
			case AnimationStateId.None:
			{
				

				break;

			}

		case AnimationStateId.StartingUp:
		{

			//Timer -= Time.deltaTime;
			//if(Timer <= 0)
			{
				this.GetComponent<Animator>().SetBool("Open", true);
				State = AnimationStateId.OpeningLid;
				
				for(int i=0;i<Coins.Length;++i)
				{
					//GameObject resource = Resources.Load<GameObject>(@"Prefabs/Lightbulb");
					
					GameObject zef = /*Instantiate(resource) as GameObject;//*/ UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().SpawnZef(Vector3.zero);
					
					zef.transform.parent = Coins[i].transform.GetChild(0).transform;
					zef.transform.localPosition = Vector3.zero;	
					zef.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
					zef.transform.localScale *= 0.8f;
					
					
					//zef.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
					
					Coins[i].GetComponent<Animator>().SetBool("play", true);
					
				}
			}

			break;
			
		}

			case AnimationStateId.OpeningLid:
			{
			Timer -= Time.deltaTime;
			if(Timer <= 0)
			{
				for(int i=0;i<Coins.Length;++i)
				{
					Coins[i].transform.GetChild(0).transform.GetChild(0).GetComponent<SpinObjectScript>().enabled = true;
					Coins[i].transform.GetChild(0).transform.GetChild(0).transform.parent = 
						UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveWorld.transform;
					
				}

				Destroy(this.gameObject);
			}

//				for(int i=0;i<Coins.Length;++i)
//				{
//					Coins[i].GetComponent<Animator>().SetBool("play", false);
//					
//				}

				break;
			}
			case AnimationStateId.ThrowItemsOut:
			{


				break;
			}
			case AnimationStateId.LidOpened:
			{
				for(int i=0;i<6;++i)
				{
					GameObject zef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().SpawnZef(this.transform.localPosition);
					int sign1 = 1;
					int sign2 = 1;
					if(Random.Range(0, 2) == 0) sign1 = -1;
					if(Random.Range(0, 2) == 0) sign2 = -1;
					zef.AddComponent<AnimateChestObjectOutScript>().Destination = zef.transform.localPosition + new Vector3(Random.Range(2.5f, 4.8f) * sign1 , 0, Random.Range(2.5f, 4.8f) * sign2);
				}

				State = AnimationStateId.ThrowItemsOut;
			Timer = 3;
				break;
			}
		}
	
	}
}
