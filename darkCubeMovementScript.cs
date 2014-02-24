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

public class darkCubeMovementScript : MonoBehaviour {
	
	private static Transform darkCube;
	
	private Quaternion rotation;
	private Vector3 radius;
	private float currentRotation;
	private bool stopMoving;
	private bool moveToward;
	private int travelSpeedMultiple;
	private int circleSpeedMultiple;
	
	private Vector3 circleStart;
	
	private float decisionTimeInterval;
	private float decisionTimeMax;
	
	public static bool goToRedPosition;
	public static bool goToBluePosition;
	public static bool goToYellowPosition;
	public static bool goToGreenPosition;
	public static bool goToOrangePosition;
	private bool targetChosen;
	
	private bool redDone;
	private bool blueDone;
	private bool yellowDone;
	private bool greenDone;
	private bool orangeDone;
	
	public bool moveBegin;
	
	private Vector3 startPosition;
	public bool startBegin;
	

	void Start () {
		radius = new Vector3(35,0,0);
		currentRotation = 0.0f;
		stopMoving = true;
		moveToward = false;
		travelSpeedMultiple = 30;
		circleSpeedMultiple = 30;

		decisionTimeInterval = 0.0f;
		decisionTimeMax = 2.0f;
	
		goToRedPosition = false;
		goToBluePosition = false;
		goToYellowPosition = false;
		goToGreenPosition = false;
		goToOrangePosition = false;
		targetChosen = true;
	
		redDone = false;
		blueDone = false;
		yellowDone = false;
		greenDone = false;
		orangeDone = false;
	
		moveBegin = false;
	
		startPosition = new Vector3(0.0f, 8.0f, 35.0f);
		startBegin = false;
		
		darkCube = GameObject.Find("darkCube").transform;
		circleStart = new Vector3(33.6f, 9.6f, -0.3f);
	}
	

	void Update () {
		
		if(globalVariable.showEndingPictures)
			return;
		
		if(!startBegin)
			transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime*75);
		
		if(transform.position == startPosition)
		{
			startBegin = true;
			globalVariable.startOfFinalLevel = false;
		}
		
		if(globalVariable.cube != null)
			transform.LookAt(globalVariable.cube.transform);
		
		if(darkCubeCollisionScript.purpleDamageTaken && !darkCubeCollisionScript.startDamage && !moveBegin)
		{
			moveToward = true;
			moveBegin = true;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		
		if(moveToward)
			moveToPoint(circleStart);
		
		if(transform.position == circleStart)
		{
			moveToward = false;
			stopMoving = false;
		}
		
		if(goToRedPosition && atRedPosition())
			stopMoving = true;

		if(goToBluePosition && atBluePosition())
			stopMoving = true;

		if(goToYellowPosition && atYellowPosition())
			stopMoving = true;

		if(goToGreenPosition && atGreenPosition())
			stopMoving = true;

		if(goToOrangePosition && atOrangePosition())
			stopMoving = true;
		
		if(darkCubeCollisionScript.blueDamageTaken && !darkCubeCollisionScript.startDamage && !blueDone)
		{
			blueDone = true;
			goToBluePosition = false;
			stopMoving = false;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		if(darkCubeCollisionScript.redDamageTaken && !darkCubeCollisionScript.startDamage && !redDone)
		{
			redDone = true;
			goToRedPosition = false;
			stopMoving = false;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		if(darkCubeCollisionScript.greenDamageTaken && !darkCubeCollisionScript.startDamage && !greenDone)
		{
			greenDone = true;
			goToGreenPosition = false;
			stopMoving = false;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		if(darkCubeCollisionScript.orangeDamageTaken && !darkCubeCollisionScript.startDamage && !orangeDone)
		{
			orangeDone = true;
			goToOrangePosition = false;
			stopMoving = false;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		if(darkCubeCollisionScript.yellowDamageTaken && !darkCubeCollisionScript.startDamage && !yellowDone)
		{
			yellowDone = true;
			goToYellowPosition = false;
			stopMoving = false;
			targetChosen = false;
			transform.GetComponent<darkCubeCollisionScript>().setOutline("standard");
		}
		
		if(!stopMoving)
			moveInCircle();
		
		if(!targetChosen)
			decisionTimeInterval += Time.deltaTime;
		
		if(decisionTimeInterval >= decisionTimeMax && !darkCubeCollisionScript.startDamage)
		{
			decisionTimeInterval = 0.0f;
			
			string target = transform.GetComponent<darkCubeCollisionScript>().getNextTarget();
			
			switch(target)
			{
				case "blue":
					goToBluePosition = true;
					transform.GetComponent<darkCubeCollisionScript>().setOutline("blue");
					break;
				
				case "red":
					goToRedPosition = true;
					transform.GetComponent<darkCubeCollisionScript>().setOutline("red");
					break;
				
				case "green":
					goToGreenPosition = true;
					transform.GetComponent<darkCubeCollisionScript>().setOutline("green");
					break;
				
				case "orange":
					goToOrangePosition = true;
					transform.GetComponent<darkCubeCollisionScript>().setOutline("orange");
					break;
				
				case "yellow":
					goToYellowPosition = true;
					transform.GetComponent<darkCubeCollisionScript>().setOutline("yellow");
					break;
				
				default:
					Debug.Log("'"+target+"' is not a valid target");
					break;
			}
			
			targetChosen = true;
		}
	}
	

	private void moveToPoint(Vector3 destination)
	{
		transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime*travelSpeedMultiple);
	}

	
	private void moveInCircle()
	{
		currentRotation += Time.deltaTime*circleSpeedMultiple;
	    rotation.eulerAngles = new Vector3(0, currentRotation, 16);
	    transform.position = rotation * radius;
	}

	
	public static bool atRedPosition()
	{
		return (2.5f < darkCube.position.z && darkCube.position.z < 3.2f && 32.5f < darkCube.position.x && darkCube.position.x < 33.6f);
	}
	

	public static bool atBluePosition()
	{
		return (2.5f < darkCube.position.z && darkCube.position.z < 3.2f && -32.5f > darkCube.position.x && darkCube.position.x > -33.6f);
	}
	

	public static bool atYellowPosition()
	{
		return (0.0f < darkCube.position.x && darkCube.position.x < 0.5f && -33.5f > darkCube.position.z && darkCube.position.z > -34.0f);
	}
	

	public static bool atGreenPosition()
	{
		return (-7.5f > darkCube.position.z && darkCube.position.z > -8.0f && -32.5f > darkCube.position.x && darkCube.position.x > -33.0f);
	}
	

	public static bool atOrangePosition()
	{
		return (-7.5f > darkCube.position.z && darkCube.position.z > -8.0f && 32.5f < darkCube.position.x && darkCube.position.x < 33.0f);
	}
}
