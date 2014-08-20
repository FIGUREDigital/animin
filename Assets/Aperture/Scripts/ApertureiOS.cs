using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ApertureiOS : MonoBehaviour, IAperture {

	[DllImport("__Internal")]
	private static extern void _setSaveToCameraRoll(bool shouldSave);

	public void SetSaveToPhotoLibrary(bool shouldSave) {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setSaveToCameraRoll(shouldSave);
		}
	}


	[DllImport("__Internal")]
	private static extern void _setSaveToDisk(bool shouldSave);

	public void SetSaveToDisk(bool shouldSave) {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setSaveToDisk(shouldSave);
		}
	}

	
	[DllImport("__Internal")]
	private static extern void _setSaveToTexture(bool shouldSave);

	public void SetSaveToTexture(bool shouldSave) {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setSaveToTexture(shouldSave);
		}
	}


	[DllImport("__Internal")]
	private static extern void _setTargetTextureID(int texID);

	public void SetTargetTextureID(int texID) {
		Debug.Log("Setting target texture ID: " + texID);
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setTargetTextureID(texID);
		}
	}


	[DllImport("__Internal")]
	private static extern void _setPreviewAnimation(int animation);

	public void SetPreviewAnimation(PreviewAnimation style) {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setPreviewAnimation((int)style);
		}
	}


	[DllImport("__Internal")]
	private static extern void _setPauseUnityOnPreview(bool shouldPause);

	public void SetPauseUnityOnPreview(bool shouldPause) {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_setPauseUnityOnPreview(shouldPause);
		}
	}


	[DllImport("__Internal")]
	private static extern void _photo();

	public void Photo() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_photo();
		}
	}


	[DllImport("__Internal")]
	private static extern void _showPreview();
	
	public void ShowPreview() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_showPreview();
		}
	}


	[DllImport("__Internal")]
	private static extern void _hidePreview();

	public void HidePreview() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_hidePreview();
		}
	}


	[DllImport("__Internal")]
	private static extern void _destroy();

	public void Destroy() {
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			_destroy();
		}
	}

}

