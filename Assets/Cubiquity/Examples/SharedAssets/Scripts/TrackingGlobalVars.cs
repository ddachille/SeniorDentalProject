using UnityEngine;
using System.Collections;
using Cubiquity;

public class TrackingGlobalVars : MonoBehaviour {
	//Global variables that will keep the count of all total voxels in the array
	public static uint totalWhite = 0;
	public static uint totalBrown = 0; 
	public static uint totalDarkYellow = 0;
	public static uint totalYellow = 0;

	//Global variables that will increase every time a voxel of the color is deleted
	public static uint deletedWhite = 0;
	public static uint deletedBrown = 0;
	public static uint deletedDarkYellow = 0;
	public static uint deletedYellow = 0;






	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
