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
		string path = Application.persistentDataPath + "/printOut.png";
		Debug.Log("Photo saved to: " + path);
		File.WriteAllBytes(path, bytes);
#if UNITY_IOS
		EtceteraBinding.saveImageToPhotoAlbum(path);
#elif UNITY_ANDROID
		EtceteraAndroid.saveImageToGallery(path,"printOut.png");
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
