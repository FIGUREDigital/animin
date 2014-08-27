using UnityEngine;
using System.Collections;

public class CharacterSwapManagementScript : MonoBehaviour 
{
	//private AnimationClip[,] AnimationsPerModel;
	private string[] Models;
	public GameObject CurrentModel;
	public AnimatorOverrideController TBOAdultAnimations;


	void Awake()
	{
		//AnimationsPerModel = new AnimationClip[(int)CreatureTypeId.Count, (int)CreatureAnimationId.Count];

		//LoadAnimations(CreatureTypeId.TBOAdult, "Models/TBO/Adult", "/tbo_adult@");
		//LoadAnimations(CreatureTypeId.TBOAdult, "Models/TBO/Kid", "/tbo_kid@");

		Models = new string[(int)CreatureTypeId.Count];
		Models[(int)CreatureTypeId.TBOBaby] = "Prefabs/tbo_baby";
		Models[(int)CreatureTypeId.TBOKid] = "Prefabs/tbo_kid";
		Models[(int)CreatureTypeId.TBOAdult] = "Prefabs/tbo_adult";
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

	public void LoadCharacter(CreatureTypeId id)
	{
		Object resource = Resources.Load(Models[(int)id]);

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
		CurrentModel.GetComponent<Animator>().runtimeAnimatorController = TBOAdultAnimations;
		
		/*for(int i=0;i<TBOAdultAnimations.clips.Length;++i)
		{
			CurrentModel.GetComponent<Animator>().runtimeAnimatorController
			overrideController[clipOverride.clipNamed] = clipOverride.overrideWith;
		}
		
		animator.runtimeAnimatorController = overrideController;
*/



		UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().SetCharacter(instance);

	}

	/*private void LoadAnimations(CreatureTypeId creatureId, string basePath, string animPrefix)
	{
		for(int i=0;i<(int)CreatureAnimationId.Count;++i)
		{
			string animName = ((CreatureAnimationId)i).ToString();
			animName = animName.Substring(1);

			string finalPath = basePath + animPrefix + animName;
			//Debug.Log(finalPath);
			Object clip = Resources.Load(finalPath);
			if(clip == null) 
			{
				Debug.Log("ANIMATION MISSING: " + finalPath);
				continue;
			}

			AnimationsPerModel[(int)creatureId, i] = clip as AnimationClip;
		}
	}*/

	// Use this for initialization
	void Start () 
	{

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public enum CreatureTypeId
{
	TBOBaby = 0,
	TBOKid,
	TBOAdult,
	
	
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