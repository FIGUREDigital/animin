using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class EvolutionMarkerManager : MonoBehaviour 
{
	[SerializeField]
	private const string FILENAME = "Assets/Resources/MarkerGuide.xml";
	private const int MARKER_RATE = 10;
	private int mZefProgress;
	private int mCurrentMarker;
	private string mReward;

	public static List<string> mMarkers = new List<string>();

	#region Singleton
	
	private static EvolutionMarkerManager s_Instance;
	
	public static EvolutionMarkerManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new EvolutionMarkerManager();
			}
			return s_Instance;
		}
	}
	
	#endregion


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mZefProgress >= MARKER_RATE)
		{
			mZefProgress = 0;
			mReward = mMarkers[mCurrentMarker];
			mCurrentMarker++;
		}
	}

	public List<string> Deserialize()
	{
		List<string> data = null;
		
		XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
		
		StreamReader reader = new StreamReader(FILENAME);
		data = (List<string>)serializer.Deserialize(reader);
		reader.Close();
		
		return data;
	}

	public void Serialize()
	{
		XmlSerializer ser = new XmlSerializer(typeof(List<string>));
		TextWriter writer = new StreamWriter(FILENAME);
		ser.Serialize(writer, mMarkers);
		writer.Close();
	}
}
