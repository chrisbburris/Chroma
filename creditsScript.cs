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

public class creditsScript : MonoBehaviour 
{
	public float rotateAmount; // The amount the block should rotate every frame.
	
	private float normalMultiple = 15.0f;
	private float fastMultiple = 100.0f;
	
	public bool paused = false; 
	
	private Transform frontTile;
	private Transform backTile;
	private Transform leftTile;
	private Transform rightTile;
	
	private List<Texture> creditTextures;
	
	// Determines whether it is ok to change the text shown on a given side of the credits block.
	private bool changeFront = true;
	private bool changeBack = true;
	private bool changeLeft = true;
	private bool changeRight = true;
	
	// Determines which side of the credits block is displaying the last part of the credits.
	private bool frontLast = false;
	private bool backLast = false;
	private bool leftLast = false;
	private bool rightLast = false;
	
	private int angle = 0; // Specifies what angle of rotation the credits block is currently at.
	
	// These ints index the credits array to get the next bit of text to show for the credits.
	private int frontIndex;
	private int backIndex;
	private int leftIndex;
	private int rightIndex;
	
	private bool endReached = false; // Determines whether the end of the credits has been reached.
	

	void Awake () 
    {
		rotateAmount = Time.deltaTime*normalMultiple;
		
		frontTile = transform.Find("frontTile");
        backTile = transform.Find("backTile");
        leftTile = transform.Find("leftTile");
        rightTile = transform.Find("rightTile");
		
		creditTextures = new List<Texture>();
		
		loadCreditTextures();
		
		frontIndex = 0;
 		backIndex = 2;
		leftIndex = 1;
		rightIndex = 3;

		if(globalVariable.endGame)
            endGame();
	}
	

	void Update () 
    {
		// If the 'down arrow' key is down, stop the credits block from rotating. 
		if(Input.GetKeyDown("down") && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor))
			rotateAmount = 0;
		if(Input.touchCount == 1 && Application.platform == RuntimePlatform.IPhonePlayer)
			rotateAmount = 0;
		
        // Fast forward the credits
		if(Input.GetKeyDown("right") && leftIndex > 1 && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor))
		{
			rotateAmount = Time.deltaTime*fastMultiple;
		}	
		if(Input.touchCount == 2 && leftIndex > 1 && Application.platform == RuntimePlatform.IPhonePlayer)
		{
			rotateAmount = Time.deltaTime*fastMultiple;
		}
		
		// If neither the 'down arrow' key or the 'right arrow' key is down, rotate the credits block at the defualt speed.
		if((Input.GetKeyUp("down") || Input.GetKeyUp("right"))  && (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor))
		{
			rotateAmount = Time.deltaTime*normalMultiple;
		}
		if(Input.touchCount == 0 && Application.platform == RuntimePlatform.IPhonePlayer)
		{
			rotateAmount = Time.deltaTime*normalMultiple;
		}
		
		// If the end of the credits is reached, figure out how to stop the credits block in the right place.
		if(endReached)
		{
			if(frontLast && angle > 152 && angle < 155)
				rotateAmount = 0;
			if(leftLast && angle > 242 && angle < 245)
				rotateAmount = 0;
			if(backLast && angle > 332 && angle < 335)
				rotateAmount = 0;
			if(rightLast && angle > 62 && angle < 65)
				rotateAmount = 0;
		}

		// If the end of the credits has not been reached yet, make sure no side of the credits block has the last bit of text.
		if(!endReached)
		{
			frontLast = false;
			backLast = false;
			leftLast = false;
			rightLast = false;
		}
		
		if(paused)
			rotateAmount = 0.0f;
		
		// Rotate the credits block based on rotation amount. 
		transform.Rotate(0, rotateAmount, 0);
		
		// Calculate the angle the credits block is currently at.
		angle = (int)Math.Floor((double)transform.localRotation.eulerAngles.y);
		
		
		// Front //////////////////////////////////////////////////////////////
		
		
		// Check if it's time to update the text on the front side of the credits block. If it's time, update the text.
		if(angle > 0 && angle < 10 && changeFront && frontIndex < creditTextures.Count && frontIndex >= 0)
		{
			// Get the next bit of text and make sure this side of the credits block can't be updated until it's time again.
			frontTile.renderer.material.mainTexture = creditTextures[frontIndex];
			changeFront = false;
			
			// If the front side of the credits block has the last bit of text, set the end of the credits. 
			if(frontIndex == creditTextures.Count-1)
			{				
				frontLast = true;
				endReached = true;
			}
			// Else, update this side of the credits block normally.
			else
				frontIndex += 4;
		} 
		if(angle > 10)
			changeFront = true;
		
		
		// Left ///////////////////////////////////////////////////////////////
		
		
		// Check if it's time to update the text on the left side of the credits block. If it's time, update the text.
		if(angle > 90 && angle < 100 && changeLeft && leftIndex < creditTextures.Count && leftIndex >= 0)
		{
			// Get the next bit of text and make sure this side of the credits block can't be updated until it's time again.
			leftTile.renderer.material.mainTexture = creditTextures[leftIndex];
			changeLeft = false;
			
			// If the left side of the credits block has the last bit of text, set the end of the credits.
			if(leftIndex == creditTextures.Count-1)
			{
				leftLast = true;
				endReached = true;
			}
			// Else, update this side of the credits block normally.
			else
				leftIndex += 4;
		}
		if(angle > 100)
			changeLeft = true;
		
		
		// Back ///////////////////////////////////////////////////////////////
		
		
		// Check if it's time to update the text on the back side of the credits block. If it's time, update the text.
		if(angle > 180 && angle < 190 && changeBack && backIndex < creditTextures.Count && backIndex >= 0)
		{
			// Get the next bit of text and make sure this side of the credits block can't be updated until it's time again.
			backTile.renderer.material.mainTexture = creditTextures[backIndex];
			changeBack = false;
			
			// If the back side of the credits block has the last bit of text, set the end of the credits.
			if(backIndex == creditTextures.Count-1)
			{
				backLast = true;
				endReached = true;
			}
			// Else, update this side of the credits block normally.
			else
				backIndex += 4;
		}
		if(angle > 190)
			changeBack = true;
		
		
		// Right //////////////////////////////////////////////////////////////
		
		
		// Check if it's time to update the text on the right side of the credits block. If it's time, update the text.
		if(angle > 270 && angle < 280 && changeRight && rightIndex < creditTextures.Count && rightIndex >= 0)
		{
			// Get the next bit of text and make sure this side of the credits block can't be updated until it's time again.
			rightTile.renderer.material.mainTexture = creditTextures[rightIndex];
			changeRight = false;
			
			// If the right side of the credits block has the last bit of text, set the end of the credits.
			if(rightIndex == creditTextures.Count-1)
			{
				rightLast = true;
				endReached = true;
			}
			// Else, update this side of the credits block normally.
			else
				rightIndex += 4;
		}
		if(angle > 280)
			changeRight = true;
		
	}
	

	private void loadCreditTextures()
	{
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits2"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits3"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits4"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits5"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits6"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits7"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits8"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits9"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits10"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits11"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits12"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits13"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits14"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits15"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits16"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits17"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits18"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits19"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits20"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits21"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits22"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits23"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits24"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits25"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits26"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits27"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits28"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits29"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits30"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits31"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits32"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits33"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits34"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits35"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits36"));
		creditTextures.Add((Texture)Resources.Load("creditsAssets/credits37"));
	}


	public void endGame()
    {
		AudioSource song = GameObject.Find("creditsBlock").GetComponent<AudioSource>();
		song.Stop();
		song.clip = (AudioClip)Resources.Load("soundAssets/Kotoshi");
		song.timeSamples = globalVariable.songTiming;
		song.Play();
		globalVariable.endGame = false;
	}
}
