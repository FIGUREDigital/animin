using System;
using System.Collections.Generic;
using UnityEngine;

public class OscillationUpDownScript : MonoBehaviour
{
	private float Timer;
	private float Speed = 10;
	
	void Start()
	{
		Timer = UnityEngine.Random.Range(0.0f, 9.0f);
		Speed = UnityEngine.Random.Range(6.0f, 8.0f);
	}
	
	void Update()
	{
		Timer += Time.deltaTime * Speed;
		if(Timer >= 90)
		{
			Timer = 90;
			Speed *= -1;
		}
		else if(Timer <= 0)
		{
			Timer = 0;
			Speed *= -1;
		}
		
		this.transform.localPosition = this.transform.localPosition + new Vector3(0, Mathf.Sin(Timer) * 0.004f, 0);
	}
}

public class EnemyDeathAnimationScript : MonoBehaviour
{
	private float Timer;
	private float Speed;
	
	void Start()
	{
		Timer = 3;
		Speed = UnityEngine.Random.Range(0.3f, 0.4f);
	}
	
	void Update()
	{
		this.transform.localPosition = this.transform.localPosition - new Vector3(0, Time.deltaTime * Speed, 0);

		Timer -= Time.deltaTime;
		if(Timer <= 0)
		{
			Destroy(this);
			this.gameObject.SetActive(false);
		}
	}
}


public class FlashMaterialColorScript : MonoBehaviour
{
	private float Timer;
	private float Speed;
	private int TimesFlashed;
	
	void Start()
	{
		Timer = 90;
		Speed = UnityEngine.Random.Range(3.0f, 4.0f);
	}
	
	void Update()
	{
		Timer += Time.deltaTime * Speed;
		if(Timer >= 1)
		{
			Timer = 1;
			Speed *= -1;
			TimesFlashed++;
		}
		else if(Timer <= 0)
		{
			Timer = 0;
			Speed *= -1;
		}

		float alpha = Mathf.Lerp(0.6f, 1.0f, Timer);

		for(int i=0;i<transform.childCount;++i)
		{
			transform.GetChild(i).renderer.material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		}

		if(TimesFlashed == 5)
		{
			Destroy(this);
		}
		
		//this.transform.localPosition = this.transform.localPosition + new Vector3(0, Mathf.Lerp(0.4f, 1.0f, Timer / 90.0f), 0);
	}
}
