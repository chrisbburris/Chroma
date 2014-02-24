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

public class worldPictureWallScript : MonoBehaviour {
	
	public Projector pictureProjector; // The projector component attached to this object
	public AudioSource revealSound;
	
	public Material world1Picture;
	public Material world2Picture;
	public Material world3Picture;
	public Material world4Picture;
	public Material world5Picture;
	public Material world6Picture;
	
	private Transform[,] quadrants = new Transform[6, 6];
	
	private Vector3 picPiece1Location;
	private Vector3 picPiece2Location;
	private Vector3 picPiece3Location;
	private Vector3 picPiece4Location;
	private Vector3 picPiece5Location;
	private Vector3 picPiece6Location;
	private Vector3 picPiece7Location;
	private Vector3 picPiece8Location;
	private Vector3 picPiece9Location;
	
	private float revealTimeInterval = 0.0f;
	private float revealTimeMax = 0.5f;
	
	private int[,] sections;
		

	void Awake () {
		
		for(int i = 0; i < 6; i++)
		{
			for(int j = 0; j < 6; j++)
			{
				quadrants[i,j] = transform.Find(i+","+j);
			}
		}
		
		picPiece1Location = transform.Find("piece1Loc").position;
		picPiece2Location = transform.Find("piece2Loc").position;
		picPiece3Location = transform.Find("piece3Loc").position;
		picPiece4Location = transform.Find("piece4Loc").position;
		picPiece5Location = transform.Find("piece5Loc").position;
		picPiece6Location = transform.Find("piece6Loc").position;
		picPiece7Location = transform.Find("piece7Loc").position;
		picPiece8Location = transform.Find("piece8Loc").position;
		picPiece9Location = transform.Find("piece9Loc").position;
		
		sections = new int[,]
		{
			{0,0,0,0,0,0,0,0,0,0},
			{0,1,9,3,7,2,6,4,8,5},
			{0,1,9,8,4,5,6,7,3,2},
			{0,8,2,3,1,9,6,7,4,5},
			{0,8,2,9,5,1,6,7,4,3},
			{0,2,1,6,4,9,3,7,8,5},
			{0,7,2,3,4,8,6,1,9,5}
		};
		
		// Disable the picture projector before the game starts so it is only enabled when it needs to be
		pictureProjector.enabled = true;
	}
	

	void Start ()
	{
		setPicture(false, false, false, false, false, false, false, false, false);
	}
	

	void Update () {		
		///////////////////////////////////////////////////////////////////////
		/// World 1 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world1Selected)
		{	
			pictureProjector.material = world1Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(1))
			{
				World worldObject = globalVariable.gameInfo[1]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "1-1":							
							checkReveal("1-1");
							break;
						
						case "1-2":
							checkReveal("1-1");
							checkReveal("1-2");
							break;
						
						case "1-3":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							break;
						
						case "1-4":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							break;
						
						case "1-5":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							checkReveal("1-5");
							break;
						
						case "1-6":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							checkReveal("1-5");
							checkReveal("1-6");
							break;
						
						case "1-7":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							checkReveal("1-5");
							checkReveal("1-6");
							checkReveal("1-7");
							break;
						
						case "1-8":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							checkReveal("1-5");
							checkReveal("1-6");
							checkReveal("1-7");
							checkReveal("1-8");
							break;
						
						case "1-9":
							checkReveal("1-1");
							checkReveal("1-2");
							checkReveal("1-3");
							checkReveal("1-4");
							checkReveal("1-5");
							checkReveal("1-6");
							checkReveal("1-7");
							checkReveal("1-8");
							checkReveal("1-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
		
		
		///////////////////////////////////////////////////////////////////////
		/// World 2 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world2Selected)
		{
			pictureProjector.material = world2Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(2))
			{
				World worldObject = globalVariable.gameInfo[2]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "2-1":							
							checkReveal("2-1");
							break;
						
						case "2-2":
							checkReveal("2-1");
							checkReveal("2-2");
							break;
						
						case "2-3":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							break;
						
						case "2-4":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							break;
						
						case "2-5":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							checkReveal("2-5");
							break;
						
						case "2-6":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							checkReveal("2-5");
							checkReveal("2-6");
							break;
						
						case "2-7":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							checkReveal("2-5");
							checkReveal("2-6");
							checkReveal("2-7");
							break;
						
						case "2-8":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							checkReveal("2-5");
							checkReveal("2-6");
							checkReveal("2-7");
							checkReveal("2-8");
							break;
						
						case "2-9":
							checkReveal("2-1");
							checkReveal("2-2");
							checkReveal("2-3");
							checkReveal("2-4");
							checkReveal("2-5");
							checkReveal("2-6");
							checkReveal("2-7");
							checkReveal("2-8");
							checkReveal("2-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
		
		
		///////////////////////////////////////////////////////////////////////
		/// World 3 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world3Selected)
		{
			pictureProjector.material = world3Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(3))
			{
				World worldObject = globalVariable.gameInfo[3]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "3-1":							
							checkReveal("3-1");
							break;
						
						case "3-2":
							checkReveal("3-1");
							checkReveal("3-2");
							break;
						
						case "3-3":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							break;
						
						case "3-4":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							break;
						
						case "3-5":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							checkReveal("3-5");
							break;
						
						case "3-6":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							checkReveal("3-5");
							checkReveal("3-6");
							break;
						
						case "3-7":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							checkReveal("3-5");
							checkReveal("3-6");
							checkReveal("3-7");
							break;
						
						case "3-8":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							checkReveal("3-5");
							checkReveal("3-6");
							checkReveal("3-7");
							checkReveal("3-8");
							break;
						
						case "3-9":
							checkReveal("3-1");
							checkReveal("3-2");
							checkReveal("3-3");
							checkReveal("3-4");
							checkReveal("3-5");
							checkReveal("3-6");
							checkReveal("3-7");
							checkReveal("3-8");
							checkReveal("3-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
		
		
		///////////////////////////////////////////////////////////////////////
		/// World 4 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world4Selected)
		{
			pictureProjector.material = world4Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(4))
			{
				World worldObject = globalVariable.gameInfo[4]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "4-1":							
							checkReveal("4-1");
							break;
						
						case "4-2":
							checkReveal("4-1");
							checkReveal("4-2");
							break;
						
						case "4-3":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							break;
						
						case "4-4":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							break;
						
						case "4-5":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							checkReveal("4-5");
							break;
						
						case "4-6":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							checkReveal("4-5");
							checkReveal("4-6");
							break;
						
						case "4-7":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							checkReveal("4-5");
							checkReveal("4-6");
							checkReveal("4-7");
							break;
						
						case "4-8":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							checkReveal("4-5");
							checkReveal("4-6");
							checkReveal("4-7");
							checkReveal("4-8");
							break;
						
						case "4-9":
							checkReveal("4-1");
							checkReveal("4-2");
							checkReveal("4-3");
							checkReveal("4-4");
							checkReveal("4-5");
							checkReveal("4-6");
							checkReveal("4-7");
							checkReveal("4-8");
							checkReveal("4-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
		
		
		///////////////////////////////////////////////////////////////////////
		/// World 5 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world5Selected)
		{
			pictureProjector.material = world5Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(5))
			{
				World worldObject = globalVariable.gameInfo[5]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "5-1":							
							checkReveal("5-1");
							break;
						
						case "5-2":
							checkReveal("5-1");
							checkReveal("5-2");
							break;
						
						case "5-3":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							break;
						
						case "5-4":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							break;
						
						case "5-5":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							checkReveal("5-5");
							break;
						
						case "5-6":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							checkReveal("5-5");
							checkReveal("5-6");
							break;
						
						case "5-7":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							checkReveal("5-5");
							checkReveal("5-6");
							checkReveal("5-7");
							break;
						
						case "5-8":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							checkReveal("5-5");
							checkReveal("5-6");
							checkReveal("5-7");
							checkReveal("5-8");
							break;
						
						case "5-9":
							checkReveal("5-1");
							checkReveal("5-2");
							checkReveal("5-3");
							checkReveal("5-4");
							checkReveal("5-5");
							checkReveal("5-6");
							checkReveal("5-7");
							checkReveal("5-8");
							checkReveal("5-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
		
		
		///////////////////////////////////////////////////////////////////////
		/// World 6 Picture ///////////////////////////////////////////////////
		///////////////////////////////////////////////////////////////////////
		if(globalVariable.world6Selected)
		{
			pictureProjector.material = world6Picture; // Display the picture for this world.
			
			setPicture(false, false, false, false, false, false, false, false, false);
			
			// If there is data saved for this world, determine which picture sections to show based on player progress.
			if(globalVariable.gameInfo.ContainsKey(6))
			{
				World worldObject = globalVariable.gameInfo[6]; // Get the necessary data for this world. 
				
				// If there is at least one level that has been completed in this world, determine what picture sections to show.
				if(worldObject.getLevels().Count != 0)
				{
					// Get the name of the farthest level completed in this world.
					string levelName = worldObject.getLevels()[worldObject.getLevels().Count-1].getLevelName();
					
					// Determine what picture sections to show based on the farthest level completed.
					switch(levelName)
					{
						case "6-1":							
							checkReveal("6-1");
							break;
						
						case "6-2":
							checkReveal("6-1");
							checkReveal("6-2");
							break;
						
						case "6-3":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							break;
						
						case "6-4":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							break;
						
						case "6-5":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							checkReveal("6-5");
							break;
						
						case "6-6":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							checkReveal("6-5");
							checkReveal("6-6");
							break;
						
						case "6-7":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							checkReveal("6-5");
							checkReveal("6-6");
							checkReveal("6-7");
							break;
						
						case "6-8":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							checkReveal("6-5");
							checkReveal("6-6");
							checkReveal("6-7");
							checkReveal("6-8");
							break;
						
						case "6-9":
							checkReveal("6-1");
							checkReveal("6-2");
							checkReveal("6-3");
							checkReveal("6-4");
							checkReveal("6-5");
							checkReveal("6-6");
							checkReveal("6-7");
							checkReveal("6-8");
							checkReveal("6-9");
							break;
						
						default:
							Debug.Log("'"+levelName+"' is not a valid level name.");
							break;
					}
				}
				// No levels in this world have been completed, make the entire picture blank. 
				else
					setPicture(false, false, false, false, false, false, false, false, false);
			}
		}
	}
	

	private void setPicture(bool set1, bool set2, bool set3, bool set4, bool set5, bool set6, bool set7, bool set8, bool set9)
	{
		// Section 1 of picture.
		if(set1)
			setSection1(10);
		else
			setSection1(0);
		
		// Section 2 of picture.
		if(set2)
			setSection2(10);
		else
			setSection2(0);
		
		// Section 3 of picture.
		if(set3)
			setSection3(10);
		else
			setSection3(0);
		
		// Section 4 of picture.
		if(set4)
			setSection4(10);
		else
			setSection4(0);
		
		// Section 5 of picture.
		if(set5)
			setSection5(10);
		else
			setSection5(0);
		
		// Section 6 of picture.
		if(set6)
			setSection6(10);
		else
			setSection6(0);
		
		// Section 7 of picture.
		if(set7)
			setSection7(10);
		else
			setSection7(0);
		
		// Section 8 of picture.
		if(set8)
			setSection8(10);
		else
			setSection8(0);
		
		// Section 9 of picture.
		if(set9)
			setSection9(10);
		else
			setSection9(0);
	}
	

	private void setSection1(int flag)
	{
		quadrants[0,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[0,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection2(int flag)
	{
		quadrants[0,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[0,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection3(int flag)
	{
		quadrants[0,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[0,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[1,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection4(int flag)
	{
		quadrants[2,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[2,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection5(int flag)
	{
		quadrants[2,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[2,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection6(int flag)
	{
		quadrants[2,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[2,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[3,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection7(int flag)
	{
		quadrants[4,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[4,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,0].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,1].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection8(int flag)
	{
		quadrants[4,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[4,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,2].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,3].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void setSection9(int flag)
	{
		quadrants[4,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[4,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,4].GetComponent<levelBlockScript>().worldPictureSet(flag);
		quadrants[5,5].GetComponent<levelBlockScript>().worldPictureSet(flag);
	}
	

	private void checkReveal(string levelName)
	{
		int worldNum = Convert.ToInt32(levelName.Substring(0,1));
		int levelNum = Convert.ToInt32(levelName.Substring(2));
		
		int picture = 0;
		
		switch(worldNum)
		{
			case 1:
				picture = globalVariable.picture1Number;
				break;
			case 2:
				picture = globalVariable.picture2Number;
				break;
			case 3:
				picture = globalVariable.picture3Number;
				break;
			case 4:
				picture = globalVariable.picture4Number;
				break;
			case 5:
				picture = globalVariable.picture5Number;
				break;
			case 6:
				picture = globalVariable.picture6Number;
				break;
			default:
				break;
		}
		
		if(picture <= levelNum-1)
		{
			revealTimeInterval += Time.deltaTime;
		
			if(revealTimeInterval >= revealTimeMax)
			{
				revealSection(sections[worldNum, levelNum]);
				switch(worldNum)
				{
					case 1:
						Instantiate(globalVariable.redPicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					case 2:
						Instantiate(globalVariable.bluePicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					case 3:
						Instantiate(globalVariable.yellowPicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					case 4:
						Instantiate(globalVariable.greenPicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					case 5:
						Instantiate(globalVariable.orangePicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					case 6:
						Instantiate(globalVariable.purplePicPieceParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
					default:
						Instantiate(globalVariable.goldParticles, getLocation(sections[worldNum, levelNum]), Quaternion.identity);
						break;
				}
				revealSound.Play();
				
				switch(worldNum)
				{
					case 1:
						globalVariable.picture1Number = levelNum;
						break;
					case 2:
						globalVariable.picture2Number = levelNum;
						break;
					case 3:
						globalVariable.picture3Number = levelNum;
						break;
					case 4:
						globalVariable.picture4Number = levelNum;
						break;
					case 5:
						globalVariable.picture5Number = levelNum;
						break;
					case 6:
						globalVariable.picture6Number = levelNum;
						break;
					default:
						break;
				}
				
				globalVariable.globalVariablesObject.GetComponent<fileIOScript>().save();
			}
		}
		else if(picture > levelNum-1)
		{
			revealSection(sections[worldNum, levelNum]);
		}
		
	}
	

	private Vector3 getLocation(int section)
	{				
		switch(section)
		{
			case 1:
				return picPiece1Location;
			case 2:
				return picPiece2Location;
			case 3:
				return picPiece3Location;
			case 4:
				return picPiece4Location;;
			case 5:
				return picPiece5Location;
			case 6:
				return picPiece6Location;
			case 7:
				return picPiece7Location;
			case 8:
				return picPiece8Location;
			case 9:
				return picPiece9Location;
			default:
				Debug.Log("'"+section+"' is not a valid section number.");
				return picPiece5Location;
		}
	}
	

	private void revealSection(int section)
	{
		switch(section)
		{
			case 1:
				setSection1(10);
				break;
			case 2:
				setSection2(10);
				break;
			case 3:
				setSection3(10);
				break;
			case 4:
				setSection4(10);
				break;
			case 5:
				setSection5(10);
				break;
			case 6:
				setSection6(10);
				break;
			case 7:
				setSection7(10);
				break;
			case 8:
				setSection8(10);
				break;
			case 9:
				setSection9(10);
				break;
			default:
				Debug.Log("'"+section+"' is not a valid section number.");
				break;
		}
	}
	

	private void determineShowPicture(int worldNum, int levelNum)
	{
		switch(levelNum)
		{
			case 1:
				revealSection(sections[worldNum, levelNum]);
				break;
			case 2:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				break;
			case 3:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				break;
			case 4:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				break;
			case 5:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				revealSection(sections[worldNum, levelNum-4]);
				break;
			case 6:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				revealSection(sections[worldNum, levelNum-4]);
				revealSection(sections[worldNum, levelNum-5]);
				break;
			case 7:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				revealSection(sections[worldNum, levelNum-4]);
				revealSection(sections[worldNum, levelNum-5]);
				revealSection(sections[worldNum, levelNum-6]);
				break;
			case 8:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				revealSection(sections[worldNum, levelNum-4]);
				revealSection(sections[worldNum, levelNum-5]);
				revealSection(sections[worldNum, levelNum-6]);
				revealSection(sections[worldNum, levelNum-7]);
				break;
			case 9:
				revealSection(sections[worldNum, levelNum]);
				revealSection(sections[worldNum, levelNum-1]);
				revealSection(sections[worldNum, levelNum-2]);
				revealSection(sections[worldNum, levelNum-3]);
				revealSection(sections[worldNum, levelNum-4]);
				revealSection(sections[worldNum, levelNum-5]);
				revealSection(sections[worldNum, levelNum-6]);
				revealSection(sections[worldNum, levelNum-7]);
				revealSection(sections[worldNum, levelNum-8]);
				break;
			default:
				Debug.Log("'"+levelNum+"' is not a valid level number.");
				break;
		}
	}
	

	private void showPicture(string levelName)
	{
		int worldNum = Convert.ToInt32(levelName.Substring(0,1));
		int levelNum = Convert.ToInt32(levelName.Substring(2));
		
		determineShowPicture(worldNum, levelNum);
	}
}
