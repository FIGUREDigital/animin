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
				Timer = 1.2f;

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
						GameObject zef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().SpawnZef(Vector3.zero);

						zef.GetComponent<SpinObjectScript>().enabled = false;
						
						zef.transform.parent = Coins[i].transform.GetChild(0).transform;
						zef.transform.localPosition = Vector3.zero;	
						zef.transform.localScale *= 0.8f;
						Coins[i].transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
						Coins[i].GetComponent<Animator>().SetBool("play", true);
						Coins[i] = zef;
						//Coins[i].transform.localScale = new Vector3(12, 12, 12);
						
					}

					Timer = 4;
					State = AnimationStateId.LidOpened;
				}

				

				break;
			}
			case AnimationStateId.ThrowItemsOut:
			{


				break;
			}
			case AnimationStateId.LidOpened:
			{
				Timer -= Time.deltaTime;
				if(Timer <= 0)
				{
					for(int i=0;i<Coins.Length;++i)
					{
						Coins[i].GetComponent<SpinObjectScript>().enabled = true;
						Coins[i].transform.parent = 
							UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveWorld.transform;
					}
					
					State = AnimationStateId.ThrowItemsOut;
					BeginFadeOut = true;
				}
	
				break;
			}
		}
	

		UpdateFading();
	}

	private void RecurviseSetAlpha(GameObject gameObject, Color alphaColor)
	{
		if(gameObject.renderer != null)
		{
			gameObject.renderer.material.shader = Shader.Find("Transparent/Diffuse"); 
			gameObject.renderer.material.color = alphaColor;
		}

		for(int i=0;i<gameObject.transform.childCount;++i)
		{
			RecurviseSetAlpha(gameObject.transform.GetChild(i).gameObject, alphaColor);
		}
	}

	private float Alpha = 1;
	private bool BeginFadeOut;
	private void UpdateFading()
	{
		if(BeginFadeOut)
		{
			bool destroy = false;


			Alpha -= Time.deltaTime *  3;
			if(Alpha <= 0) 
				{
				Alpha = 0;
					destroy = true;
				}

				RecurviseSetAlpha(this.gameObject, new Color(
					1,
					1,
					1,
				Alpha));


			if(destroy)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
