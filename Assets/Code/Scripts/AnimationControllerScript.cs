using UnityEngine;
using System.Collections.Generic;

public class AnimationControllerScript : MonoBehaviour 
{
	protected Animator animator;
	public IdleStateId IdleState
	{
		get 
		{
			return (IdleStateId)animator.GetInteger("Idle Animation Id");
		}
	}
	private float TimeInIdleState;

	public bool IsSleeping
	{
		get
		{
			return IdleState == IdleStateId.Sleeping;
		}
	}

	public bool IsEating
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("feed");
		}

		set
		{
			animator.SetBool("IsEating", value );
		}
	}

	public bool IsTakingPill
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("eat_pill");
		}
		
		set
		{
			animator.SetBool("IsTakingPill", value );
		}
	}

	public bool IsTickled
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("tickle");
		}
		
		set
		{
			animator.SetBool("IsTickled", value );
		}
	}

	public bool IsHoldingItem
	{
		get
		{

			return animator.GetLayerWeight(1) > 0;
		}
		
		set
		{
			if(value == true) animator.SetLayerWeight(1, 1);
			else animator.SetLayerWeight(1, 0);

			animator.SetBool("IsHoldingItem", value );
		}
	}




	void Awake()
	{
		animator = GetComponent<Animator>();

	}



	// Use this for initialization
	void Start () 
	{
		 
	}

	public void SetMovementNormalized(float speed)
	{
		//Debug.Log(speed);
		animator.SetFloat("Movement", Mathf.Abs(speed));
	}

	public bool IsRunning
	{
		set
		{
			animator.SetBool("IsRunning", value);
		}
	}

	public void Wakeup()
	{
		SetAnimation(IdleStateId.Default);   
	}

	public void SetAnimation(IdleStateId animId)
	{
		Debug.Log("Changing state from: " + IdleState.ToString() + " to: " + animId.ToString());
		animator.SetInteger("Idle Animation Id", (int)animId);   
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(IdleState)
		{
			case IdleStateId.Default:
			{
				TimeInIdleState += Time.deltaTime;
				if(TimeInIdleState >= 6.0f)
				{
					List<int> randomIdles = new List<int>();
					randomIdles.Add((int)IdleStateId.Look1);
					randomIdles.Add((int)IdleStateId.Look2);
					randomIdles.Add((int)IdleStateId.Look3);
					randomIdles.Add((int)IdleStateId.Wave);

					IdleStateId newId = (IdleStateId)randomIdles[UnityEngine.Random.Range(0, randomIdles.Count)];
					
					SetAnimation(newId); 
					TimeInIdleState = 0;
				}

				break;
			}

			case IdleStateId.Look1:
			case IdleStateId.Look2:
			case IdleStateId.Look3:
			case IdleStateId.Wave:
			case IdleStateId.Eating:
			case IdleStateId.TakingPill:
			{

				if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
				   SetAnimation(IdleStateId.Default); 

				break;
			}


			case IdleStateId.Sleeping:
			{
				
				break;
			}

			case IdleStateId.Sad:
			{
				break;
			}
			case IdleStateId.Moving:
			{

				break;
			}
		}
	
	}

	void LateUpdate()
	{
		IsEating = false;
		IsTakingPill = false;
		IsTickled = false;
	}
}


public enum IdleStateId
{
	Default = 0,
	Look1 = 1,
	Look2 = 2,
	Look3 = 3,
	Wave = 4,
	Sad = 5,
	Sleeping = 6,
	Moving = 7,
	Eating = 8,
	TakingPill = 9,
}