using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 
 /// <summary>
 /// High score manager.
 /// Local highScore manager for LeaderboardLength number of entries
 /// 
 /// this is a singleton class.  to access these functions, use HighScoreManager._instance object.
 /// eg: HighScoreManager._instance.SaveHighScore("meh",1232);
 /// No need to attach this to any game object, thought it would create errors attaching.
 /// </summary>
 
 public class HighScoreManager : MonoBehaviour
 {
     
     private static HighScoreManager m_instance;
     private const int LeaderboardLength = 10;
     
     public static HighScoreManager _instance {
         get {
             if (m_instance == null) {
                 m_instance = new GameObject ("HighScoreManager").AddComponent<HighScoreManager> ();                
             }
             return m_instance;
         }
     }
     
     void Awake ()
     {
         if (m_instance == null) {
             m_instance = this;            
         } else if (m_instance != this)        
             Destroy (gameObject);    
         
         DontDestroyOnLoad (gameObject);
     }
     
     public void SaveHighScore (string name, float yellowScore, float brownScore, float blackScore, float whiteScore)
     {
         Debug.Log(yellowScore);
		 List<Scores> HighScores = new List<Scores> ();
 
         int i = 1;
         while (i<=LeaderboardLength && PlayerPrefs.HasKey("HighScore"+i+"yellowScore") && PlayerPrefs.HasKey("HighScore"+i+"brownScore") && PlayerPrefs.HasKey("HighScore"+i+"blackScore") && PlayerPrefs.HasKey("HighScore"+i+"whiteScore")) {
             Scores temp = new Scores ();
             temp.yellowScore = PlayerPrefs.GetFloat ("HighScore" + i + "yellowScore");
			 temp.brownScore = PlayerPrefs.GetFloat ("HighScore" + i + "brownScore");
			 temp.blackScore = PlayerPrefs.GetFloat ("HighScore" + i + "blackScore");
			 temp.whiteScore = PlayerPrefs.GetFloat ("HighScore" + i + "whiteScore");
             temp.name = PlayerPrefs.GetString ("HighScore" + i + "name");
             HighScores.Add (temp);
			 Debug.Log("heydo ho" + i + temp.yellowScore);
             i++;
         }
         if (HighScores.Count == 0) {  //if high score table empty, add current score           
             Scores _temp = new Scores ();
             _temp.name = name;
             _temp.yellowScore = yellowScore;
             _temp.brownScore = brownScore;
             _temp.blackScore = blackScore;
             _temp.whiteScore = whiteScore;
             HighScores.Add (_temp);
         } else {
             for (i=1; i<=HighScores.Count && i<=LeaderboardLength; i++) {
				 if (blackScore > HighScores [i - 1].blackScore) { //if current black score is greater than a high score, insert current score there
                     Scores _temp = new Scores ();
					 Debug.Log("I'm in blackScore if. high score list: ");
					 for (int j=0; j<10; j++) {
						Debug.Log(j + ": " + HighScores [j].yellowScore + "\n");
					 }
                     _temp.name = name;
                     _temp.yellowScore = yellowScore;
					 Debug.Log(_temp.yellowScore);
					 _temp.brownScore = brownScore;
					 _temp.blackScore = blackScore;
					 _temp.whiteScore = whiteScore;
                     HighScores.Insert (i - 1, _temp);
                     break;
                 } 
				 else if (blackScore == HighScores [i - 1].blackScore) { //if black is same, check brown
                     if (brownScore > HighScores [i - 1].brownScore) { //if current brown score is greater than a high score, insert current score there
						 Scores _temp = new Scores ();
						 _temp.name = name;
						 _temp.yellowScore = yellowScore;
						 _temp.brownScore = brownScore;
						 _temp.blackScore = blackScore;
						 _temp.whiteScore = whiteScore;
						 HighScores.Insert (i - 1, _temp);
						 break;
					} 
					else if (brownScore == HighScores [i - 1].brownScore) { //if brown is same, check yellow
						if (yellowScore > HighScores [i - 1].yellowScore) { //if current yellow score is greater than a high score, insert current score there
						 Scores _temp = new Scores ();
						 _temp.name = name;
						 _temp.yellowScore = yellowScore;
						 _temp.brownScore = brownScore;
						 _temp.blackScore = blackScore;
						 _temp.whiteScore = whiteScore;
						 HighScores.Insert (i - 1, _temp);
						 break;
						} 
					}
                 } 
				 /*if (score > HighScores [i - 1].score) { //if current score is greater than a current high score, insert current score there
                     Scores _temp = new Scores ();
                     _temp.name = name;
                     _temp.score = score;
                     HighScores.Insert (i - 1, _temp);
                     break;
                 } */
                 if (i == HighScores.Count && i < LeaderboardLength) { //if high score table not yet full, and if current score isnt higher than any existing scores
                     Scores _temp = new Scores ();
                     _temp.name = name;
                     _temp.yellowScore = yellowScore;
					 _temp.brownScore = brownScore;
					 _temp.blackScore = blackScore;
					 _temp.whiteScore = whiteScore;
                     HighScores.Add (_temp);
                     break;
                 }
				 
             }
         }
         
         i = 1;
         while (i<=LeaderboardLength && i<=HighScores.Count) {
			 Debug.Log(HighScores [i - 1].yellowScore + "!");
             PlayerPrefs.SetString ("HighScore" + i + "name", HighScores [i - 1].name);
             PlayerPrefs.SetFloat ("HighScore" + i + "yellowScore", HighScores [i - 1].yellowScore);
			 PlayerPrefs.SetFloat ("HighScore" + i + "brownScore", HighScores [i - 1].brownScore);
			 PlayerPrefs.SetFloat ("HighScore" + i + "blackScore", HighScores [i - 1].blackScore);
			 PlayerPrefs.SetFloat ("HighScore" + i + "whiteScore", HighScores [i - 1].whiteScore);
             i++;
         }
         
     }
 
     public List<Scores>  GetHighScore ()
     {
         List<Scores> HighScores = new List<Scores> ();
 
         int i = 1;
         while (i<=LeaderboardLength && PlayerPrefs.HasKey("HighScore"+i+"yellowScore") && PlayerPrefs.HasKey("HighScore"+i+"brownScore") && PlayerPrefs.HasKey("HighScore"+i+"blackScore") && PlayerPrefs.HasKey("HighScore"+i+"whiteScore")) {
             Scores temp = new Scores ();
             temp.yellowScore = PlayerPrefs.GetFloat ("HighScore" + i + "yellowScore");
			 temp.brownScore = PlayerPrefs.GetFloat ("HighScore" + i + "brownScore");
			 temp.blackScore = PlayerPrefs.GetFloat ("HighScore" + i + "blackScore");
			 temp.whiteScore = PlayerPrefs.GetFloat ("HighScore" + i + "whiteScore");
             temp.name = PlayerPrefs.GetString ("HighScore" + i + "name");
             HighScores.Add (temp);
             i++;
         }
 
         return HighScores;
     }
     
     public void ClearLeaderBoard ()
     {
         //for(int i=0;i<HighScores.
         List<Scores> HighScores = GetHighScore();
         
         for(int i=1;i<=HighScores.Count;i++)
         {
             PlayerPrefs.DeleteKey("HighScore" + i + "name");
             PlayerPrefs.DeleteKey("HighScore" + i + "score");
             PlayerPrefs.DeleteKey("HighScore" + i + "score");
             PlayerPrefs.DeleteKey("HighScore" + i + "score");
             PlayerPrefs.DeleteKey("HighScore" + i + "score");
         }
     }
     
     void OnApplicationQuit()
     {
         PlayerPrefs.Save();
     }
 }
 
 public class Scores
 {
     public float yellowScore;
	 public float brownScore;
	 public float blackScore;
	 public float whiteScore;
     public string name;
 
 }