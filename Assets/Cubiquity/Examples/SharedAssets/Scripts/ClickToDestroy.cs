using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cubiquity;

public class ClickToDestroy : MonoBehaviour
{
	private ColoredCubesVolume coloredCubesVolume;
	
	// Bit of a hack - we want to detect mouse clicks rather than the mouse simply being down,
	// but we can't use OnMouseDown because the voxel terrain doesn't have a collider (the
	// individual pieces do, but not the parent). So we define a click as the mouse being down
	// but not being down on the previous frame. We'll fix this better in the future...

	//DD- we want to change that it is continuous though, so we do use onMouseDown


	// Use this for initialization
	void Start()
	{
		// We'll store a reference to the colored cubes volume so we can interact with it later.
		coloredCubesVolume = gameObject.GetComponent<ColoredCubesVolume>();
		if(coloredCubesVolume == null)
		{
			Debug.LogError("This 'ClickToDestroy' script should be attached to a game object with a ColoredCubesVolume component");
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		// Bail out if we're not attached to a terrain.
		if(coloredCubesVolume == null)
		{
			return;
		}
		
		// If the mouse btton is down and it was not down last frame
		// then we consider this a click, and do our destruction.
		if(Input.GetMouseButton(0))
		{

				// Build a ray based on the current mouse position
				Vector2 mousePos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 0));
				
				
				// Perform the raycasting. If there's a hit the position will be stored in these ints.
				PickVoxelResult pickResult;
				bool hit = Picking.PickFirstSolidVoxel(coloredCubesVolume, ray, 1000.0f, out pickResult);

				//DD-lets see whats stored here
				Debug.Log (pickResult.volumeSpacePos);

				// If we hit a solid voxel then create an explosion at this point.
				//DD-we need to not do an explosion. Do we have to change destroy voxels?yes
				if(hit)
				{					
					//we have to change...0, or 1..
					int range = 1;
					DestroyVoxels(pickResult.volumeSpacePos.x, pickResult.volumeSpacePos.y, pickResult.volumeSpacePos.z, range);
				}
				

		}
		else
		{
			// Clear the flag while we wait for a click.
			//isMouseAlreadyDown = false;
		}
	}
	
	public bool IsSurfaceVoxel(int x, int y, int z)
	{
		QuantizedColor quantizedColor;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x, y, z);
		if(quantizedColor.alpha < 127) return false;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x + 1, y, z);
		if(quantizedColor.alpha < 127) return true;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x - 1, y, z);
		if(quantizedColor.alpha < 127) return true;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x, y + 1, z);
		if(quantizedColor.alpha < 127) return true;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x, y - 1, z);
		if(quantizedColor.alpha < 127) return true;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x, y, z + 1);
		if(quantizedColor.alpha < 127) return true;
		
		quantizedColor = coloredCubesVolume.data.GetVoxel(x, y, z - 1);
		if(quantizedColor.alpha < 127) return true;
		
		return false;
	}
	
	void DestroyVoxels(int xPos, int yPos, int zPos, int range)
	{

		// Initialise outside the loop, but we'll use it later.
		Vector3 pos = new Vector3(xPos, yPos, zPos);
		int rangeSquared = range * range;
		
		// Later on we will be deleting some voxels, but we'll also be looking at the neighbours of a voxel.
		// This interaction can have some unexpected results, so it is best to first make a list of voxels we
		// want to delete and then delete them later in a separate pass.
		List<Vector3i> voxelsToDelete = new List<Vector3i>();
		
		// Iterage over every voxel in a cubic region defined by the received position (the center) and
		// the range. It is quite possible that this will be hundreds or even thousands of voxels.
		for(int z = zPos - range; z < zPos + range; z++) 
		{
			for(int y = yPos - range; y < yPos + range; y++)
			{
				for(int x = xPos - range; x < xPos + range; x++)
				{			
					// Compute the distance from the current voxel to the center of our explosion.
					int xDistance = x - xPos;
					int yDistance = y - yPos;
					int zDistance = z - zPos;
					
					// Working with squared distances avoids costly square root operations.
					int distSquared = xDistance * xDistance + yDistance * yDistance + zDistance * zDistance;
					
					// We're iterating over a cubic region, but we want our explosion to be spherical. Therefore 
					// we only further consider voxels which are within the required range of our explosion center. 
					// The corners of the cubic region we are iterating over will fail the following test.
					if(distSquared < rangeSquared)
					{	
						// Get the current color of the voxel
						QuantizedColor color = coloredCubesVolume.data.GetVoxel(x, y, z);				
						//WHAT IS THE COLOR!!!!!!!!!
						//Debug.Log ("here is the COLOR!!!!!!!");
						//Debug.Log (color.red);
						//Debug.Log (color.green);
						//Debug.Log (color.blue);

						if(color.red == 248 && color.green == 252 && color.blue == 200){
							TrackingGlobalVars.deletedWhite++;
						}else if(color.red == 24 && color.green == 20 && color.blue == 8){
							TrackingGlobalVars.deletedBlack++;
						}else if(color.red == 112 && color.green == 28 && color.blue == 0){
							TrackingGlobalVars.deletedBrown++;
						}else if(color.red == 152 && color.green == 152 && color.blue == 0){
							TrackingGlobalVars.deletedYellow++;
						}else{
						}

						//Debug.Log (TrackingGlobalVars.totalWhite);

						// Check the alpha to determine whether the voxel is visible. 
						if(color.alpha > 127)
						{							
							Vector3i voxel = new Vector3i(x, y, z);
							voxelsToDelete.Add(voxel);

						}
					}
				}
			}
		}
		
		foreach (Vector3i voxel in voxelsToDelete) // Loop through List with foreach
		{
		    coloredCubesVolume.data.SetVoxel(voxel.x, voxel.y, voxel.z, new QuantizedColor(0,0,0,0));
		}
	}
}
