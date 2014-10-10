using UnityEngine;
using System.Collections;


public class MultipleCardTrackerScript : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
		
	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

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
		
	void Update()
	{

	}
	
	
	void LateUpdate()
	{
		
		
	}
	
			
	private void OnTrackingFound()
	{
		TrackVuforiaScript script = GameObject.FindObjectOfType<TrackVuforiaScript>();
		script.OnTrackingFound();
	}
	
	
	public void OnTrackingLost()
	{
		TrackVuforiaScript.IsTracking = false;
		TrackVuforiaScript script = GameObject.FindObjectOfType<TrackVuforiaScript>();
		script.OnTrackingLost();
	}
}