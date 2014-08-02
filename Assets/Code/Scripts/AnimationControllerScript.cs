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
	Happy5,
}

public class AnimationControllerScript : MonoBehaviour 
{
	protected Animator animator;
	private float TimeInIdleState;
	private bool HoldingWeightAnimationUp;
	private float TimeToPlayIdleSoundFX = 6;
	float LengthOfHungryUnwellSadAnimation;
	HungrySadUnwellLoopId sadUnwellLoopState;

	public AnimationHappyId IsHappy 
	{
		get
		{
			AnimationHappyId id = AnimationHappyId.None;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_01")) id = AnimationHappyId.Happy1;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_02")) id = AnimationHappyId.Happy2;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_03")) id = AnimationHappyId.Happy3;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_04")) id = AnimationHappyId.Happy4;
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("happy_05")) id = AnimationHappyId.Happy5;

			return id;
		}
		
		set
		{
			animator.SetInteger("IsHappy", (int)value );
		}
	}

	public bool IsEnterPortal 
	{
		get
		{

			return animator.GetCurrentAnimatorStateInfo(0).IsName("jump_in_portal") || animator.GetBool("IsEnterPortal");
		}
		
		set
		{
			animator.SetBool("IsEnterPortal", value );
		}
	}

	public bool IsJumbing 
	{
		get
		{
			
			return animator.GetCurrentAnimatorStateInfo(0).IsName("IsJumbing") || animator.GetBool("IsJumbing");
		}
		
		set
		{
			animator.SetBool("IsJumbing", value );
		}
	}

	public bool IsExitPortal 
	{
		get
		{
			
			return animator.GetCurrentAnimatorStateInfo(0).IsName("jump_out_portal")|| animator.GetBool("IsExitPortal");
		}
		
		set
		{
			animator.SetBool("IsExitPortal", value );
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
			return animator.GetCurrentAnimatorStateInfo(0).IsName("pat_react") || animator.GetBool("IsPat");
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

//	public bool IsSad
//	{
//		get
//		{
//			return animator.GetCurrentAnimatorStateInfo(0).IsName("idle_sad") || animator.GetAnimatorTransitionInfo(0).IsName("idle_sad");
//		}
//		
//		set
//		{
//			animator.SetBool("IsSad", value );
//		}
//	}

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
			return animator.GetCurrentAnimatorStateInfo(0).IsName("tickle") || animator.GetBool("IsTickled");
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

		/*if(Input.GetKeyDown(KeyCode.D))
		{
			Debug.Log("ANIM!!");
			//animator.Play(
			//	Animator.StringToHash("PortalLayer.jumbin")
			//	);
			//animator.SetLayerWeight(2, 0);
			animator.SetLayerWeight(2, 1);
			//animator.CrossFade("PortalLayer.jumbin", 0.4f);
			//animator.Play("PortalLayer.jumbin", 2);
			animator.SetBool("ExtraLayerPortal", true);
		}*/

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

		CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();


		if(IsHungry || IsNotWell)
		{
			LengthOfHungryUnwellSadAnimation -= Time.deltaTime;
			if(LengthOfHungryUnwellSadAnimation <= 0)
			{
				IsNotWell = false;
				IsHungry = false;
				TimeInIdleState = 0;
			}
		}

		if(IsIdle && !script.IsMovingTowardsLocation && (script.ObjectHolding == null) && !IsLookingCamera && script.CurrentAction != ActionId.EnterPortalToAR && script.CurrentAction != ActionId.EnterPortalToNonAR)
		{
			TimeInIdleState += Time.deltaTime;
			if(TimeInIdleState >= 2.0f)
			{
				int randomAnimationGroup = UnityEngine.Random.Range(0, 5);
				switch(randomAnimationGroup)
				{
				case 0:
				{
					IsLookingCamera = true;
					break;
				}
				case 1:
				case 2:
				{
					int random = UnityEngine.Random.Range(0, 4);
					if(random == 0) 
					{
						IsIdleLook1 = true;
					}
					else if(random == 1) 
					{
						IsIdleLook2 = true;
					}
					else if(random == 2) 
					{
						IsIdleLook3 = true;
					}
					else if(random == 3) 
					{
						IsIdleWave = true;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.IdleWave);
					}

					break;
				}
				case 3:
				case 4:
				{
					if(randomAnimationGroup == 4 && (script.Hungry <= 30 || script.Health <= 30))
					{
						List<int> randomHungryOrUnwell = new List<int>();
						if(script.Hungry <= 30) randomHungryOrUnwell.Add(0);
						if(script.Health <= 30) randomHungryOrUnwell.Add(1);

						if(randomHungryOrUnwell[Random.Range(0, randomHungryOrUnwell.Count)] == 0)
						{
							IsHungry = true;
							//TimeForNextHungryUnwellSadAnimation = UnityEngine.Random.Range(15.0f, 20.0f);
							LengthOfHungryUnwellSadAnimation = UnityEngine.Random.Range(3.0f, 5.0f);
							//sadUnwellLoopState = HungrySadUnwellLoopId.PlayHungry;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Hungry);
						}
						else
						{
							IsNotWell = true;
							//TimeForNextHungryUnwellSadAnimation = UnityEngine.Random.Range(15.0f, 20.0f);
							LengthOfHungryUnwellSadAnimation = UnityEngine.Random.Range(3.0f, 5.0f);
							//sadUnwellLoopState = HungrySadUnwellLoopId.PlayUnwell;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Unwell);
						}

					}
					else
					{

						Debug.Log("HAPPY: " + script.Happy.ToString());
						List<AnimationHappyId> animationList = new List<AnimationHappyId>();
						for(int a=0;a<HappyStateRange.HappyStates.Length;++a)
						{
							if(script.Happy >= HappyStateRange.HappyStates[a].Min && script.Happy <= HappyStateRange.HappyStates[a].Max)
							{
								animationList.Add(HappyStateRange.HappyStates[a].Id);
							}
						}
						
						AnimationHappyId finalAnimationId = animationList[Random.Range(0, animationList.Count)];
						Debug.Log("finalAnimationId: " + finalAnimationId.ToString());

						if(finalAnimationId == AnimationHappyId.Happy5) 
						{
							IsHappy = AnimationHappyId.Happy5;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy4);
						}
						else if(finalAnimationId == AnimationHappyId.Happy4) 
						{
							IsHappy = AnimationHappyId.Happy4;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy4);
						}
						else if(finalAnimationId == AnimationHappyId.Happy3) 
						{
							IsHappy = AnimationHappyId.Happy3;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy3);
						}
						else if(finalAnimationId == AnimationHappyId.Happy2) 
						{
							IsHappy = AnimationHappyId.Happy2;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy2);
						}
						else if(finalAnimationId == AnimationHappyId.Happy1) 
						{
							IsHappy = AnimationHappyId.Happy1;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Happy1);
						}
						else if(finalAnimationId == AnimationHappyId.Sad1) 
						{
							IsHappy = AnimationHappyId.Sad1;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad1);
						}
						else if(finalAnimationId == AnimationHappyId.Sad2) 
						{
							IsHappy = AnimationHappyId.Sad2;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad2);
						}
						else if(finalAnimationId == AnimationHappyId.Sad3) 
						{
							IsHappy = AnimationHappyId.Sad3;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad3);
						}
						else if(finalAnimationId == AnimationHappyId.Sad4) 
						{
							IsHappy = AnimationHappyId.Sad4;
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(script.CreaturePlayerId, CreatureSoundId.Sad4);
						}
					}

					break;
				}
				
				}


				TimeInIdleState = 0;
			}
		}
		else
		{
			TimeInIdleState = 0;
		}

		if(IsIdle && !IsHungry && !IsNotWell)
		{
			TimeToPlayIdleSoundFX -= Time.deltaTime;

			if(TimeToPlayIdleSoundFX <= 0)
			{
				int randomId = UnityEngine.Random.Range(0, 7);
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CreaturePlayerId,
					(CreatureSoundId)((int)CreatureSoundId.RandomTalk1 + randomId ));

				TimeToPlayIdleSoundFX = UnityEngine.Random.Range(6.0f, 9.0f);
			}
		}
	}

	void LateUpdate()
	{
		IsEating = false;
		IsTakingPill = false;
		//IsTickled = false;

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("tickle"))
			IsTickled = false;

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("pat_react"))
			IsPat = false;

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
		//IsPat = false;

		IsEating = false;

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump_out_portal"))
			IsExitPortal = false;

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("jump_in_portal"))
			IsEnterPortal = false;

	}

}


