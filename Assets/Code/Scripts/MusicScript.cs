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
	void Update () {
	
	}

	public void PlayCube()
	{
		this.GetComponent<AudioSource>().clip = CubeMusic;
		this.GetComponent<AudioSource>().Play();
	}

	public void PlayGun()
	{
		this.GetComponent<AudioSource>().clip = GunMusic;
		this.GetComponent<AudioSource>().Play();
	}

	public void Stop()
	{
		this.GetComponent<AudioSource>().Stop(); 
	}
}
