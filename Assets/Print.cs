using UnityEngine;
using System.Collections;
using System.IO;

public class Print : MonoBehaviour 
{
	
	[SerializeField]
	private UITexture PhotoSaved;
	void OnClick()
	{
		Texture2D screenshot = Resources.Load<Texture2D>("printOut");
		var bytes = screenshot.EncodeToPNG();
		string filepath = Application.persistentDataPath + "/printOut.png";
		Debug.Log("Photo saved to: " + filepath);
		File.WriteAllBytes(filepath, bytes);
#if UNITY_IOS
		EtceteraBinding.saveImageToPhotoAlbum(filepath);
#elif UNITY_ANDROID
		EtceteraAndroid.saveImageToGallery(filepath,"printOut.png");
#endif
		PopPhotoSaved();

	}
	void PopPhotoSaved()
	{
		if (PhotoSaved != null && PhotoSaved.GetComponent<PhotoFadeOut> () != null) {
			PhotoSaved.gameObject.SetActive(true);
		}
	}
}
