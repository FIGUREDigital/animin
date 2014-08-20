using UnityEngine;
using System.Collections;
using System;

[Flags]
public enum SaveMode {
	PhotoLibrary,
	Disk,
	DiskPreview,
	DiskPhotoLibraryPreview,
	Texture
}

public enum PreviewAnimation {
	None,
	CoverVertical,
	Crossfade
}

public class Aperture : MonoBehaviour {

	private IAperture _aperture;

	[SerializeField]
	private SaveMode saveMode;
	
		
	/// <summary>
	/// Should the preview show on capture complete?
	/// </summary>
	private bool showPreview;


	/// <summary>
	/// Should the photo save to the photo library automatically?
	/// </summary>
	private bool saveToPhotoLibrary = false;


	/// <summary>
	/// Should the photo save to a file on the device, other than the photo library?
	/// </summary>
	private bool saveToDisk = false;


	/// <summary>
	/// Should the photo save to a texture in Unity? 
	/// </summary>
	private bool saveToTexture = false;

	/// <summary>
	/// The texture to save into. 
	/// </summary>
	private Texture2D _targetTexture = null;
	public Texture2D targetTexture {
		get {
			return _targetTexture;
		}
		set {
			_targetTexture = value;
			_aperture.SetTargetTextureID(_targetTexture.GetNativeTextureID());
		}
	}

	/// <summary>
	/// If the preview is being shown, which animation style?
	/// </summary>
	[SerializeField]
	private PreviewAnimation previewAnimation;




	/// <summary>
	/// Should the photo save to a file on the device, other than the photo library?
	/// </summary>
	[SerializeField]
	private bool _pauseUnityOnPreview = false;
	private bool pauseUnityOnPreview {
		get { 
			return _pauseUnityOnPreview;
		}
		set {
			_pauseUnityOnPreview = value;
			_aperture.SetPauseUnityOnPreview(value);
		}
	}

	/// <summary>
	/// The event receiver.
	/// </summary>
	public GameObject eventReceiver;


	/// <summary>
	/// Delegates, for better performance when necessary.
	/// </summary>
	public ApertureInternal.ApertureEvents Events;


#region MonoBehavior

	void Awake() {
		// Initialize the handler
		#if UNITY_EDITOR
			_aperture = gameObject.AddComponent<ApertureEditor>();
		#elif UNITY_IOS 
			_aperture = gameObject.AddComponent<ApertureiOS>();
		#endif
		Events = new ApertureInternal.ApertureEvents();
	}


	void Start() {
		SetSaveMode(saveMode);
		SetPreviewAnimation(previewAnimation);
	}


	/// <summary>
	/// Sets the save mode.
	/// </summary>
	public void SetSaveMode(SaveMode newMode) {
		saveToDisk = 	newMode == SaveMode.Disk ||
						newMode == SaveMode.DiskPreview || 
						newMode == SaveMode.DiskPhotoLibraryPreview;

		saveToPhotoLibrary = 	newMode == SaveMode.PhotoLibrary ||
								newMode == SaveMode.DiskPhotoLibraryPreview;

		saveToTexture = newMode == SaveMode.Texture;

		showPreview = 	newMode == SaveMode.DiskPreview || 
						newMode == SaveMode.DiskPhotoLibraryPreview;

		_aperture.SetSaveToDisk(saveToDisk);
		_aperture.SetSaveToPhotoLibrary(saveToPhotoLibrary);
		_aperture.SetSaveToTexture(saveToTexture);

		pauseUnityOnPreview = _pauseUnityOnPreview;
	}


	/// <summary>
	/// Sets the preview animation.
	/// </summary>
	/// <param name="style">Animation style</param>
	public void SetPreviewAnimation(PreviewAnimation style) {
		previewAnimation = style;
		_aperture.SetPreviewAnimation(previewAnimation);
	}


	/// <summary>
	/// Destroys the native plugin instance
	/// </summary>
	void OnDestroy() {
		Events.Clear();
		_aperture.Destroy();
	}

#endregion



#region Capture

	/// <summary>
	/// Begin photo capture.
	/// </summary>
	public void Photo() {
		// If we're writing into a texture, make sure it exists
		if (saveMode == SaveMode.Texture && targetTexture == null) {
			Debug.Log("Target texture null?");
			return;
		}

		if (Events.onPhoto != null) {
			Events.onPhoto();
		}

		if (eventReceiver != null) {
			eventReceiver.SendMessage(ApertureEvents.OnPhoto.ToString(), SendMessageOptions.DontRequireReceiver);
		}

		_aperture.Photo();
	}

#endregion



#region Callbacks
	
	/// <summary>
	/// Fired when the photo capture is complete. 
	/// </summary>
	private void OnComplete(string path) {
		if (showPreview) {
			_aperture.ShowPreview();
		}

		if (Events.onCaptureComplete != null) {
			Events.onCaptureComplete();
		}

		if (eventReceiver != null) {
			eventReceiver.SendMessage(ApertureEvents.OnCaptureComplete.ToString(), SendMessageOptions.DontRequireReceiver);
		}
	}


	/// <summary>
	/// Fired when the photo has been saved to disk.
	/// </summary>
	/// <param name="path">Path to the file on disk.</param>
	private void SavedToDisk(string path) {
		if (saveToDisk) {

			if (Events.onSavedToDisk != null) {
				Events.onSavedToDisk(path);
			}

			if (eventReceiver != null) {
				eventReceiver.SendMessage(ApertureEvents.OnSavedToDisk.ToString(), path, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

#endregion



#region Preview control

	/// <summary>
	/// Fired when they user accepts the photo preview. The preview hides itself.
	/// </summary>
	private void AcceptPreview() {
		if (Events.onAcceptedPreview != null) {
			Events.onAcceptedPreview();
		}

		if (eventReceiver != null) {
			eventReceiver.SendMessage(ApertureEvents.OnAcceptedPreview.ToString(), SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// Fired when they user cancels the photo preview. This will be fired if 
	/// the preview is manually hidden with HidePreview.
	/// </summary>
	private void CancelPreview() {
		if (Events.onCancelledPreview != null) {
			Events.onCancelledPreview();
		}

		if (eventReceiver != null) {
			eventReceiver.SendMessage(ApertureEvents.OnCancelledPreview.ToString(), SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// Hides the preview. It is the responsibility of the preview to hide itself, 
	/// but this is available for other cases such as including a preview timeout.
	/// This gets fired if the user cancels the photo preview. 
	/// </summary>
	public void HidePreview() {
		_aperture.HidePreview();
	}

#endregion
	
}
