using UnityEngine;
using System.Collections;

public enum PopupItemType
{
	Food = 0,
	Item,
	Medicine,
}

public class UIPopupItemScript : MonoBehaviour 
{
	public int Points;
	public PopupItemType Type;
	public GameObject Model3D;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
