using UnityEngine;
using System.Collections;

public class ObjectLookAtDeviceScript : MonoBehaviour 

{

	float angle =0;
	float speed = (2 * Mathf.PI) / 5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
	float radius=5;


	void Start () 
	{
	
	}

	void Update () 
	{
		angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=


		//Camera.main.transform.position = new Vector3 (Mathf.Sin(angle)*radius, 6.25f, Mathf.Cos(angle)*radius);
		//Camera.main.transform.LookAt (Vector3.zero);

		//this.transform.localRotation = Input.gyro.attitude;// new Quaternion(Input.gyro.attitude.x, 0, 0, 0);

		//this.transform.localRotation = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);

		//this.transform.localRotation = Quaternion.Euler (new Vector3(360 - (Input.gyro.attitude.x * 90), 0, 0));

		//Debug.Log ("gyro rotation: " + Input.gyro.attitude.ToString());


		//Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y -  Time.deltaTime * 1, Camera.main.transform.position.z);

		//Vector3 euler = Camera.main.transform.rotation.eulerAngles;
		//euler.x += 90;

		//DoBillboardOrientation ();
		//DoWholeOrientation ();
		DoLookRotation ();
	}

	private void DoLookRotation()
	{
		var relativePos = Camera.main.transform.position - transform.position;
				var rotation = Quaternion.LookRotation (relativePos);
				transform.rotation = rotation;
	}

	private void DoWholeOrientation()
	{
		Vector3 ray = Camera.main.transform.position - this.transform.position;
		ray.Normalize ();
		
		//transform.LookAt(transform.position + Quaternion.Euler(euler) * Vector3.back,
		//                 Camera.main.transform.rotation * Vector3.up);
		transform.LookAt (Camera.main.transform.position, Camera.main.transform.up);
		transform.localRotation = Quaternion.Euler (transform.localRotation.eulerAngles.x + 90, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
		
		//this.transform.LookAt (Camera.main.transform.position);
		//this.transform.localRotation = Quaternion.Euler (this.transform.localRotation.eulerAngles.x, this.transform.localRotation.eulerAngles.y - 180, this.transform.localRotation.eulerAngles.z);
		//Debug.Log(this.transform.localRotation.eulerAngles);

	}

	private void DoBillboardOrientation()
	{
		Vector3 v = Camera.main.transform.position - transform.position;
		
		v.x = v.z = 0.0f;
		
		transform.LookAt(Camera.main.transform.position - v); 
	}
}
