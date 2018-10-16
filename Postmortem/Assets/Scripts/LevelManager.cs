using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    /*
     *Script for managing and updating the enteties on the current level
     * Coded by Stephen Rhodenizer 
     * GDD2 - Game 2 - Postmortem
     */

    //variables for the load screen
    public GameObject title;
    public GameObject loadScreen;

    //variables for level creation
    public GameObject terrain;//the game object for terrain
    public GameObject background;//the game object for the background image
    int terrainNum = 4;//the amount of terrain blocks we want, for tests 2
    List<Vector3> terrainLocations = new List<Vector3>();//list of locations for the terrain in the level
    List<Vector3> terrainScale = new List<Vector3>();//list of all the terrain scales 

    public List<GameObject> platforms = new List<GameObject>();//a list for storing made terrain

    GameObject clone; //empty game object variable to put instantiated things for deletion

    //player variable
    public GameObject playerPrefab;
    GameObject player;

    int lvl = 0;

	// Use this for initialization
	void Start () {

        LevelUp(0);//sets level to the load screen

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //changes the level to the desired screen
    private void LevelUp(int level)
    {
        lvl = level;

        if (lvl == 0)//load screen
        {
            //makes the load screen background and the title words appear 
            GameObject bg = Instantiate(loadScreen);
            clone = Instantiate(title);
            bg.transform.localScale = new Vector3(Screen.width / 8 / Screen.dpi, Screen.height / 4 / Screen.dpi, 0);
        }

        if (lvl == 1)//test level
        {
            //sets up the level's terrain
            GameObject bg = Instantiate(background);//makes the background image
            bg.transform.position = new Vector3(bg.transform.position.x,bg.transform.position.y, 9);//makes it fit screen
            bg.transform.localScale = new Vector3(Screen.width /2.5f/ Screen.dpi, Screen.height / Screen.dpi, 0);//makes it fit screen

            //hard code the terrain positions
            terrainLocations.Add(new Vector3(-(Screen.width / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3(-(Screen.width / 3 / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3((Screen.width / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3((Screen.width / 3 / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));

            //hard code the terrain scales 
            //terrainScale.Add(new Vector3(.75f, .75f, .75f));

            //sets up the terrain for the level
            for (int i = 0; i < terrainNum; i++)
            {
                //actually spawns the cubes 
                GameObject currTerr = Instantiate(terrain, terrainLocations[i], new Quaternion(0, 0, 0, 0));
                terrainScale.Add(new Vector3(.75f, .75f, .75f)); //needs to be removed later 
                currTerr.transform.localScale = terrainScale[i]; //sets their scale
                platforms.Add(currTerr);//adds this terrain to the list 
            }

            //spawns the player 
            player = Instantiate(playerPrefab);
            player.transform.position = new Vector3(terrainLocations[0].x,(terrainLocations[0].y+(terrainScale[0].y)*2.5f)+((player.transform.localScale.y/2)),0);//at this position
        }
    }

    //handles the basic user interface 
    private void OnGUI()
    {
        //title screen menu
        if (lvl == 0)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 4 - 50, Screen.height / 3 , 100, 60), "Start"))
            {
                LevelUp(1);
                Destroy(clone);
            }
            //options button
            if (GUI.Button(new Rect(Screen.width / 4 - 50, Screen.height / 3 + 65, 100, 60), "Controls"))
            {
                LevelUp(int.MaxValue);//doesn't work yet but will show the controls screen when it's made 
            }
            //button that quits out of the game
            if (GUI.Button(new Rect(Screen.width / 4 - 50, Screen.height / 3 + 130, 100, 60), "Quit Game"))
            {
                Application.Quit(); //closes application
            }
        }
    }
}
