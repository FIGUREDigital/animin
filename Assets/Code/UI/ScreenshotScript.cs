using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ScreenshotScript : MonoBehaviour {

	// Use this for initialization
	void OnClick()
	{
		StartCoroutine(CaptureScreenshot());
	}

	IEnumerator CaptureScreenshot()
	{
		Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
		yield return new WaitForEndOfFrame();
		screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
		screenshot.Apply();
		var bytes = screenshot.EncodeToPNG();
		Destroy (screenshot);
		// For testing purposes, also write to a file in the project folder
		//			File.WriteAllBytes(Application.dataPath + "screenshot.png", bytes);
		string path = "/private/var/mobile/Media/DCIM/screenshot" + DateTime.Now.ToString() + ".png";
		Debug.Log("Photo saved to: " + path);
		File.WriteAllBytes(path, bytes);
	}
	
}