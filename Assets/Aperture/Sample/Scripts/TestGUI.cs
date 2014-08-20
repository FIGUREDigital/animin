using UnityEngine;
using System.Collections;

public class TestGUI : MonoBehaviour {

	private Aperture ap;
	private bool shouldDraw = true;
	private string filePath = null;

	private Texture2D photo;
	public GameObject renderObject;

	// Use this for initialization
	void Start () {
		ap = GameObject.Find("Aperture").GetComponent<Aperture>();
		ap.Events.onPhoto += HideUI;
		ap.Events.onCaptureComplete += ShowUI;
		ap.Events.onSavedToDisk += GetFilePath;
		ap.Events.onAcceptedPreview += AcceptedPreview;
		ap.Events.onCancelledPreview += CancelledPreview;


#if UNITY_EDITOR
		photo = new Texture2D(Screen.width, Screen.height); 
#else
		// Texture format must be BGRA32 on iPad!
		photo = new Texture2D(Screen.width, Screen.height, TextureFormat.BGRA32, false);
#endif
	}


	void OnDestroy() {
		ap.Events.onPhoto -= HideUI;
		ap.Events.onCaptureComplete -= ShowUI;
		ap.Events.onSavedToDisk -= GetFilePath;
		ap.Events.onAcceptedPreview -= AcceptedPreview;
		ap.Events.onCancelledPreview -= CancelledPreview;
	}


	// Update is called once per frame
	void OnGUI() {
		if (shouldDraw) {

			if (GUI.Button(new Rect(10, 10, 100, 50), "Start")) {
				Application.LoadLevel("Menu");
			}

			if (GUI.Button(new Rect(Screen.width/3f, Screen.height-100f, Screen.width/3f, 75f), "Photo")) {
				ap.targetTexture = photo;
				ap.Photo();
			}

			if (!string.IsNullOrEmpty(filePath)) {
				GUI.Label(new Rect(Screen.width - 200f, 5f, 200f, 50f), filePath);
			}
		}
	
	}

	void HideUI() {
		Debug.Log("Hiding UI");
		shouldDraw = false;
		filePath = null;
	}

	void ShowUI() {
		shouldDraw = true;
		renderObject.renderer.material.mainTexture = photo;
	}


	void GetFilePath(string path) {
		filePath = path;
	}

	void AcceptedPreview() {
		shouldDraw = true;
	}

	void CancelledPreview() {
		shouldDraw = true;
	}




}
