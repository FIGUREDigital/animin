using UnityEngine;
using System.Collections;


public class TrackVuforiaScript : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;

	Vector3 SavedARPosition;

	//public GameObject ARSceneRef;
	public GameObject NonARSceneRef;
	public GameObject MainSceneRef;
	public GameObject CharacterRef;

	public static bool IsTracking;


	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}


		this.renderer.enabled = false;
		SavedARPosition = new Vector3(0, 0.0f, 0);
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	
	private void OnTrackingFound()
	{
		//Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

		//MainSceneRef.transform.parent = ARSceneRef.transform;
	
		//MainSceneRef.transform.localPosition = Vector3.zero;
		//MainSceneRef.transform.localRotation = Quaternion.identity;
		//MainSceneRef.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		//MainSceneRef.transform.position = Vector3.zero;
		//MainSceneRef.transform.rotation = Quaternion.identity;



		/*for (int i=0; i<this.transform.childCount; ++i) {
			this.transform.GetChild(i).gameObject.SetActive(true);
		}*/

		CharacterRef.transform.parent = null;

		NonARSceneRef.SetActive (false);
		MainSceneRef.SetActive(true);

		CharacterRef.transform.parent = MainSceneRef.transform;

		//CharacterRef.transform.localP.osition = new Vector3(0, 0.5f, 0);
		SavedARPosition.y = 0;
		CharacterRef.transform.localPosition = SavedARPosition + new Vector3(0, 0.5f, 0);
		CharacterRef.transform.localRotation = Quaternion.identity;
		CharacterRef.GetComponent<CharacterProgressScript>().Stop();

		UIGlobalVariablesScript.Singleton.AROnIndicator.SetActive(true);
		UIGlobalVariablesScript.Singleton.AROffIndicator.SetActive(false);
	
		//Debug.Log ("scene @ track: " + MainSceneRef.transform.position.ToString ());
		IsTracking = true;
	}

	public void FlipFrontBackCamera()
	{
		CameraDevice.Instance.Stop();

		// This assumes that the back facing camera was used before:
		if(CameraDevice.Instance.GetCameraDirection() == CameraDevice.CameraDirection.CAMERA_BACK || CameraDevice.Instance.GetCameraDirection() == CameraDevice.CameraDirection.CAMERA_DEFAULT)
		{
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);
			//camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3 (1,-1,1)); 
		}
		else
		{
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);
			//camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3 (1,1,1)); 
		}

		CameraDevice.Instance.Start();
	}

	void Update()
	{
		//Debug.Log(Input.gyro.attitude.eulerAngles.ToString());
		//NonARSceneRef.transform.localRotation = Quaternion.Euler(Input.gyro.attitude.eulerAngles.x / 340.0f, Input.gyro.attitude.eulerAngles.y / 340.0f, Input.gyro.attitude.eulerAngles.z / 340.0f);
	}
	
	private void OnTrackingLost()
	{
		if(IsTracking)
			SavedARPosition = CharacterRef.transform.localPosition;

		CharacterRef.transform.parent = null;

		NonARSceneRef.SetActive (true);
		MainSceneRef.SetActive(false);

		Camera.main.transform.position = Vector3.zero;
		Camera.main.transform.rotation = Quaternion.identity;


		//Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		CharacterRef.transform.parent = NonARSceneRef.transform;
		CharacterRef.transform.localPosition = new Vector3(0, 0.5f, -0.7f);
		CharacterRef.transform.localRotation = Quaternion.identity;
		CharacterRef.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

		CharacterRef.GetComponent<CharacterProgressScript>().Stop();

		UIGlobalVariablesScript.Singleton.AROnIndicator.SetActive(false);
		UIGlobalVariablesScript.Singleton.AROffIndicator.SetActive( true);

		/*MainSceneRef.transform.parent = NonARSceneRef.transform;

		MainSceneRef.transform.localPosition = Vector3.zero;
		MainSceneRef.transform.localRotation = Quaternion.identity;
		MainSceneRef.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);*/
		//MainSceneRef.transform.position = Vector3.zero;
		//MainSceneRef.transform.rotation = Quaternion.identity;



		/*for (int i=0; i<this.transform.childCount; ++i) {
		
			this.transform.GetChild(i).gameObject.SetActive(false);
		}*/


		//Debug.Log ("scene @ notrack: " + MainSceneRef.transform.position.ToString ());
		IsTracking	= false;
	}
}