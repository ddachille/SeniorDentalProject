using UnityEngine;
using System.Collections;

public class mygui : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
		{
		  GUI.Label(new Rect(0,0,100,100), ("Yellow: " + TrackingGlobalVars.truncatedYellow.ToString() + "%  Brown: " + TrackingGlobalVars.truncatedBrown.ToString() + "%  Black: " + TrackingGlobalVars.truncatedBlack + "%  White: " + TrackingGlobalVars.truncatedWhite + "%"));
		  
		  GUI.Button(new Rect(165,320,145,30),"Back to Main Menu");
		}
}

