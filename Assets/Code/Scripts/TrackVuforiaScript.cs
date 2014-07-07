using UnityEngine;
using System.Collections;

public class ValueSmoother
{
	public float ValueNow;
	public float ValueNext;

	public void Update()
	{
		if(Mathf.Abs(ValueNow - ValueNext) >= 0.15f)
			ValueNow = Mathf.Lerp(ValueNow, ValueNext, Time.deltaTime * 6);
	}
}


public class TrackVuforiaScript : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;

	Vector3 SavedARPosition;

	//public GameObject ARSceneRef;
	public GameObject NonARSceneRef;
	public GameObject MainSceneRef;
	public GameObject CharacterRef;

	public static bool IsTracking;


	private ValueSmoother SmootherAxisX = new ValueSmoother();
	private ValueSmoother SmootherAxisY = new ValueSmoother();

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}


		this.renderer.enabled = false;
		SavedARPosition = new Vector3(0, 0.0f, 0);
		Input.gyro.enabled = true;
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
		CharacterRef.transform.localRotation = Quaternion.Euler(0, 180, 0);
		CharacterRef.GetComponent<CharacterProgressScript>().Stop(true);

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

		//Debug.Log("GYOR: " + Input.gyro.attitude.eulerAngles.ToString());

		if(NonARSceneRef.activeSelf)
		{
			/*float gyroAngle = 270 - Input.gyro.attitude.eulerAngles.z;
			if(gyroAngle >= 30) gyroAngle = 30;
			if(gyroAngle <= -30) gyroAngle = -30;

			gyroAngle += 30;
			gyroAngle /= 60;*/

			//float xAngle = Mathf.Lerp(15.5f, -15.5f, (Input.acceleration.x + 1) / 2);


			//Debug.Log("GYRO: " + Input.gyro.attitude.eulerAngles.ToString());

			//NonARSceneRef.transform.localRotation = Quaternion.Euler(gyroAngleY, 0, 0);

			//Debug.Log("Acceleration: " + Input.acceleration.ToString() + "_" + ((Input.acceleration.x + 1) / 2).ToString());
			//Debug.Log("Gravity: " + Input.gyro.gravity.ToString());

			//0.0f to -0.5f to -1.0f
			//Debug.Log((Mathf.Lerp(-50, 50, (Input.acceleration.x + 1) / 2)).ToString());
			//Camera.main.transform.rotation = Quaternion.Euler(14.73474f + (Input.acceleration.y) * 10, -(Input.acceleration.x) * 10, 0);
			//Camera.main.transform.localPosition = new Vector3(Mathf.Lerp(200, -200, (Input.acceleration.x + 1) / 2), Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);


			//Debug.Log(	GetQuadraticCoordinates(((Camera.main.transform.localPosition.x + 200) / 1)).ToString());

		
			float stableAccelerationX = (float)System.Math.Round(Input.acceleration.x, 2);
			float stableAccelerationY = (float)System.Math.Round(Input.acceleration.y, 2);

			float angle = Mathf.Lerp(180, 360, (stableAccelerationX + 1) / 2 /*(Mathf.Sin(Time.time) + 1) / 2*/);
			SmootherAxisX.ValueNext = (float)System.Math.Round(angle);

			Vector3 newPosition = new Vector3(
				Mathf.Cos(SmootherAxisX.ValueNow* Mathf.Deg2Rad ) * 220,
				0,
				(Mathf.Sin(SmootherAxisX.ValueNow* Mathf.Deg2Rad ) * 220) * 0.2f);


		

			float value = stableAccelerationY;
			if(value > 0) value = 0;
			if(value < -1) value = -1;
			value *= -1;
			//value = 1 - value;

			float angle2 = Mathf.Lerp(360, 180, value/*(Mathf.Sin(Time.time) + 1) / 2*/);
			SmootherAxisY.ValueNext = (float)System.Math.Round(angle2);
		
			Vector3 newPosition2 = new Vector3(
				0,
				Mathf.Cos(SmootherAxisY.ValueNow* Mathf.Deg2Rad ) * 90,
				(Mathf.Sin(SmootherAxisY.ValueNow* Mathf.Deg2Rad ) * 90) * 0.2f);



			Camera.main.transform.localPosition = new Vector3(0, 233.1f, -198.3f) + newPosition2 + newPosition;
			Camera.main.transform.LookAt(NonARSceneRef.transform);
		}


		SmootherAxisX.Update();
		SmootherAxisY.Update();

	}



	

	
	private void OnTrackingLost()
	{
		if(IsTracking)
			SavedARPosition = CharacterRef.transform.localPosition;

		CharacterRef.transform.parent = null;

		NonARSceneRef.SetActive (true);
		MainSceneRef.SetActive(false);

		Camera.main.transform.position = new Vector3(0, 123.1f, -198.3f);
		Camera.main.transform.rotation = Quaternion.Euler(14.73474f, 0.0f, 0.0f);


		//Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		CharacterRef.transform.parent = NonARSceneRef.transform;
		CharacterRef.transform.localPosition = new Vector3(0, 0.0006500053f, 0.0f);
		CharacterRef.transform.localRotation = Quaternion.Euler(0, 180, 0);
		CharacterRef.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

		CharacterRef.GetComponent<CharacterProgressScript>().Stop(true);

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