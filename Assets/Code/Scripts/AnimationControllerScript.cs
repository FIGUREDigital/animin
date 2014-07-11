using UnityEngine;
using System.Collections.Generic;

public enum AnimationHappyId
{
	None = 0,
	Happy1,
	Happy2,
	Happy3,
	Happy4,
	Sad1,
	Sad2,
	Sad3,
	Sad4,
}

public class AnimationControllerScript : MonoBehaviour 
{
	protected Animator animator;
	private float TimeInIdleState;
	private bool HoldingWeightAnimationUp;

	public AnimationHappyId IsHappy 
	{
		get
		{
			AnimationHappyId id = AnimationHappyId.None;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_01")) id = AnimationHappyId.Happy1;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_02")) id = AnimationHappyId.Happy2;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_03")) id = AnimationHappyId.Happy3;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_04")) id = AnimationHappyId.Happy4;

			return id;
		}
		
		set
		{
			animator.SetInteger("IsHappy", (int)value );
		}
	}

	public int IsInPortalStage 
	{
		get
		{

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump_in_portal")) return 1;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump_out_portal")) return 2;

			return 0;
		}
		
		set
		{
			animator.SetInteger("EnterPotal", (int)value );
		}
	}






	public bool IsCelebrate
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("celebrate");
		}
		
		set
		{
			animator.SetBool("IsCelebrate", value );
		}
	}

	
	public bool IsLookingCamera
	{
		get
		{
			return GetComponent<ObjectLookAtDeviceScript>().IsActiveTimer > 0;
		}
		
		set
		{
			if(value)
				GetComponent<ObjectLookAtDeviceScript>().IsActiveTimer = UnityEngine.Random.Range(4.0f, 6.0f);
			else
				GetComponent<ObjectLookAtDeviceScript>().IsActiveTimer = 0;
		}
	}



	public bool IsAngry
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("angry");
		}
		
		set
		{
			animator.SetBool("IsAngry", value );
		}
	}

	public bool IsDance
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("dance");
		}
		
		set
		{
			animator.SetBool("IsDance", value );
		}
	}

	public bool IsEvolving
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("evolve_react");
		}
		
		set
		{
			animator.SetBool("IsEvolving", value );
		}
	}

	public bool IsHungry
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("hungry_loop") || animator.GetAnimatorTransitionInfo(0).IsName("hungry_loop") ;
		}
		
		set
		{
			animator.SetBool("IsHungry", value );
		}
	}

	public bool IsNo
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("no");
		}
		
		set
		{
			animator.SetBool("IsNo", value );
		}
	}

	public bool IsFull
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("full");
		}
		
		set
		{
			animator.SetBool("IsFull", value );
		}
	}


	public bool IsPat
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("pat_react");
		}
		
		set
		{
			animator.SetBool("IsPat", value );
		}
	}

	public bool IsNotWell
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("unwell") || animator.GetAnimatorTransitionInfo(0).IsName("unwell");
		}
		
		set
		{
			animator.SetBool("IsNotWell", value );
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

	public bool IsSad
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("idle_sad") || animator.GetAnimatorTransitionInfo(0).IsName("idle_sad");
		}
		
		set
		{
			animator.SetBool("IsSad", value );
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

	public bool IsIdle
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("idle_stand");
		}
	}

	public bool IsWakingUp
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("Transition Animation: Sleep to Idle");
		}
	}

	public bool IsIdleLook1
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("look_01");
		}
		
		set
		{
			animator.SetBool("IsIdleLook1", value );
		}
	}

	public bool IsIdleLook2
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("look_02");
		}
		
		set
		{
			animator.SetBool("IsIdleLook2", value );
		}
	}

	public bool IsIdleLook3
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("look_03");
		}
		
		set
		{
			animator.SetBool("IsIdleLook3", value );
		}
	}

	/*public bool IsIdleLook4
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("look_04");
		}
		
		set
		{
			animator.SetBool("IsIdleLook4", value );
		}
	}*/

	public bool IsIdleWave
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("IsIdleWave");
		}
		
		set
		{
			animator.SetBool("IsIdleWave", value );
		}
	}

	public bool IsSleeping
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep");
		}
		
		set
		{
			animator.SetBool("IsSleeping", value );
		}
	}

	public bool IsThrowing
	{
		get
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsName("throw");
		}
		
		set
		{
			animator.SetBool("IsThrowing", value );
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
			if(value == true) HoldingWeightAnimationUp = true;
			else HoldingWeightAnimationUp = false;

			animator.SetBool("IsHoldingItem", value );
		}
	}


	public bool IsAnyIdleAnimationPlaying()
	{
		if(IsIdle 
		        || IsIdleLook1 
		        || IsIdleLook2 
		        || IsIdleLook3 
		       
		        || IsIdleWave
		   || IsLookingCamera)
		{
			return true;
		}

		return false;
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

	public bool IsWalking
	{
		set
		{
			animator.SetBool("IsWalking", value);
		}
	}


	// Update is called once per frame
	void Update () 
	{

		if(HoldingWeightAnimationUp && animator.GetLayerWeight(1) >= 1)
		{
		}
		else if(!HoldingWeightAnimationUp && animator.GetLayerWeight(1) <= 0)
		{
		}
		else if(HoldingWeightAnimationUp)
		{
			float value = animator.GetLayerWeight(1) + Time.deltaTime * 3;
			if(value >= 1) value = 1;
			animator.SetLayerWeight(1, value);
		}
		else 
		{
		
			float value = animator.GetLayerWeight(1) - Time.deltaTime * 3;
			if(value <= 0) value = 0;
			animator.SetLayerWeight(1, value);

		}

		if(IsIdle && !IsLookingCamera)
		{
			TimeInIdleState += Time.deltaTime;
			if(TimeInIdleState >= 4.0f)
			{
				int random = UnityEngine.Random.Range(0, 15);
				if(random == 0) IsIdleLook1 = true;
				else if(random == 1) IsIdleLook2 = true;
				else if(random == 2) IsIdleLook3 = true;
				else if(random == 3) IsIdleWave = true;
				else if(random >= 4 && random <= 10) IsLookingCamera = true;
				else
				{
					CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();


					if(script.Happy >= (12.5f * 7)) 
					{
						IsHappy = AnimationHappyId.Happy4;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy4);
					}
					else if(script.Happy >= (12.5f * 6)) 
					{
						IsHappy = AnimationHappyId.Happy3;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy3);
					}
					else if(script.Happy >= (12.5f * 5)) 
					{
						IsHappy = AnimationHappyId.Happy2;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy2);
					}
					else if(script.Happy >= (12.5f * 4)) 
					{
						IsHappy = AnimationHappyId.Happy1;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy1);
					}
					else if(script.Happy >= (12.5f * 3)) 
					{
						IsHappy = AnimationHappyId.Sad1;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad1);
					}
					else if(script.Happy >= (12.5f * 2)) 
					{
						IsHappy = AnimationHappyId.Sad2;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad2);
					}
					else if(script.Happy >= (12.5f * 1)) 
					{
						IsHappy = AnimationHappyId.Sad3;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad3);
					}
					else 
					{
						IsHappy = AnimationHappyId.Sad4;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad4);
					}
				}

				TimeInIdleState = 0;
			}
		}
		else
		{
			TimeInIdleState = 0;
		}
	}

	void LateUpdate()
	{
		IsEating = false;
		IsTakingPill = false;
		IsTickled = false;
		IsIdleLook1 = false;
		IsIdleLook2 = false;
		IsIdleLook3 = false;
	
		IsIdleWave = false;
		IsThrowing = false;
		IsHappy = AnimationHappyId.None;
		IsFull = false;
	
		IsCelebrate = false;
		IsAngry = false;
		IsDance = false;
		IsEvolving = false;
		IsNo = false;
		IsPat = false;

		IsEating = false;	
	}
}


