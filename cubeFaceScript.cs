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


public class cubeFaceScript : MonoBehaviour {
	
	private enum cubeState { moving, idle, happy, sad, sleeping, endOfLevel, determined };
	
	private cubeState currentState;
	private cubeState oldState;
		
	private Transform cubeFace;
	
	// Faces
	private Texture normalFace;
	private Texture happyFace;
	private Texture sadFace;
	private Texture determinedFace;
	
	// Animations
	private List<Texture> blinking;
	private List<Texture> lookLeft;
	private List<Texture> lookRight;
	private List<Texture> sleeping;
	private List<Texture> determinedBlink;
	
	private bool endOfLevelReached;
	
	// Blinking
	private float blinkTimeDuration;
	private float blinkTimeMax;
	private int blinkIndex;
	private int oldBlinkIndex;
	private bool playBlinkForward;
	private bool blinkingPlayed;
	
	// Look Left
	private float lookLeftTimeDuration;
	private float lookLeftTimeMax;
	private int lookLeftIndex;
	private int oldLookLeftIndex;
	private bool playLookLeftForward;
	private bool lookLeftPlayed;
	private int longLeftCount;
	private bool startLeftHold;
	
	// Look Right
	private float lookRightTimeDuration;
	private float lookRightTimeMax;
	private int lookRightIndex;
	private int oldLookRightIndex;
	private bool playLookRightForward;
	private bool lookRightPlayed;
	private int longRightCount;
	private bool startRightHold;
	
	// Sleeping
	private float sleepTimeDuration;
	private float sleepTimeMax;
	private int sleepIndex;
	private int oldSleepIndex;
	private bool playSleepForward;
	private float sleepCountdown;
	private float sleepLimit;
	private float fallAsleepTimeDuration;
	private float fallAsleepTimeMax;
	private int fallAsleepIndex;
	private int oldFallAsleepIndex;
	private bool fellAsleep;
	
	// Happy
	private float happyTimeDurration;
	private float happyTimeMax;
	private bool isHappy;
	private bool redSolved;
	private bool blueSolved;
	private bool yellowSolved;
	private bool greenSolved;
	private bool orangeSolved;
	private bool purpleSolved;
	
	// Sad
	private float sadTimeDurration;
	private float sadTimeMax;
	private bool isSad;
	private int lostColors;
	
	// Moving
	private float movingTimeDuration;
	private float movingTimeMax;
	private bool movingTimeToBlink;
	
	// Idle
	private float idleTimeDuration;
	private float idleTimeMax;
	private bool idleTimeToBlink;
	private bool idleTimeToLookLeft;
	private bool idleTimeToLookRight;


	void Start () {
		currentState = cubeState.idle;
		
		endOfLevelReached = false;

		cubeFace = transform.Find("face");
		
		// Blinking
		blinkTimeDuration = 0.0f;
		blinkTimeMax = 0.01f;
		blinkIndex = 0;
		oldBlinkIndex = 0;
		playBlinkForward = true;
		blinkingPlayed = false;
		
		// Look Left
		lookLeftTimeDuration = 0.0f;
		lookLeftTimeMax = 0.02f;
		lookLeftIndex = 0;
		oldLookLeftIndex = 0;
		playLookLeftForward = true;
		lookLeftPlayed = false;
		longLeftCount = 40;
		startLeftHold = false;
		
		// Look Right
		lookRightTimeDuration = 0.0f;
		lookRightTimeMax = 0.02f;
		lookRightIndex = 0;
		oldLookRightIndex = 0;
		playLookRightForward = true;
		lookRightPlayed = false;
		longRightCount = 40;
		startRightHold = false;
		
		// Sleeping
		sleepTimeDuration = 0.0f;
		sleepTimeMax = 0.20f;
		sleepIndex = 0;
		oldSleepIndex = 0;
		playSleepForward = true;
		sleepCountdown = 0.0f;
		sleepLimit = 10.5f;
		fallAsleepTimeDuration = 0.0f;
		fallAsleepTimeMax = 0.08f;
		fallAsleepIndex = 0;
		oldFallAsleepIndex = 0;
		fellAsleep = false;
		
		// Happy
		happyTimeDurration = 0.0f;
		happyTimeMax = 2.0f;
		isHappy = false;
	    redSolved = false;
		blueSolved = false;
		yellowSolved = false;
		greenSolved = false;
		orangeSolved = false;
		purpleSolved = false;
		
		// Sad
		sadTimeDurration = 0.0f;
		sadTimeMax = 2.0f;
		isSad = false;
		lostColors = 0;
		
		// Moving
		movingTimeDuration = 0.0f;
		movingTimeMax = 2.0f;
		movingTimeToBlink = false;
		
		// Idle
		idleTimeDuration = 0.0f;
		idleTimeMax = 3.0f;
		idleTimeToBlink = false;
		idleTimeToLookLeft = false;
		idleTimeToLookRight = false;
		
		blinking = new List<Texture>();
		determinedBlink = new List<Texture>();
		lookLeft = new List<Texture>();
		lookRight = new List<Texture>();
		sleeping = new List<Texture>();
		
		loadTextures();
		
		showFace(true);
		
		if(Application.loadedLevelName == "7-1")
		{
			setCurrentFace(determinedFace);
			currentState = cubeState.determined;
		}
	}
	

	void Update () {
		
		if(globalVariable.teleporting)
			cubeFace.renderer.enabled = false;
		else if(!globalVariable.teleporting && !globalVariable.endLevel)
			cubeFace.renderer.enabled = true;
		
		oldState = currentState;
		
		if(Application.loadedLevelName == "7-1" && blinkingPlayed)
			setCurrentFace(determinedFace);
		
		// Check if Moving
		if(globalVariable.isMoving && !isSad && !isHappy && currentState != cubeState.determined)
		{
			currentState = cubeState.moving;
		}
		// Else the cube is Idle
		else if(!globalVariable.isMoving && currentState != cubeState.sleeping && !isSad && !isHappy && currentState != cubeState.determined)
			currentState = cubeState.idle;	
		
		// If transitioning between states
		if(currentState != oldState && currentState != cubeState.sleeping)
		{
			setCurrentFace(normalFace);
			
			if(oldState == cubeState.idle)
				sleepCountdown = 0.0f;
			
			if(oldState == cubeState.sleeping)
			{
				fellAsleep = false;
				resetFallAsleep();
			}
			
			idleTimeDuration = 0.0f;
			movingTimeDuration = 0.0f;
			resetLookLeftAnimation();
			resetLookRightAnimation();
			idleTimeToLookLeft = false;
			idleTimeToLookRight = false;
			idleTimeToBlink = false;
		}
			
		// Check if sad
		if(globalVariable.totalColorLoss != lostColors && currentState != cubeState.determined)
		{
			lostColors = globalVariable.totalColorLoss;
			currentState = cubeState.sad;
			isSad = true;
		}
		if(sadTimeDurration >= sadTimeMax)
		{
			sadTimeDurration = 0.0f;
			isSad = false;
			isHappy = false;
			currentState = cubeState.idle;
		}
		
		// Check if happy
		if(globalVariable.redSolved && !redSolved && currentState != cubeState.determined)
		{
			redSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(globalVariable.blueSolved && !blueSolved && currentState != cubeState.determined)
		{
			blueSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(globalVariable.yellowSolved && !yellowSolved && currentState != cubeState.determined)
		{
			yellowSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(globalVariable.greenSolved && !greenSolved && currentState != cubeState.determined)
		{
			greenSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(globalVariable.orangeSolved && !orangeSolved && currentState != cubeState.determined)
		{
			orangeSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(globalVariable.purpleSolved && !purpleSolved && currentState != cubeState.determined)
		{
			purpleSolved = true;
			currentState = cubeState.happy;
			isHappy = true;
		}
		if(happyTimeDurration >= happyTimeMax)
		{
			happyTimeDurration = 0.0f;
			isHappy = false;
			isSad = false;
			currentState = cubeState.idle;
		}

		// Call correct face animation based on the current state 
		switch(currentState)
		{
			case cubeState.moving:
				runMovingState();
				break;
			
			case cubeState.idle:
				sleepCountdown += Time.deltaTime;
			
				if(sleepCountdown >= sleepLimit)
				{
					currentState = cubeState.sleeping;
					sleepCountdown = 0.0f;
				}
				
				runIdleState();
				break;
			
			case cubeState.happy:
				setCurrentFace(happyFace);
				happyTimeDurration += Time.deltaTime;
				break;
			
			case cubeState.sad:
				setCurrentFace(sadFace);
				sadTimeDurration += Time.deltaTime;
				break;
			
			case cubeState.sleeping:
				
				fallAsleep();

				if(fellAsleep)
					playSleepingAnimation();
			
				break;
			
			case cubeState.determined:
				runDeterminedState();
				break;
			
			case cubeState.endOfLevel:
				break;
			
			default:
				Debug.Log("'"+currentState+"' is not a valid cube face state.");
				break;
		}
		
		if(globalVariable.endLevel && !endOfLevelReached)
		{
			showFace(false);
			endOfLevelReached = true;
			currentState = cubeState.endOfLevel;
		}		
	}
	

	private void loadTextures()
	{
		normalFace = (Texture)Resources.Load("cubeAssets/cubeFaces/cubeFace");
		happyFace = (Texture)Resources.Load("cubeAssets/cubeFaces/happy");
		sadFace = (Texture)Resources.Load("cubeAssets/cubeFaces/sad");
		determinedFace = (Texture)Resources.Load("cubeAssets/cubeFaces/determined");
		
		loadBlinkingTextures();
		loadDeterminedBlinkTextures();
		loadLookLeftTextures();
		loadLookRightTextures();
		loadSleepingTextures();
	}
	

	private void loadBlinkingTextures()
	{
		blinking.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/blinking/blinking1"));
		blinking.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/blinking/blinking2"));
		blinking.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/blinking/blinking3"));
		blinking.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/blinking/blinking4"));
		blinking.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/blinking/blinking5"));
	}
	

	private void loadDeterminedBlinkTextures()
	{
		determinedBlink.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/determinedBlink/determinedBlink1"));
		determinedBlink.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/determinedBlink/determinedBlink2"));
		determinedBlink.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/determinedBlink/determinedBlink3"));
		determinedBlink.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/determinedBlink/determinedBlink4"));
		determinedBlink.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/determinedBlink/determinedBlink5"));
	}
	

	private void loadLookLeftTextures()
	{
		lookLeft.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookLeft/lookLeft1"));
		lookLeft.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookLeft/lookLeft2"));
		lookLeft.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookLeft/lookLeft3"));
		lookLeft.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookLeft/lookLeft4"));
		lookLeft.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookLeft/lookLeft5"));
	}
	

	private void loadLookRightTextures()
	{
		lookRight.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookRight/lookRight1"));
		lookRight.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookRight/lookRight2"));
		lookRight.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookRight/lookRight3"));
		lookRight.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookRight/lookRight4"));
		lookRight.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/lookRight/lookRight5"));
	}
	

	private void loadSleepingTextures()
	{
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping1"));
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping2"));
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping3"));
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping4"));
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping5"));
		sleeping.Add((Texture)Resources.Load("cubeAssets/cubeFaces/animations/sleeping/sleeping6"));
	}
	

	private void showFace(bool show)
	{		
		// Show or hide face
		if(show)
			cubeFace.renderer.enabled = true;
		else
			cubeFace.renderer.enabled = false;
	}
	

	private void setCurrentFace(Texture newFace)
	{
		cubeFace.renderer.material.mainTexture = newFace;
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
	

	private void playDeterminedBlinkAnimation()
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
			setCurrentFace(determinedBlink[blinkIndex]);
	}
	

	private void playLookLeftAnimation()
	{
		if(lookLeftPlayed)
			return;
		
		lookLeftTimeDuration += Time.deltaTime;
		
		if(lookLeftTimeDuration >= lookLeftTimeMax)
		{	
			if(longLeftCount <= 0 && startLeftHold)
			{
				startLeftHold = false;
				longLeftCount = 40;
			}
			if(startLeftHold)
			{
				lookLeftTimeDuration = 0.0f;
				longLeftCount--;
				return;
			}
			
			oldLookLeftIndex = lookLeftIndex;
			
			if(playLookLeftForward)
				lookLeftIndex++;
			else if(!startLeftHold)
				lookLeftIndex--;
			
			if(lookLeftIndex >= lookLeft.Count)
			{
				lookLeftIndex = lookLeft.Count-1;
				playLookLeftForward = false;
				startLeftHold = true;
			}
			
			if(lookLeftIndex < 0)
			{
				lookLeftIndex = 0;
				playLookLeftForward = true;
				lookLeftPlayed = true;
			}
			
			lookLeftTimeDuration = 0.0f;
		}
		
		if(oldLookLeftIndex != lookLeftIndex)
			setCurrentFace(lookLeft[lookLeftIndex]);
	}
	

	private void resetLookLeftAnimation()
	{		
		lookLeftIndex = 0;
		oldLookLeftIndex = 0;
		lookLeftTimeDuration = 0.0f;
		playLookLeftForward = true;
		lookLeftPlayed = false;
	}
	

	private void playLookRightAnimation()
	{
		if(lookRightPlayed)
			return;
		
		lookRightTimeDuration += Time.deltaTime;
		
		if(lookRightTimeDuration >= lookRightTimeMax)
		{	
			if(longRightCount <= 0 && startRightHold)
			{
				startRightHold = false;
				longRightCount = 40;
			}
			if(startRightHold)
			{
				lookRightTimeDuration = 0.0f;
				longRightCount--;
				return;
			}
			
			oldLookRightIndex = lookRightIndex;
			
			if(playLookRightForward)
				lookRightIndex++;
			else if(!startRightHold)
				lookRightIndex--;
			
			if(lookRightIndex >= lookRight.Count)
			{
				lookRightIndex = lookRight.Count-1;
				playLookRightForward = false;
				startRightHold = true;
			}
			
			if(lookRightIndex < 0)
			{
				lookRightIndex = 0;
				playLookRightForward = true;
				lookRightPlayed = true;
			}
			
			lookRightTimeDuration = 0.0f;
		}
		
		if(oldLookRightIndex != lookRightIndex)
			setCurrentFace(lookRight[lookRightIndex]);
	}
	

	private void resetLookRightAnimation()
	{		
		lookRightIndex = 0;
		oldLookRightIndex = 0;
		lookRightTimeDuration = 0.0f;
		playLookRightForward = true;
		lookRightPlayed = false;
	}
	

	private void fallAsleep()
	{
		if(fellAsleep)
			return;
		
		fallAsleepTimeDuration += Time.deltaTime;
		
		if(fallAsleepTimeDuration >= fallAsleepTimeMax)
		{	
			oldFallAsleepIndex = fallAsleepIndex;
			
			fallAsleepIndex++;
			
			if(fallAsleepIndex >= blinking.Count)
			{
				resetBlinkingAnimation();
				fellAsleep = true;
				fallAsleepIndex = blinking.Count-1;
			}
			

			
			fallAsleepTimeDuration = 0.0f;
		}
		
		if(oldFallAsleepIndex != fallAsleepIndex)
			setCurrentFace(blinking[fallAsleepIndex]);
	}
	

	private void resetFallAsleep()
	{
		oldFallAsleepIndex = 0;
		fallAsleepIndex = 0;
		fallAsleepTimeDuration = 0.0f;
	}
	

	private void playSleepingAnimation()
	{
		sleepTimeDuration += Time.deltaTime;
		
		if(sleepTimeDuration >= sleepTimeMax)
		{	
			oldSleepIndex = sleepIndex;
			
			if(playSleepForward)
				sleepIndex++;
			else
				sleepIndex--;
			
			if(sleepIndex >= sleeping.Count)
			{
				sleepIndex = sleeping.Count-1;
				playSleepForward = false;
			}
			
			if(sleepIndex < 0)
			{
				sleepIndex = 0;
				playSleepForward = true;
			}
			
			sleepTimeDuration = 0.0f;
		}
		
		if(oldSleepIndex != sleepIndex)
			setCurrentFace(sleeping[sleepIndex]);
	}
	

	private void resetSleepingAnimation()
	{		
		sleepIndex = 0;
		oldSleepIndex = 0;
		sleepTimeDuration = 0.0f;
		playSleepForward = true;
	}
	

	private void runMovingState()
	{
		movingTimeDuration += Time.deltaTime;
	
		if(blinkingPlayed)
		{
			resetBlinkingAnimation();
			movingTimeToBlink = false;
		}
		
		if(movingTimeDuration >= movingTimeMax)
		{
			movingTimeToBlink = true;
			movingTimeDuration = 0.0f;
			movingTimeMax = UnityEngine.Random.Range(2.0f, 3.0f);
		}
	
		if(movingTimeToBlink)
			playBlinkingAnimation();
	}
	

	private void runDeterminedState()
	{
		movingTimeDuration += Time.deltaTime;
	
		if(blinkingPlayed)
		{
			resetBlinkingAnimation();
			movingTimeToBlink = false;
		}
		
		if(movingTimeDuration >= movingTimeMax)
		{
			movingTimeToBlink = true;
			movingTimeDuration = 0.0f;
			movingTimeMax = UnityEngine.Random.Range(2.0f, 3.0f);
		}
	
		if(movingTimeToBlink)
			playDeterminedBlinkAnimation();
	}
	

	private void runIdleState()
	{
		idleTimeDuration += Time.deltaTime;
	
		if(lookLeftPlayed)
		{
			resetLookLeftAnimation();
			idleTimeToLookLeft = false;
		}
		
		if(lookRightPlayed)
		{
			resetLookRightAnimation();
			idleTimeToLookRight = false;
		}
		
		if(blinkingPlayed)
		{
			resetBlinkingAnimation();
			idleTimeToBlink = false;
		}
		
		if(idleTimeDuration >= idleTimeMax)
		{
			int num = UnityEngine.Random.Range(1, 6);
			
			switch(num)
			{
				case 1:
					idleTimeToBlink = true;
					break;
				case 2:
					idleTimeToLookLeft = true;
					break;
				case 3:
					idleTimeToLookRight = true;
					break;
				case 4:
					idleTimeToBlink = true;
					break;
				case 5:
					idleTimeToBlink = true;
					break;
				default:
					break;
			}
			
			idleTimeDuration = 0.0f;
			idleTimeMax = UnityEngine.Random.Range(3.0f, 3.5f);
		}
	
		if(idleTimeToBlink)
			playBlinkingAnimation();
		
		if(idleTimeToLookLeft)
			playLookLeftAnimation();
		
		if(idleTimeToLookRight)
			playLookRightAnimation();
	}
}
