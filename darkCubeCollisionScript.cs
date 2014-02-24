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

public class darkCubeCollisionScript : MonoBehaviour {
	
	public Material darkCubeMaterial;
	
	private string[] targetColors;
	private int targetIndex;
	
	public static bool startDamage;
	private float damageTimeInterval;
	private float damageTimeMax;
	private int damageCount;
	
	public static bool redDamageTaken;
	public static bool blueDamageTaken;
	public static bool yellowDamageTaken;
	public static bool greenDamageTaken;
	public static bool orangeDamageTaken;
	public static bool purpleDamageTaken;
	
	private bool colorIsNormal;
	private Color32 normalColor;
	private Color32 flashColor;
	private Color32 flashRed;
	private Color32 flashBlue;
	private Color32 flashYellow;
	private Color32 flashGreen;
	private Color32 flashOrange;
	private Color32 flashPurple;
	private Color32 flashGrey;
	
	private Color32 outlineStandard;
	private Color32 outlineRed;
	private Color32 outlineBlue;
	private Color32 outlineYellow;
	private Color32 outlineGreen;
	private Color32 outlineOrange;
	private Color32 outlinePurple;

	private float flashTimeInterval;
	private float flashTimeMax;
	
	private float standardOutlineWidth;
	private int outlineWidthMultiple;
	
	private bool offLaunchHit;
	
	public static bool startLaugh;
	private float flashLaughTimeInterval;
	private float flashLaughTimeMax;
	
	private bool startHit = false;
	

	void Start () {
		targetColors = new string[5];
		targetIndex = -1;
		
		startDamage = false;
		
		damageTimeInterval = 0.0f;
		damageTimeMax = 2.0f;
		damageCount = 0;
		
		redDamageTaken = false;
		blueDamageTaken = false;
		yellowDamageTaken = false;
		greenDamageTaken = false;
		orangeDamageTaken = false;
		purpleDamageTaken = false;
		
		colorIsNormal = true;
		targetColors[0] = "red";
		targetColors[1] = "blue";
		targetColors[2] = "yellow";
		targetColors[3] = "green";
		targetColors[4] = "orange";
		
		normalColor = new Color32(41, 41, 41, 255);
		flashRed = new Color32(100, 0, 0, 255);
		flashBlue = new Color32(0, 0, 150, 255);
		flashYellow = new Color32(100, 100, 0, 255);
		flashGreen = new Color32(0, 100, 0, 255);
		flashOrange = new Color32(190, 105, 30, 255);
		flashPurple = new Color32(100, 0, 100, 255);
		flashGrey = new Color32(100,100,100,255);
		
		outlineStandard = new Color32(138, 210, 241, 255);
		outlineRed = new Color32(245, 53, 44, 255);
		outlineBlue = new Color32(42, 53, 200, 255);
		outlineYellow = new Color32(237, 234, 14, 255);
		outlineGreen = new Color32(127, 224, 24, 255);
		outlineOrange = new Color32(227, 167, 53, 255);
		outlinePurple = new Color32(130,53,188,255);
		
		flashTimeInterval = 0.0f;
		flashTimeMax = 0.10f;
		standardOutlineWidth = 0.008f;
		outlineWidthMultiple = 2;
		
		flashColor = normalColor;
		changeColor(flashColor);
		
		changeOutline(outlineStandard, standardOutlineWidth);
		
		startLaugh = false;
		flashLaughTimeInterval = 0.0f;
		flashLaughTimeMax = 0.10f;
		
		offLaunchHit = false;
	}
	

	void Update () {
		
		if(globalVariable.showEndingPictures)
			return;
		
		if(Input.GetKeyDown("j"))
		{
			redDamageTaken = true;
			blueDamageTaken = true;
			yellowDamageTaken = true;
			greenDamageTaken = true;
			orangeDamageTaken = true;
			GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().monkeyFade();
			GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().kotoshiFadeIn();
			purpleDamageTaken = true;
			damageCount = 6;
			globalVariable.darkCubeDefeated = 1;
			globalVariable.globalVariablesObject.GetComponent<fileIOScript>().save();
			globalVariable.gameCam.GetComponent<cameraFade>().StartFade(new Color(255.0f, 255.0f, 255.0f), 5.5f);
		}
		
		if(transform.GetComponent<darkCubeMovementScript>().startBegin && !purpleDamageTaken && !startHit)
		{
			changeOutline(outlinePurple, standardOutlineWidth*outlineWidthMultiple);
			startLaugh = true;
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeLaughFX();
			flashColor = normalColor;
			changeColor(flashColor);
			colorIsNormal = false;
			startHit = true;
		}
					
		if(hitWithRed() && !redDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("red", 4.5f);
			flashColor = flashRed;
			startDamage = true;
			redDamageTaken = true;
			damageCount++;
		}
		if(hitWithBlue() && !blueDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("blue", 4.5f);
			flashColor = flashBlue;
			startDamage = true;
			blueDamageTaken = true;
			damageCount++;
		}
		if(hitWithYellow() && !yellowDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("yellow", 4.5f);
			flashColor = flashYellow;
			startDamage = true;
			yellowDamageTaken = true;
			damageCount++;
		}
		if(hitWithGreen() && !greenDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("green", 4.5f);
			flashColor = flashGreen;
			startDamage = true;
			greenDamageTaken = true;
			damageCount++;
		}
		if(hitWithOrange() && !orangeDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("orange", 20.0f);
			flashColor = flashOrange;
			damageTimeMax = 20.0f;
			startDamage = true;
			orangeDamageTaken = true;
			damageCount++;
			GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().monkeyFade();
			GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().kotoshiFadeIn();
		}
		if(hitWithPurple() && !purpleDamageTaken)
		{
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeDamageFX();
			globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("purple", 4.5f);
			flashColor = flashPurple;
			startDamage = true;
			purpleDamageTaken = true;
			damageCount++;
		}
		if(hitIneffective() && !startDamage)
		{
			startLaugh = true;
			if(globalVariable.isSoundFXOn)GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>().darkCubeLaughFX();
			flashColor = flashGrey;
			changeColor(flashColor);
			colorIsNormal = false;
			
			if(darkCubeMovementScript.atRedPosition())
				globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("red", 3.0f);
			if(darkCubeMovementScript.atBluePosition())
				globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("blue", 3.0f);
			if(darkCubeMovementScript.atYellowPosition())
			{
				if(offLaunchHit)
					globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("off", 3.0f);
				else
					globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("yellow", 3.0f);
			}
			if(darkCubeMovementScript.atGreenPosition())
				globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("green", 3.0f);
			if(darkCubeMovementScript.atOrangePosition())
				globalVariable.gameCam.GetComponent<smoothFollow>().cameraPan("orange", 3.0f);
		}
		
		if(startDamage)
		{
			if(damageCount >= 6)
			{
				globalVariable.gameCam.GetComponent<cameraFade>().StartFade(new Color(255.0f, 255.0f, 255.0f), 5.5f);
				globalVariable.darkCubeDefeated = 1;
				globalVariable.globalVariablesObject.GetComponent<fileIOScript>().save();
			}
			executeDamage();
		}
		
		if(startLaugh)
		{
			executeLaugh();
		}
	}

	
	private bool collisionWithCube()
	{
		if(transform.position.x-4 <= globalVariable.cube.transform.position.x && globalVariable.cube.transform.position.x <= transform.position.x+4 &&
		   transform.position.y-4 <= globalVariable.cube.transform.position.y && globalVariable.cube.transform.position.y <= transform.position.y+4 &&
		   transform.position.z-4 <= globalVariable.cube.transform.position.z && globalVariable.cube.transform.position.z <= transform.position.z+4)
		{
			return true;
		}
		else
			return false;
	}

	
	private bool hitWithRed()
	{
		return (collisionWithCube() && darkCubeMovementScript.goToRedPosition && darkCubeMovementScript.atRedPosition() && globalVariable.redActive);
	}

	
	private bool hitWithBlue()
	{
		return (collisionWithCube() && darkCubeMovementScript.goToBluePosition && darkCubeMovementScript.atBluePosition() && globalVariable.blueActive);
	}

	
	private bool hitWithYellow()
	{
		return (collisionWithCube() && darkCubeMovementScript.goToYellowPosition && darkCubeMovementScript.atYellowPosition() && globalVariable.yellowActive);
	}
	

	private bool hitWithGreen()
	{
		return (collisionWithCube() && darkCubeMovementScript.goToGreenPosition && darkCubeMovementScript.atGreenPosition() && globalVariable.greenActive);
	}

	
	private bool hitWithOrange()
	{
		return (collisionWithCube() && darkCubeMovementScript.goToOrangePosition && darkCubeMovementScript.atOrangePosition() && globalVariable.orangeActive);
	}

	
	private bool hitWithPurple()
	{
		return (collisionWithCube() && globalVariable.purpleActive);
	}
	

	private bool hitIneffective()
	{
		if(collisionWithCube())
		{
			if(hitWithRed() && redDamageTaken ||
			   hitWithBlue() && blueDamageTaken ||
			   hitWithYellow() && yellowDamageTaken ||
			   hitWithGreen() && greenDamageTaken ||
			   hitWithOrange() && orangeDamageTaken ||
			   hitWithPurple() && purpleDamageTaken ||
			   (!globalVariable.redActive && !globalVariable.blueActive && !globalVariable.yellowActive && 
				!globalVariable.greenActive && !globalVariable.orangeActive && !globalVariable.purpleActive))
			{
				return true;
			}
		}
		
		return false;
	}
	

	private void executeDamage()
	{
		damageTimeInterval += Time.deltaTime;
		
		if(damageTimeInterval < damageTimeMax)
		{
			flashTimeInterval += Time.deltaTime;
			
			if(flashTimeInterval >= flashTimeMax)
			{
				if(colorIsNormal)
				{
					changeColor(flashColor);
					shakeUp();
					colorIsNormal = false;
				}
				else
				{
					changeColor(normalColor);
					shakeDown();
					colorIsNormal = true;
				}
				flashTimeInterval = 0.0f;
			}
		}
		else
		{
			startDamage = false;
			damageTimeInterval = 0.0f;
			changeColor(normalColor);
			colorIsNormal = true;
		}
	}
	

	private void executeLaugh()
	{
		flashLaughTimeInterval += Time.deltaTime;
		
		if(flashLaughTimeInterval >= flashLaughTimeMax)
		{
			changeColor(normalColor);
			flashLaughTimeInterval = 0.0f;
			startLaugh = false;
			offLaunchHit = false;
			colorIsNormal = true;
		}
	}

	
	private void changeColor(Color32 newColor)
	{
		darkCubeMaterial.SetColor("_Color", newColor); 
	}
	

	public void setOutline(string color)
	{
		switch(color)
		{
			case "red":
				changeOutline(outlineRed, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "blue":
				changeOutline(outlineBlue, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "yellow":
				changeOutline(outlineYellow, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "green":
				changeOutline(outlineGreen, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "orange":
				changeOutline(outlineOrange, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "purple":
				changeOutline(outlinePurple, standardOutlineWidth*outlineWidthMultiple);
				break;
			case "standard":
				changeOutline(outlineStandard, standardOutlineWidth);
				break;
			default:
				Debug.Log("'"+color+"' not a valid color for outline.");
				break;
		}
	}

	
	private void changeOutline(Color32 newColor, float width)
	{
		darkCubeMaterial.SetColor("_OutlineColor", newColor);
		darkCubeMaterial.SetFloat("_Outline", width);
	}
	

	private void shakeUp()
	{
		transform.Translate(0.0f, 0.5f, 0.0f);
	}
	

	private void shakeDown()
	{
		transform.Translate(0.0f, -0.5f, 0.0f);
	}
	

	public string getNextTarget()
	{	
		targetIndex++;
		
		if(targetIndex >= targetColors.Length)
			return "none";
		
		return targetColors[targetIndex];
	}
	

	public void offLaunch()
	{
		offLaunchHit = true;
	}
}
