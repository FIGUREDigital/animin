using UnityEngine;
using System.Collections;

public class SpinObjectScript : MonoBehaviour
{

	float rotationAngle;
	float rotaitonSpeed;

	// Use this for initialization
	void Start () 
	{
		rotationAngle = Random.Range(0, 360) + this.transform.rotation.eulerAngles.y;
		rotaitonSpeed = Random.Range(45, 55);
	}
	
	// Update is called once per frame
	void Update () 
	{

		rotationAngle += Time.deltaTime * rotaitonSpeed;

		transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
	
	}
}
