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

public class darkCubeFaceScript : MonoBehaviour {

	private enum darkCubeState { normal, laughing, damaged, defeated };
	
	private darkCubeState currentState;
	private darkCubeState oldState;
		
	private Transform darkCubeFace;
	
	// Faces
	private Texture normalFace;
	private Texture damagedFace;
	
	// Animations
	private List<Texture> blinking;
	private List<Texture> leer;
	private List<Texture> laughing;
	
	// Blinking
	private float blinkTimeDuration;
	private float blinkTimeMax;
	private int blinkIndex;
	private int oldBlinkIndex;
	private bool playBlinkForward;
	private bool blinkingPlayed;
	
	// Leer
	private float leerTimeDuration;
	private float leerTimeMax;
	private int leerIndex;
	private int oldLeerIndex;
	private bool playLeerForward;
	private bool leerPlayed;
	private int longLeerCount;
	private bool startLeerHold;
	
	// Laughing
	private float laughingTimeDuration;
	private float laughingTimeMax;
	private int laughingIndex;
	private int oldlaughingIndex;
	private bool playlaughingForward;
	private bool laughingPlayed;
	private int longlaughingCount;
	private bool startlaughingHold;
	
	// Damaged
	public bool startDamage; 
	private float damagedTimeInterval;
	private float damagedTimeMax;
	
	// Normal
	private float normalTimeDuration;
	private float normalTimeMax;
	private bool normalTimeToBlink;
	private bool normalTimeToLeer;
	

	void Start () {
		currentState = darkCubeState.normal;
		
		darkCubeFace = transform.Find("face");
		
		// Blinking
		blinkTimeDuration = 0.0f;
		blinkTimeMax = 0.01f;
		blinkIndex = 0;
		oldBlinkIndex = 0;
		playBlinkForward = true;
		blinkingPlayed = false;
		
		// Leer
		leerTimeDuration = 0.0f;
		leerTimeMax = 0.02f;
		leerIndex = 0;
		oldLeerIndex = 0;
		playLeerForward = true;
		leerPlayed = false;
		longLeerCount = 40;
		startLeerHold = false;
		
		// Laughing
		laughingTimeDuration = 0.0f;
		laughingTimeMax = 0.02f;
		laughingIndex = 0;
		oldlaughingIndex = 0;
		playlaughingForward = true;
		laughingPlayed = false;
		longlaughingCount = 20;
		startlaughingHold = false;
		
		// Damaged
		damagedTimeInterval = 0.0f;
		damagedTimeMax = 2.0f;
		
		// Normal
		normalTimeDuration = 0.0f;
		normalTimeMax = 2.0f;
		normalTimeToBlink = false;
		normalTimeToLeer = false;
		
		blinking = new List<Texture>();
		leer = new List<Texture>();
		laughing = new List<Texture>();
		
		loadTextures();
	}
	

	void Update () {
		
		oldState = currentState;
		
		if(darkCubeCollisionScript.startDamage)
		{
			if(currentState != darkCubeState.damaged)
			{
				currentState = darkCubeState.damaged;
			}
		}
		
		if(darkCubeCollisionScript.startLaugh)
		{
			if(currentState != darkCubeState.laughing)
			{
				currentState = darkCubeState.laughing;
			}
		}
		
		if(currentState != oldState)
		{
			setCurrentFace(normalFace);
			
			resetBlinkingAnimation();
			resetLeerAnimation();
			resetLaughingAnimation();
			
			normalTimeToBlink = false;
			normalTimeToLeer = false;
		}
		
		// Call correct face animation based on the current state 
		switch(currentState)
		{
			case darkCubeState.normal:
				runNormalState();
				break;
			
			case darkCubeState.laughing:
				if(laughingPlayed)
					currentState = darkCubeState.normal;
				
				playLaughingAnimation();
				break;
			
			case darkCubeState.damaged:
				setCurrentFace(damagedFace);
			
				damagedTimeInterval += Time.deltaTime;
				if(damagedTimeInterval >= damagedTimeMax)
				{
					currentState = darkCubeState.normal;
					setCurrentFace(normalFace);
					damagedTimeInterval = 0.0f;
					startDamage = false;
				}
				break;
			
			case darkCubeState.defeated:
				break;
			
			default:
				Debug.Log("'"+currentState+"' is not a valid dark cube face state.");
				break;
		}		
	}
	

	private void loadTextures()
	{
		normalFace = (Texture)Resources.Load("darkCubeAssets/darkCubeFace");
		damagedFace = (Texture)Resources.Load("darkCubeAssets/damaged");
		
		loadBlinkingTextures();
		loadLeerTextures();
		loadLaughingTextures(); 
	}
	

	private void loadBlinkingTextures()
	{
		blinking.Add((Texture)Resources.Load("darkCubeAssets/animations/blinking/darkBlink1"));
		blinking.Add((Texture)Resources.Load("darkCubeAssets/animations/blinking/darkBlink2"));
		blinking.Add((Texture)Resources.Load("darkCubeAssets/animations/blinking/darkBlink3"));
		blinking.Add((Texture)Resources.Load("darkCubeAssets/animations/blinking/darkBlink4"));
		blinking.Add((Texture)Resources.Load("darkCubeAssets/animations/blinking/darkBlink5"));
	}
	

	private void loadLeerTextures()
	{
		leer.Add((Texture)Resources.Load("darkCubeAssets/animations/leer/leer1"));
		leer.Add((Texture)Resources.Load("darkCubeAssets/animations/leer/leer2"));
		leer.Add((Texture)Resources.Load("darkCubeAssets/animations/leer/leer3"));
		leer.Add((Texture)Resources.Load("darkCubeAssets/animations/leer/leer4"));
		leer.Add((Texture)Resources.Load("darkCubeAssets/animations/leer/leer5"));
	}
	

	private void loadLaughingTextures()
	{
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh1"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh2"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh3"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh4"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh5"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh6"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh7"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh8"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh9"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh10"));
		laughing.Add((Texture)Resources.Load("darkCubeAssets/animations/laughing/laugh11"));
	}
	

	private void setCurrentFace(Texture newFace)
	{
		darkCubeFace.renderer.material.mainTexture = newFace;
	}
	

	private void playBlinkingAnimation()
	{
		if(blinkingPlayed)
			return;
		
		blinkTimeDuration += Time.deltaTime;
		
		if(blinkTimeDuration >= blinkTimeMax)
		{	
			oldBlinkIndex = blinkIndex;
			
			if(playBlinkForward)
				blinkIndex++;
			else
				blinkIndex--;
			
			if(blinkIndex >= blinking.Count)
			{
				blinkIndex = blinking.Count-1;
				playBlinkForward = false;
			}
			
			if(blinkIndex < 0)
			{
				blinkIndex = 0;
				playBlinkForward = true;
				blinkingPlayed = true;
			}
			
			blinkTimeDuration = 0.0f;
		}
		
		if(oldBlinkIndex != blinkIndex)
			setCurrentFace(blinking[blinkIndex]);
	}
	

	private void resetBlinkingAnimation()
	{		
		blinkIndex = 0;
		oldBlinkIndex = 0;
		blinkTimeDuration = 0.0f;
		playBlinkForward = true;
		blinkingPlayed = false;
	}
	

	private void playLeerAnimation()
	{
		if(leerPlayed)
			return;
		
		leerTimeDuration += Time.deltaTime;
		
		if(leerTimeDuration >= leerTimeMax)
		{	
			if(longLeerCount <= 0 && startLeerHold)
			{
				startLeerHold = false;
				longLeerCount = 40;
			}
			if(startLeerHold)
			{
				leerTimeDuration = 0.0f;
				longLeerCount--;
				return;
			}
			
			oldLeerIndex = leerIndex;
			
			if(playLeerForward)
				leerIndex++;
			else if(!startLeerHold)
				leerIndex--;
			
			if(leerIndex >= leer.Count)
			{
				leerIndex = leer.Count-1;
				playLeerForward = false;
				startLeerHold = true;
			}
			
			if(leerIndex < 0)
			{
				leerIndex = 0;
				playLeerForward = true;
				leerPlayed = true;
			}
			
			leerTimeDuration = 0.0f;
		}
		
		if(oldLeerIndex != leerIndex)
			setCurrentFace(leer[leerIndex]);
	}
	

	private void resetLeerAnimation()
	{		
		leerIndex = 0;
		oldLeerIndex = 0;
		leerTimeDuration = 0.0f;
		playLeerForward = true;
		leerPlayed = false;
	}
	

	private void playLaughingAnimation()
	{
		if(laughingPlayed)
			return;
		
		laughingTimeDuration += Time.deltaTime;
		
		if(laughingTimeDuration >= laughingTimeMax)
		{	
			if(longlaughingCount <= 0 && startlaughingHold)
			{
				startlaughingHold = false;
				longlaughingCount = 20;
			}
			if(startlaughingHold)
			{
				laughingTimeDuration = 0.0f;
				longlaughingCount--;
				return;
			}
			
			oldlaughingIndex = laughingIndex;
			
			if(playlaughingForward)
				laughingIndex++;
			else if(!startlaughingHold)
				laughingIndex--;
			
			if(laughingIndex > 4)
				laughingTimeMax = 0.25f; 
			
			if(laughingIndex >= laughing.Count)
			{
				laughingTimeMax = 0.02f;
				laughingIndex = laughing.Count-7;
				playlaughingForward = false;
				startlaughingHold = true;
			}
			
			if(laughingIndex < 0)
			{
				laughingIndex = 0;
				playlaughingForward = true;
				laughingPlayed = true;
			}
			
			laughingTimeDuration = 0.0f;
		}
		
		if(oldlaughingIndex != laughingIndex)
			setCurrentFace(laughing[laughingIndex]);
	}
	

	private void resetLaughingAnimation()
	{		
		laughingIndex = 0;
		oldlaughingIndex = 0;
		laughingTimeDuration = 0.0f;
		laughingTimeMax = 0.02f; 
		playlaughingForward = true;
		laughingPlayed = false;
	}
	

	private void runNormalState()
	{
		normalTimeDuration += Time.deltaTime;
	
		if(leerPlayed)
		{
			resetLeerAnimation();
			normalTimeToLeer = false;
		}
		
		if(blinkingPlayed)
		{
			resetBlinkingAnimation();
			normalTimeToBlink = false;
		}
		
		if(normalTimeDuration >= normalTimeMax)
		{
			int num = UnityEngine.Random.Range(1, 6);
			
			switch(num)
			{
				case 1:
					normalTimeToBlink = true;
					break;
				case 2:
					normalTimeToBlink = true;
					break;
				case 3:
					normalTimeToBlink = true;
					break;
				case 4:
					normalTimeToLeer = true;
					break;
				case 5:
					normalTimeToLeer = true;
					break;
				default:
					break;
			}
			
			normalTimeDuration = 0.0f;
			normalTimeMax = UnityEngine.Random.Range(3.0f, 4.0f);
		}
	
		if(normalTimeToBlink)
			playBlinkingAnimation();
		
		if(normalTimeToLeer)
			playLeerAnimation();
	}
}
