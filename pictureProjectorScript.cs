/*
 * README
 * 
 * The following code is for a video game called Chroma, 
 * which was made by a student team at UC Santa Cruz.
 * 
 * This code's purpose is for the portfolio website of Chris Burris, one of the students
 * who worked on the game.
 * 
 * You can visit Chris Burris' website at: chrisbburris.com
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class pictureProjectorScript : MonoBehaviour {
	
	public Projector pictureProjector; // The projector component attached to this object
	
	private GameObject[] levelBlocks; // Holds all the level blocks the projector is displaying the picture on
	
	private List<GameObject> removeList;
	
	private bool showPicture = false; // Set to true, if it is time to display the picture
	
	// An int that increases until it is greater than the number of level blocks the picture is displaying on
	private int index = 0;
	
	private Vector3 upperLeftPoint; // The upper left point of the picture projector box as seen from a top-down view
	private Vector3 lowerRightPoint; // The lower right point of the picture projector box as seen from a top-down view
	
	private int multiple;
	
	private int count = 0;
	

	void Awake () {
		
		// Use to hold the level blocks returned from Find
		List<GameObject> temp = new List<GameObject>();
		
		removeList = new List<GameObject>();
		
		// Calculate the upper left point of the picture projector box
		upperLeftPoint = new Vector3(transform.position.x - pictureProjector.orthographicSize, 0, 
		                             transform.position.z + pictureProjector.orthographicSize);
		
		// Calculate the lower right point of the picture projector box
		lowerRightPoint = new Vector3(transform.position.x + pictureProjector.orthographicSize, 0,
		                              transform.position.z - pictureProjector.orthographicSize);
		
		// For each level block in the level, add it to the temporay list if it is
		foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("levelBlock"))
		{
			count++;
			// If the center position of the level block is within the top-down box of the picture projector, add it to the list 
			if(gameObject.transform.position.x >= upperLeftPoint.x && gameObject.transform.position.x <= lowerRightPoint.x &&
			   gameObject.transform.position.z <= upperLeftPoint.z && gameObject.transform.position.z >= lowerRightPoint.z)
			{	
				temp.Add(gameObject);
			}
		}
		
		foreach(GameObject levelBlock1 in temp)
		{
			foreach(GameObject levelBlock2 in temp)
			{
				if(levelBlock2.transform.position.y < levelBlock1.transform.position.y &&
					levelBlock2.transform.position.x == levelBlock1.transform.position.x &&
					levelBlock2.transform.position.z == levelBlock1.transform.position.z)
				{
					removeList.Add(levelBlock2);
				}
			}
		}
		
		foreach(GameObject obj in removeList)
		{
			temp.Remove(obj);
		}
		
		// Convert the list of level blocks to an array
		levelBlocks = temp.ToArray();
		
		// Shuffle the array
		for(int i = levelBlocks.Length-1; i > 1; i--)
		{
			int randomNum = UnityEngine.Random.Range(0, levelBlocks.Length);
			
			GameObject tempObj = levelBlocks[randomNum];
			levelBlocks[randomNum] = levelBlocks[i];
			levelBlocks[i] = tempObj;
		}
		
		multiple = ((int)Math.Ceiling((levelBlocks.Length/5)*0.1f))*14;

		// Disable the picture projector before the game starts so it is only enabled when it needs to be
		pictureProjector.enabled = false;
	}
	

	void Update () {
		
		if(index < levelBlocks.Length && showPicture)
		{
			int i = 0;
			int limit = (int)Math.Ceiling((Time.deltaTime*multiple)/1.0f);
			
			while(i < limit)
			{
				if(index >= levelBlocks.Length)
					break;
				
				levelBlocks[index].GetComponent<levelBlockScript>().showPicture();
				i++;
				index++;
			}
		}
		
		if(index >= levelBlocks.Length)
			globalVariable.pictureDone = true;
		
		// If End of Level, begin displaying the picture
		if(globalVariable.endLevel)
		{ 	
			globalVariable.cube.renderer.material.SetFloat("_Outline", 0.0f);
			showPicture = true;
			pictureProjector.enabled = true; // Enable the picture projector because the picture is now being displayed
		}
	}
}
