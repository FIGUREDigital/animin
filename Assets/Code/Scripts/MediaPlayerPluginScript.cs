using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices; // Don't forget this!

public class MediaPlayerPluginScript : MonoBehaviour
{
	void Start () 
	{
		// not necessary now, but we'll use it later
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_initMediaPlayer();
		}
	}

	void Update()
	{
		SetVideo("WTF VIDEO");
	}

	public void Play() {
	}
	
	public void Pause() {
	}


	[DllImport("__Internal")]
	private static extern void _setVideo(string filename);
	
	public void SetVideo(string filename) 
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setVideo(filename);
		}
	}

	[DllImport("__Internal")]
	private static extern void _initMediaPlayer();
}