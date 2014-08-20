using UnityEngine;
using System.Collections;

public class ApertureEditor : MonoBehaviour, IAperture {


	private Aperture _aperture;

	private Texture2D photo;
	private bool _preview;
	private bool _saveToTexture = false;


	void Start() {
		_aperture = gameObject.GetComponent<Aperture>();
		photo = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		_preview = false;
	}


	public void SetSaveToPhotoLibrary(bool shouldSave) {}
	public void SetSaveToDisk(bool shouldSave) {}

	public void SetSaveToTexture(bool shouldSave) {
		_saveToTexture = shouldSave;
	}
	public void SetTargetTextureID(int texID) {}

	public void SetPreviewAnimation(PreviewAnimation style) {}
	public void SetPauseUnityOnPreview(bool shouldPause) {}

	public void Photo() {
		StartCoroutine(_photo());
	}

	private IEnumerator _photo() {
		yield return new WaitForEndOfFrame();

		if (_saveToTexture) {
			_aperture.targetTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
			_aperture.targetTexture.Apply();
		} else {
			
			photo.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
			photo.Apply();
		}

		yield return null;
		SendMessage("OnComplete", "path", SendMessageOptions.DontRequireReceiver);

	}

	
	public void ShowPreview() {
		Debug.Log("Showing preview");
		_preview = true;
	}

	public void HidePreview() {
		Debug.Log("Hide preview");
		_preview = false;
	}
	
	void OnGUI() {
		if (_preview) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), photo);

			if (GUI.Button(new Rect(10f, Screen.height-60f, 100f, 50f), "Cancel")) {
				SendMessage("CancelPreview", SendMessageOptions.DontRequireReceiver);
				HidePreview();
			}

			if (GUI.Button(new Rect(Screen.width-110f, Screen.height-60f, 100f, 50f), "Use")) {
				SendMessage("AcceptPreview", SendMessageOptions.DontRequireReceiver);
				HidePreview();
			}
		}
	}


	public void Destroy() {
		Destroy(photo);
		photo = null;
	}

}
