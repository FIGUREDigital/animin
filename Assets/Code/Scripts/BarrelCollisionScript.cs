using UnityEngine;
using System.Collections;

public class BarrelCollisionScript : MonoBehaviour 
{
	private bool BeginDestroy;
	private float Timer = 0.7f;

	float leftBoundary;
	float rightBoundary;
	float lerp;
	float sign = 1;
	float lerpSpeed = 10;

	public GameObject BulletPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(BeginDestroy)
		{
			Timer -= Time.deltaTime;
			if(Timer <= 0)
			{
				Destroy(this.gameObject);
			}
		
			lerpSpeed += lerpSpeed * Time.deltaTime;
			lerp += Time.deltaTime * sign * lerpSpeed;
			if(lerp >= 1)
			{
				lerp = 1;
				sign *= -1;
			}
			else if(lerp <= 0)
			{
				lerp = 0;
				sign *= -1;
			}

			this.transform.localPosition = new Vector3(
				Mathf.Lerp(leftBoundary, rightBoundary, lerp),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
	}

	void OnCollisionEnter(Collision collision)
	{

		Debug.Log("COLLISION DETECTED: " + collision.gameObject.tag);

		if(collision.gameObject.tag == "Bullet")
		{
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().OnBulletHitBarrel(this.gameObject);
			if(!BeginDestroy)
			{
				BeginDestroy = true;
				leftBoundary = this.transform.localPosition.x - 0.02f;
				rightBoundary = this.transform.localPosition.x + 0.02f;
			}
		}
	}
}
