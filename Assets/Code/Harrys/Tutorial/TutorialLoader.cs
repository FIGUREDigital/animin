using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using System.Xml.Serialization;
using System.Xml;
using System.IO;

public class TutorialManager{
	#region Singleton
	private static TutorialManager s_Instance;
	
	public static TutorialManager Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new TutorialManager();
			}
			return s_Instance;
		}
	}
	#endregion

}

public class TutorialLoader : MonoBehaviour 
{
	[SerializeField]
	private const string FILENAME = "Assets/Resources/Tutorials.xml";
	
	//public static Tutorial[] mMarkers = new Tutorial[];


	// Use this for initialization
	void Start () 
	{
		
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			Debug.ClearDeveloperConsole();
			ArrayOfTutorials t = Deserialize();

			Debug.Log ("Counted : ["+t.Tutorials.Length+"] tutorials;");
			for (int i = 0; i < t.Tutorials.Length; i++){

				Debug.Log (" . Counted : ["+t.Tutorials[i].Lessons.Length+"] lessons;");
				for (int j = 0; j < t.Tutorials[i].Lessons.Length; j++){
					
					Debug.Log (" . . Counted : ["+t.Tutorials[i].Lessons[j].TutEntries.Length+"] entries;");
					for (int k = 0; k < t.Tutorials[i].Lessons[j].TutEntries.Length; k++){
						
						Debug.Log (" . . TutName : ["+t.Tutorials[i].name+"]; TutEntry : ["+t.Tutorials[i].Lessons[j].TutEntries[k].text+"];");
					}
				}
			}
		}
	}
	public ArrayOfTutorials Deserialize()
	{
		ArrayOfTutorials data = null;
		
		XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTutorials));
		
		StreamReader reader = new StreamReader(FILENAME);
		data = (ArrayOfTutorials)serializer.Deserialize(reader);
		reader.Close();
		
		return data;
	}
}
/* EXAMPLE XML
	<?xml version="1.0" encoding="utf-8"?>
	<ArrayOfTutorials xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	  <Tutorial>
	  	<Lesson>
	  		<TutEntry text = "Hello, I'm Andy Wormhole!"/>
	  		<TutEntry text = "Press button"/>
	  		<Stamp exit = "Acheivement"/>
	  	</Lesson>
	  </Tutorial>
	</ArrayOfTutorials>
*/

[Serializable()]
[System.Xml.Serialization.XmlRoot("ArrayOfTutorials")]
public class ArrayOfTutorials{
	[XmlArray("Tutorials")]
	[XmlArrayItem("Tutorial", typeof(Tutorial))]
	public Tutorial[] Tutorials{ get; set; }
}
[Serializable()]
public class Tutorial{
	[XmlArray("Lessons")]
	[XmlArrayItem("Lesson", typeof(Lesson))]
	public Lesson[] Lessons{ get; set; }
	[System.Xml.Serialization.XmlAttribute("name")]
	public string name { get; set; }
}
[Serializable()]
public class Lesson{
	[XmlArray("TutEntries")]
	[XmlArrayItem("TutEntry", typeof(TutEntry))]
	public TutEntry[] TutEntries{ get; set; }
}
[Serializable()]
public class TutEntry{
	[System.Xml.Serialization.XmlAttribute("text")]
	public string text { get; set; }
}
[Serializable()]
public class Stamp{
	[System.Xml.Serialization.XmlAttribute("exit")]
	public string exit { get; set; }
}















