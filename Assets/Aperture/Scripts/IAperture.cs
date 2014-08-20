using UnityEngine;
using System.Collections;

public interface IAperture {
	
	void SetSaveToPhotoLibrary(bool shouldSave);
	void SetSaveToDisk(bool shouldSave);

	void SetSaveToTexture(bool shouldSave);
	void SetTargetTextureID(int texID);

	void SetPreviewAnimation(PreviewAnimation style);
	void SetPauseUnityOnPreview(bool shouldPause);

	void Photo();
	
	void ShowPreview();
	void HidePreview();

	void Destroy();
}
