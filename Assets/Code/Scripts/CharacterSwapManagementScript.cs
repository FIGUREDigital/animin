﻿using UnityEngine;
using System.Collections;

public enum EmotionId
{
	Default = 0,
	Blink,
	Happy1,
	Happy2,
	Happy3,
	Happy4,
	Happy5,
	Happy6,
	Count,

}

public class EmotionPerModelData
{
	public string[] TexturePaths;
}

public class CharacterSwapManagementScript : MonoBehaviour 
{
	//private AnimationClip[,] AnimationsPerModel;
	private string[,] Models;
	public GameObject CurrentModel;

	public AnimatorOverrideController TBOAdultAnimations;
	public AnimatorOverrideController[,] AnimationLists;


	void Awake()
	{
		//AnimationsPerModel = new AnimationClip[(int)CreatureTypeId.Count, (int)CreatureAnimationId.Count];

		//LoadAnimations(CreatureTypeId.TBOAdult, "Models/TBO/Adult", "/tbo_adult@");
		//LoadAnimations(CreatureTypeId.TBOAdult, "Models/TBO/Kid", "/tbo_kid@");
	
		Models = new string[(int)AniminId.Count, (int)AniminEvolutionStageId.Count];
		Models[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Baby] = "Prefabs/tbo_baby";
		Models[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Kid] = "Prefabs/tbo_kid";
		Models[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Adult] = "Prefabs/tbo_adult";

		Models[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Baby] = "Prefabs/ke_baby";
		Models[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Kid] = "Prefabs/ke_baby";
		Models[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Adult] = "Prefabs/ke_baby";



		AnimationLists = new AnimatorOverrideController[(int)AniminId.Count, (int)AniminEvolutionStageId.Count];
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"TBOBabyAnimations");
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"TBOKidAnimations");
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"TBOAdultAnimations");
	
		AnimationLists[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"KelsiBabyAnimations");
		AnimationLists[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"KelsiBabyAnimations");
		AnimationLists[(int)AniminId.Kelsi, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"KelsiBabyAnimations");

		
	}

	void OnGUI()
	{
//		if(GUI.Button(new Rect(0, 0, 200, 100), "TBOBaby"))
//		{
//			LoadCharacter(CreatureTypeId.TBOBaby);
//		}

//		if(GUI.Button(new Rect(250, 0, 200, 100), "TBOKid"))
//		{
//			LoadCharacter(CreatureTypeId.TBOKid);
//		}

//		if(GUI.Button(new Rect(500, 0, 200, 100), "TBOAdult"))
//		{
//			LoadCharacter(CreatureTypeId.TBOAdult);
//		}
	}

	public string GetModelPath(AniminId animinId, AniminEvolutionStageId id)
	{
		return Models[(int)animinId, (int)id];
	}

	public AnimatorOverrideController GetAnimationControlller(AniminId animinId, AniminEvolutionStageId id)
	{
		return AnimationLists[(int)animinId, (int)id];
	}

	public void LoadCharacter(AniminId animinId, AniminEvolutionStageId id)
	{
		Object resource = Resources.Load(Models[(int)animinId, (int)id]);

		GameObject instance = GameObject.Instantiate(resource) as GameObject;
		Vector3 scale = instance.transform.localScale;
		//RuntimeAnimatorController controller = CurrentModel.GetComponent<Animator>().runtimeAnimatorController;
		instance.transform.parent = CurrentModel.transform.parent;

		CurrentModel.transform.parent = null;
		//CurrentModel.SetActive(false);
		Destroy(CurrentModel);

		instance.transform.localPosition = Vector3.zero;
		instance.transform.localScale = scale;
		//instance.transform.position = Vector3.zero;
		instance.transform.localRotation = Quaternion.identity;
		//CurrentModel.GetComponent<Animator>().runtimeAnimatorController = controller;
		CurrentModel = instance;
//		TBOAdultAnimations.runtimeAnimatorController = CurrentModel.GetComponent<Animator>().runtimeAnimatorController;

		//AnimatorOverrideController overrideController = new AnimatorOverrideController();
		CurrentModel.GetComponent<Animator>().runtimeAnimatorController = AnimationLists[(int)animinId, (int)id];
		
		/*for(int i=0;i<TBOAdultAnimations.clips.Length;++i)
		{
			CurrentModel.GetComponent<Animator>().runtimeAnimatorController
			overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
		}
		
		animator.runtimeAnimatorController = overrideController;
*/



		UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().SetCharacter(instance);

	}
}

public enum AniminId
{
	Tbo = 0,
	Kelsi,
	Mandi,
	Pi,
	Count,
}

public enum AniminEvolutionStageId
{
	Baby = 0,
	Kid,
	Adult,

	Count,
}

public enum CreatureAnimationId
{
	_celebrate = 0,
	_dance,
	_eat_pill,
	_evolve_react,
	_feed,
	_happy_01,
	_happy_02,
	_happy_03,
	_happy_04,
	_happy_05,
	_hit,
	_hungry_loop,
	_idle_stand,
	_injection_react,
	_jump,
	_jump_in_portal,
	_jump_out_portal,
	_look_01,
	_look_02,
	_look_03,
	_no,
	_pat_react,
	_pick_up_object_idle,
	_run,
	_sad_01,
	_sad_02,
	_sad_03,
	_sad_04,
	_sleep,
	_sleep_to_idle_stand,
	_throw,
	_tickle,
	_unwell,
	_walk,
	_wave,

	Count
}