using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using System.Xml.Serialization;
using System.Xml;
using System.IO;






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
	[System.Xml.Serialization.XmlAttribute("id")]
	public string id { get; set; }

	public int id_num { get { return Convert.ToInt32(id); } }
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






public class TutorialReader{

	private ArrayOfTutorials m_Tutorials;
	public Tutorial[] Tutorials{ get { return m_Tutorials.Tutorials; } }
	private bool[] m_Finished;
	public bool[] TutorialFinished{ get { return m_Finished; } }

	
	private const string FILENAME = "Assets/Resources/Tutorials.xml";

	public TutorialReader(){
		this.Deserialize();
	}

	public void Deserialize()
	{
		ArrayOfTutorials data = null;
		
		XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfTutorials));
		
		StreamReader reader = new StreamReader(FILENAME);
		data = (ArrayOfTutorials)serializer.Deserialize(reader);
		reader.Close();
		
		//return data;
		
		m_Tutorials = data;
		Debug.Log ("You're a tool, Harry");
		m_Finished = new bool[m_Tutorials.Tutorials.Length];
	}











	public void test(){
		ArrayOfTutorials t = m_Tutorials;
		Debug.Log ("Counted : ["+t.Tutorials.Length+"] tutorials;");
		for (int i = 0; i < t.Tutorials.Length; i++){
			
			Debug.Log (" . Counted : ["+t.Tutorials[i].Lessons.Length+"] lessons;");
			for (int j = 0; j < t.Tutorials[i].Lessons.Length; j++){
				
				Debug.Log (" . . Counted : ["+t.Tutorials[i].Lessons[j].TutEntries.Length+"] entries;");
				for (int k = 0; k < t.Tutorials[i].Lessons[j].TutEntries.Length; k++){
					
					Debug.Log (" . . TutName : ["+t.Tutorials[i].id+"]; TutEntry : ["+t.Tutorials[i].Lessons[j].TutEntries[k].text+"];");
				}
			}
		}
	}






	#region Singleton
	private static TutorialReader s_Instance;
	
	public static TutorialReader Instance
	{
		get
		{
			if ( s_Instance == null )
			{
				s_Instance = new TutorialReader();
			}
			return s_Instance;
		}
	}
	#endregion
}














