using UnityEngine;
using System.Collections;

public class SoundEngineScript : MonoBehaviour 
{
	private AudioClip[] GenericSounds;
	private AudioClip[,] CreatureSounds;

	// Use this for initialization
	void Start () 
	{
		GenericSounds = new AudioClip[(int)GenericSoundId.Count];
		GenericSounds[(int)GenericSoundId.DropFood] = Resources.Load("Sounds/Items/Food_Drop") as AudioClip;
		GenericSounds[(int)GenericSoundId.ItemPickup] = Resources.Load("Sounds/Items/Item_Pick_Up") as AudioClip;
		GenericSounds[(int)GenericSoundId.DropMeds] = Resources.Load("Sounds/Items/Meds_Drop") as AudioClip;
		GenericSounds[(int)GenericSoundId.DropItem] = Resources.Load("Sounds/Items/Item_Drop") as AudioClip;


		CreatureSounds = new AudioClip[(int)CreatureTypeId.Count, (int)CreatureSoundId.Count];
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Celebrate] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@celebrate") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.EatPill] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@eat_pill") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Feed] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Feed") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Happy1] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@happy_01") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Happy2] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@happy_02") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Happy3] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@happy_03") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Happy4] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@happy_04") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Hungry] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Hungry") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.JumbInPortal] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@ump_in_portal") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.JumbOutPortal] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@ump_out_portal") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.No] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@No") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Sad1] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Sad01") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Sad2] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Sad02") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Sad3] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Sad03") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Sad4] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Sad04") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.SleepToIdle] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@sleep_to_idle_stand") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Throw] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Throw") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Tickle] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Tickle") as AudioClip;
		CreatureSounds[(int)CreatureTypeId.TBOBaby, (int)CreatureSoundId.Unwell] = Resources.Load("Sounds/TboBabyAnims/tbo_baby@Unwell") as AudioClip;

	}

	public void Play(GenericSoundId id)
	{
		this.audio.PlayOneShot(GenericSounds[(int)id]);
	}

	public void Play(CreatureTypeId creatureId, CreatureSoundId soundId)
	{
		this.audio.PlayOneShot(CreatureSounds[(int)creatureId, (int)soundId]);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

public enum GenericSoundId
{
	DropItem,
	DropFood,
	DropMeds,
	ItemPickup,
	Count,
}

public enum CreatureSoundId
{
	Celebrate,
	EatPill,
	Feed,
	Happy1,
	Happy2,
	Happy3,
	Happy4,
	Hungry,
	JumbInPortal,
	JumbOutPortal,
	No,
	Sad1,
	Sad2,
	Sad3,
	Sad4,
	SleepToIdle,
	Throw,
	Tickle,
	Unwell,


	Count,
}

public enum CreatureTypeId
{
	TBOBaby,
	TBOMiddle,
	TBOAdult,
	
	
	Count,
}
