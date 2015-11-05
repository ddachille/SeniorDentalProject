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

	//the volumedata held in here
	private ColoredCubesVolume coloredCubesVol;

	

	// Use this for initialization
	void Start () {
		//Save Tooth GameObject into the volume variable to access 
		coloredCubesVol = gameObject.GetComponent<ColoredCubesVolume>();

		//Save Region as a variable to access outer corners of array
		Region theWholeRegion = coloredCubesVol.data.enclosingRegion;
		//Debug.Log (theWholeRegion.lowerCorner.x); and y..z...and uppercorner etc.

		//loop through an array of the enclosing region to figure out total numbers of certain colors.
		for (int arrayX = theWholeRegion.lowerCorner.x; arrayX <= theWholeRegion.upperCorner.x; arrayX++) {
			for(int arrayY = theWholeRegion.lowerCorner.y; arrayY <= theWholeRegion.upperCorner.y; arrayY++){
				for(int arrayZ = theWholeRegion.lowerCorner.z; arrayZ <= theWholeRegion.upperCorner.z; arrayZ++){
					//save currentVoxel we are on's color
					QuantizedColor currentColor = coloredCubesVol.data.GetVoxel(arrayX,arrayY,arrayZ);

					//test the colorS
					if(currentColor.red == 248 && currentColor.green == 252 && currentColor.blue == 248){
						totalWhite++;
					}

				}
			}
		}
		Debug.Log ("TOTAL WHITE");
		Debug.Log (totalWhite);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
