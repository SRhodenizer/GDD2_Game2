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
    int terrainNum = 2;//the amount of terrain blocks we want, for tests 2
    List<Vector3> terrainLocations = new List<Vector3>();//list of locations for the terrain in the level
    List<Vector3> terrainScale = new List<Vector3>();//list of all the terrain scales 

    int lvl = 0;

	// Use this for initialization
	void Start () {

        if (lvl == 0)//load screen
        {
            Instantiate(loadScreen);
            Instantiate(title);
        }

        if (lvl == 1)//test level
        {
            //sets up the level's terrain
            GameObject bg = Instantiate(background);//makes the background image
            bg.transform.localScale = new Vector3(Screen.width / 4 / Screen.dpi, Screen.height / 2 / Screen.dpi, 0);

            //hard code the terrain positions
            terrainLocations.Add(new Vector3(-(Screen.width / 2 / Screen.dpi), -(Screen.height / 2 / Screen.dpi)));//bottom left corner
            terrainLocations.Add(new Vector3(1, -2));//middle of the screen

            //hard code the terrain scales 
            terrainScale.Add(new Vector3(5, 3, 1));
            terrainScale.Add(new Vector3(3, 1, 1));

            //sets up the terrain for the level
            for (int i = 0; i < terrainNum; i++)
            {
                //actually spawns the cubes 
                GameObject currTerr = Instantiate(terrain, terrainLocations[i], new Quaternion(0, 0, 0, 0));
                currTerr.transform.localScale = terrainScale[i]; //sets their scale
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
