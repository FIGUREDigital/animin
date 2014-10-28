using UnityEngine;
using System.Collections;

public class ArCameraManager : MonoBehaviour {

	#region Singleton
	
	private static ArCameraManager s_Instance;
	
	public static ArCameraManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new ArCameraManager();
			}
			return s_Instance;
		}
	}
	
	#endregion

	public GameObject Go;

	void OnLevelWasLoaded(int level) 
	{
		UIGlobalVariablesScript.Singleton.ARCamera = Go;
		UIGlobalVariablesScript.Singleton.DragableUI3DObject = Go.GetComponentInChildren<CameraModelScript>().gameObject;
	}
}
