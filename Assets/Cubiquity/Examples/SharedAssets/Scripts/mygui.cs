using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mygui : MonoBehaviour {

	string name; 
	List<Scores> highscores;
	Rect lastRect;
	
	// Use this for initialization
	void Start () {
		name = "bob";
		highscores = new List<Scores>();
		HighScoreManager._instance.SaveHighScore(name, float.Parse(TrackingGlobalVars.truncatedYellow.ToString()), float.Parse(TrackingGlobalVars.truncatedBrown.ToString()), float.Parse(TrackingGlobalVars.truncatedBlack.ToString()), float.Parse(TrackingGlobalVars.truncatedWhite.ToString()));
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	void OnGUI()
		{
		
		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		
		highscores = HighScoreManager._instance.GetHighScore();
		
		 GUILayout.Space(25);
		  
		 GUILayout.BeginHorizontal();
         //GUILayout.Label("Name",GUILayout.Width(Screen.width/5));
         GUILayout.Label("Black %",GUILayout.Width(Screen.width/4));
         GUILayout.Label("Brown %",GUILayout.Width(Screen.width/4));
         GUILayout.Label("Yellow %",GUILayout.Width(Screen.width/4));
         GUILayout.Label("White %",GUILayout.Width(Screen.width/4));
         GUILayout.EndHorizontal();
		  
		 GUILayout.BeginHorizontal();
		 //GUILayout.Label(_score.name,GUILayout.Width(Screen.width/5));
		 GUILayout.Label(""+TrackingGlobalVars.truncatedBlack,GUILayout.Width(Screen.width/4));
		 GUILayout.Label(""+TrackingGlobalVars.truncatedBrown,GUILayout.Width(Screen.width/4));
		 GUILayout.Label(""+TrackingGlobalVars.truncatedYellow,GUILayout.Width(Screen.width/4));
		 GUILayout.Label(""+TrackingGlobalVars.truncatedWhite,GUILayout.Width(Screen.width/4));
		 GUILayout.EndHorizontal(); 
		  
		  GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        
		 GUILayout.BeginHorizontal();
		 //GUILayout.Label("Name",GUILayout.Width(Screen.width/5));
		 GUILayout.Label("Black %",GUILayout.Width(Screen.width/4));
		 GUILayout.Label("Brown %",GUILayout.Width(Screen.width/4));
		 GUILayout.Label("Yellow %",GUILayout.Width(Screen.width/4));
		 GUILayout.Label("White %",GUILayout.Width(Screen.width/4));
		 GUILayout.EndHorizontal();
		 
		 foreach(Scores _score in highscores)
		 {
			 GUILayout.BeginHorizontal();
			 //GUILayout.Label(_score.name,GUILayout.Width(Screen.width/5));
			 GUILayout.Label(""+_score.blackScore,GUILayout.Width(Screen.width/4));
			 GUILayout.Label(""+_score.brownScore,GUILayout.Width(Screen.width/4));
			 GUILayout.Label(""+_score.yellowScore,GUILayout.Width(Screen.width/4));
			 GUILayout.Label(""+_score.whiteScore,GUILayout.Width(Screen.width/4));
			 GUILayout.EndHorizontal();
		 }
		  GUI.Button(new Rect((Screen.width / 2) - Screen.width / 4, 340, 130, 20),"Back to Main Menu");
		  //GUI.Button(new Rect(165,320,145,30),"Back to Main Menu");
		  
		 
		  
		}
}

