using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// MAZE PROC GEN LAB
// all students: complete steps 1-6, as listed in this file
// optional: if you're up for a bit of a mind safari, complete the "extra tasks" to do at the very bottom

// STEP 1: ======================================================================================
// put this script on a Sphere... it SHOULD move around, and drop a path of floor tiles behind it

public class Pathmaker : MonoBehaviour {

	// STEP 2: ============================================================================================
	// translate the pseudocode below

	//	DECLARE CLASS MEMBER VARIABLES:
	//	Declare a private integer called counter that starts at 0; 		// counter will track how many floor tiles I've instantiated
	//	Declare a public Transform called floorPrefab, assign the prefab in inspector;
	//	Declare a public Transform called pathmakerSpherePrefab, assign the prefab in inspector; 		// you'll have to make a "pathmakerSphere" prefab later
	public int counter = 0;
	public Transform floorPrefab;
	public Transform pathmakerSpherePrefab;
	public GameObject floorGO;
	public GameObject sphereGO;
	private bool turned = false;
	private float createIndex;
	public position_control pc;
	public Camera cam;
	public Transform camT;
	public Sprite floor1;
	public Sprite floor2;
	public Sprite floor3;
	public Sprite floor4;
	private int chooseFloor;
	private bool activateR = false;
	

    void Start()
    {
		chooseFloor = UnityEngine.Random.Range(1, 5);
		counter = UnityEngine.Random.Range(-50, 50);
		createIndex = UnityEngine.Random.Range(0.9f, 0.97f);
    }

    void Update () {
		if (Input.GetKeyDown(KeyCode.R) && activateR == true)
		{
			Global.floorCounter = 0;
			counter = 0;
			pc.x_pos.Clear();
			pc.y_pos.Clear();
			cam.orthographicSize = 20;
			camT.transform.position = new Vector3(0, 1, 0);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			activateR = false;
			

		}
		//		If counter is less than 50, then:
		//			Generate a random number from 0.0f to 1.0f;
		//			If random number is less than 0.25f, then rotate myself 90 degrees;
		//				... Else if number is 0.25f-0.5f, then rotate myself -90 degrees;
		//				... Else if number is 0.99f-1.0f, then instantiate a pathmakerSpherePrefab clone at my current position;
		//			// end elseIf
		if (counter < 500 && Global.floorCounter < 500)
        {
			float randomNum = UnityEngine.Random.Range(0.0f, 1.0f);
			if (randomNum < 0.1f && turned == false)
			{
				transform.rotation = Quaternion.Euler(0, 0, 90);
				turned = true;
			}
			else if (randomNum >= 0.1f && randomNum < 0.2f && turned == false)
			{
				transform.rotation = Quaternion.Euler(0, 0, -90);
				turned = true;
			}
			else if (randomNum >= 0.2f && randomNum < 0.3f && turned == false)
			{
				transform.rotation = Quaternion.Euler(0, 0, -180);
				turned = true;
			}
			else if (randomNum >= 0.3f && randomNum < 0.4f && turned == false)
			{
				transform.rotation = Quaternion.Euler(0, 0, 180);
				turned = true;
			}
			else if (randomNum >= createIndex && randomNum <= 1.0f)
			{
				int zeroone = UnityEngine.Random.Range(0, 2);
				if (zeroone == 1)
				{
					GameObject sphere = Instantiate(pathmakerSpherePrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0));
				}
				else
				{
					GameObject sphere = Instantiate(pathmakerSpherePrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 90));
				}
			}

			
			Collider[] floors = Physics.OverlapSphere(transform.position, 0.1f);
			if (floors.Length == 1)
			{

				if (floorPrefab != null)
				{
					GameObject floor = Instantiate(floorPrefab.gameObject, transform.position, Quaternion.Euler(0, 0, 0));
					SpriteRenderer floorSpr = floor.GetComponent<SpriteRenderer>();
					if (chooseFloor == 1)
                    {
						floorSpr.sprite = floor1;
                    }else if(chooseFloor == 2)
                    {
						floorSpr.sprite = floor2;
                    }else if(chooseFloor == 3)
                    {
						floorSpr.sprite = floor3;
                    }else if(chooseFloor == 4)
                    {
						floorSpr.sprite = floor4;
                    }
				}


				pc.x_pos.Add(transform.position.x);
				pc.y_pos.Add(transform.position.y);
				Global.floorCounter += 1;
			}
            
			transform.Translate(10, 0, 0);
			counter += 1;
			if (counter > 25 && counter < 200)
            {
				turned = false;
            }else if (counter > 400)
            {
				turned = false;
            }
        }
        else
        {
			activateR = true;
        }
		/*
        else
        {
			Destroy(gameObject);
        }
		

		if (Global.floorCounter > 500)
        {
			Destroy(gameObject);
        }
		*/
//			Instantiate a floorPrefab clone at current position;
//			Move forward ("forward", as in, the direction I'm currently facing) by 5 units;
//			Increment counter;
//		Else:
//			Destroy my game object; 		// self destruct if I've made enough tiles already
	}

    private GameObject Instantiate(GameObject gameObject, Vector3 newPos)
    {
        throw new System.NotImplementedException();
    }
	/*
	public bool CheckOverlap()
    {
		Collider[] floors = Physics.OverlapSphere(transform.position, 0.1f);
		if (floors.Length > 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	*/
} 

public class Global
{
	public static int floorCounter = 0;
	
}

// MORE STEPS BELOW!!!........

// STEP 3: =====================================================================================
// implement, test, and stabilize the system

//	IMPLEMENT AND TEST:
//	- save your scene! the code could potentially be infinite / exponential, and crash Unity
//	- put Pathmaker.cs on a sphere, configure all the prefabs in the Inspector, and test it to make sure it works
//	STABILIZE: 
//	- code it so that all the Pathmakers can only spawn a grand total of 500 tiles in the entire world; how would you do that?
//	- hint: declare a "public static int" and have each Pathmaker check this "globalTileCount", somewhere in your code? 
//      -  What is a 'static'?  Static???  Simply speak the password "static" to the instructor and knowledge will flow.
//	- Perhaps... if there already are enough tiles maybe the Pathmaker could Destroy my game object

// STEP 4: ======================================================================================
// tune your values...

// a. how long should a pathmaker live? etc.  (see: static  ---^)
// b. how would you tune the probabilities to generate lots of long hallways? does it... work?
// c. tweak all the probabilities that you want... what % chance is there for a pathmaker to make a pathmaker? is that too high or too low?



// STEP 5: ===================================================================================
// maybe randomize it even more?

// - randomize 2 more variables in Pathmaker.cs for each different Pathmaker... you would do this in Start()
// - maybe randomize each pathmaker's lifetime? maybe randomize the probability it will turn right? etc. if there's any number in your code, you can randomize it if you move it into a variable



// STEP 6:  =====================================================================================
// art pass, usability pass

// - move the game camera to a position high in the world, and then point it down, so we can see your world get generated
// - CHANGE THE DEFAULT UNITY COLORS
// - add more detail to your original floorTile placeholder -- and let it randomly pick one of 3 different floorTile models, etc. so for example, it could randomly pick a "normal" floor tile, or a cactus, or a rock, or a skull
// - or... make large city tiles and create a city.  Set the camera low so and une the values so the city tiles get clustered tightly together.

//		- MODEL 3 DIFFERENT TILES IN BLENDER.  CREATE SOMETHING FROM THE DEEP DEPTHS OF YOUR MIND TO PROCEDURALLY GENERATE. 
//		- THESE TILES CAN BE BASED ON PAST MODELS YOU'VE MADE, OR NEW.  BUT THEY NEED TO BE UNIQUE TO THIS PROJECT AND CLEARLY TILE-ABLE.

//		- then, add a simple in-game restart button; let us press [R] to reload the scene and see a new level generation
// - with Text UI, name your proc generation system ("AwesomeGen", "RobertGen", etc.) and display Text UI that tells us we can press [R]


// OPTIONAL EXTRA TASKS TO DO, IF YOU WANT / DARE: ===================================================

// AVOID SPAWNING A TILE IN THE SAME PLACE AS ANOTHER TILE  https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
// Check out the Physics.OverlapSphere functionality... 
//     If the collider is overlapping any others (the tile prefab has one), prevent a new tile from spawning and move forward one space. 


// DYNAMIC CAMERA:
// position the camera to center itself based on your generated world...
// 1. keep a list of all your spawned tiles
// 2. then calculate the average position of all of them (use a for() loop to go through the whole list) 
// 3. then move your camera to that averaged center and make sure fieldOfView is wide enough?

// BETTER UI:
// learn how to use UI Sliders (https://unity3d.com/learn/tutorials/topics/user-interface-ui/ui-slider) 
// let us tweak various parameters and settings of our tech demo
// let us click a UI Button to reload the scene, so we don't even need the keyboard anymore.  Throw that thing out!

// WALL GENERATION
// add a "wall pass" to your proc gen after it generates all the floors
// 1. raycast downwards around each floor tile (that'd be 8 raycasts per floor tile, in a square "ring" around each tile?)
// 2. if the raycast "fails" that means there's empty void there, so then instantiate a Wall tile prefab
// 3. ... repeat until walls surround your entire floorplan
// (technically, you will end up raycasting the same spot over and over... but the "proper" way to do this would involve keeping more lists and arrays to track all this data)
