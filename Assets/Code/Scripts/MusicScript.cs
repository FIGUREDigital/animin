using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour 
{
	public AudioClip CubeMusic;
	public AudioClip GunMusic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!PlayerProfileData.ActiveProfile.Settings.AudioEnabled) 
		{
			if(this.GetComponent<AudioSource>().isPlaying)
				this.GetComponent<AudioSource>().Stop();
		}
		else
		{
			if(LooperPlaying && !this.GetComponent<AudioSource>().isPlaying)
				this.GetComponent<AudioSource>().Play();
		}
	}

	private bool LooperPlaying;

	public void PlayCube()
	{
		this.GetComponent<AudioSource>().clip = CubeMusic;
		LooperPlaying = true;
	}

	public void PlayGun()
	{
		this.GetComponent<AudioSource>().clip = GunMusic;
		LooperPlaying = true;
	}

	public void Stop()
	{
		this.GetComponent<AudioSource>().Stop(); 
		LooperPlaying = false;
	}
}
