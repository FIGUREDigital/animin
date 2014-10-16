using UnityEngine;
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

		Models[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Baby] = "Prefabs/ke_baby";
		Models[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Kid] = "Prefabs/ke_kid";
		Models[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Adult] = "Prefabs/ke_adult";

		Models[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Baby] = "Prefabs/ma_baby";
        Models[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Kid] = "Prefabs/ma_kid";
        Models[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Adult] = "Prefabs/ma_adult";

        Models[(int)AniminId.Pi, (int)AniminEvolutionStageId.Baby] = "Prefabs/pi_baby";
        Models[(int)AniminId.Pi, (int)AniminEvolutionStageId.Kid] = "Prefabs/pi_kid";
        Models[(int)AniminId.Pi, (int)AniminEvolutionStageId.Adult] = "Prefabs/pi_adult";

		AnimationLists = new AnimatorOverrideController[(int)AniminId.Count, (int)AniminEvolutionStageId.Count];
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"TBOBabyAnimations");
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"TBOKidAnimations");
		AnimationLists[(int)AniminId.Tbo, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"TBOAdultAnimations");
	
		AnimationLists[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"KelsiBabyAnimations");
		AnimationLists[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"KelsyKidAnimations");
		AnimationLists[(int)AniminId.Kelsey, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"KelsiAdultAnimations");

        AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiBabyAnimations");
		AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiKidAnimations");
		AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiAdultAnimations");

        AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiBabyAnimations");
		AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiKidAnimations");
		AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiAdultAnimations");

        AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiBabyAnimations");
		AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiKidAnimations");
		AnimationLists[(int)AniminId.Mandi, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/MandiAdultAnimations");

        AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Baby] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiBabyAnimations");
		AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Kid] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiKidAnimations");
		AnimationLists[(int)AniminId.Pi, (int)AniminEvolutionStageId.Adult] = Resources.Load<AnimatorOverrideController>(@"AnimControllers/PiAdultAnimations");

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

		Debug.Log("instance.transform = " + instance.transform);
		Debug.Log("instance.transform.parent = " + instance.transform.parent);

		if (UIGlobalVariablesScript.Singleton == null)
			Debug.Log("PANIC");

		instance.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform;

		if(CurrentModel != null)
		{
		
			CurrentModel.transform.parent = null;
			Destroy(CurrentModel);
		}

		instance.transform.localPosition = Vector3.zero;
		instance.transform.localScale = scale;

		instance.transform.localRotation = Quaternion.identity;

		CurrentModel = instance;

		CurrentModel.GetComponent<Animator>().runtimeAnimatorController = AnimationLists[(int)animinId, (int)id];


		UIGlobalVariablesScript.Singleton.SoundEngine.LoadSoundsForAnimin(animinId, id);
		/*for(int i=0;i<TBOAdultAnimations.clips.Length;++i)
		{
			CurrentModel.GetComponent<Animator>().runtimeAnimatorController
			overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
		}
		
		animator.runtimeAnimatorController = overrideController;
*/


		if (UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>()!=null)
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().SetCharacter(instance);

	}
}

public enum AniminId
{
	Pi = 0,
	Tbo = 1,
	Kelsey = 2,
	Mandi = 3,
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