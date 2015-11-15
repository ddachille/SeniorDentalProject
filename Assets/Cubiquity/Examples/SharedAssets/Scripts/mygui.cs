﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mygui : MonoBehaviour {

	// Use this for initialization
	void Start () {
		string name = "bob";
		List<Scores> highscores = new List<Scores>();
		
		HighScoreManager._instance.SaveHighScore(name, float.Parse(TrackingGlobalVars.truncatedYellow.ToString()), float.Parse(TrackingGlobalVars.truncatedBrown.ToString()), float.Parse(TrackingGlobalVars.truncatedBlack.ToString()), float.Parse(TrackingGlobalVars.truncatedWhite.ToString()));
		
		highscores = HighScoreManager._instance.GetHighScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	
	void OnGUI()
		{
		  GUI.Label(new Rect(0,0,100,100), ("Yellow: " + TrackingGlobalVars.truncatedYellow.ToString() + "%  Brown: " + TrackingGlobalVars.truncatedBrown.ToString() + "%  Black: " + TrackingGlobalVars.truncatedBlack + "%  White: " + TrackingGlobalVars.truncatedWhite + "%"));
		  
		  GUILayout.Space(60);
         
         GUILayout.BeginHorizontal();
         GUILayout.Label("Name",GUILayout.Width(Screen.width/2));
         GUILayout.Label("Scores",GUILayout.Width(Screen.width/2));
         GUILayout.Label("Scores",GUILayout.Width(Screen.width/2));
         GUILayout.Label("Scores",GUILayout.Width(Screen.width/2));
         GUILayout.Label("Scores",GUILayout.Width(Screen.width/2));
         GUILayout.EndHorizontal();
         
         GUILayout.Space(25);
         
         foreach(Scores _score in highscores)
         {
             GUILayout.BeginHorizontal();
             GUILayout.Label(_score.name,GUILayout.Width(Screen.width/2));
             GUILayout.Label(""+_score.yellowScore,GUILayout.Width(Screen.width/2));
             GUILayout.Label(""+_score.brownScore,GUILayout.Width(Screen.width/2));
             GUILayout.Label(""+_score.blackScore,GUILayout.Width(Screen.width/2));
             GUILayout.Label(""+_score.whiteScore,GUILayout.Width(Screen.width/2));
             GUILayout.EndHorizontal();
         }
		  
		  GUI.Button(new Rect(165,320,145,30),"Back to Main Menu");
		}
}

