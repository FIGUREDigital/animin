using UnityEngine;
using System.Collections;

public enum ApertureEvents {
	OnPhoto,
	OnCaptureComplete,
	OnSavedToDisk,
	OnAcceptedPreview,
	OnCancelledPreview
}

namespace ApertureInternal {

	public class ApertureEvents {
		public delegate void OnPhoto();
		public OnPhoto onPhoto;

		public delegate void OnCaptureComplete();
		public OnCaptureComplete onCaptureComplete;

		public delegate void OnSavedToDisk(string path);
		public OnSavedToDisk onSavedToDisk;

		public delegate void OnAcceptedPreview();
		public OnAcceptedPreview onAcceptedPreview;

		public delegate void OnCancelledPreview();
		public OnCancelledPreview onCancelledPreview;

		public void Clear() {
			onPhoto = null;
			onCaptureComplete = null;
			onSavedToDisk = null;
			onAcceptedPreview = null;
			onCancelledPreview = null;
		}
	}

}