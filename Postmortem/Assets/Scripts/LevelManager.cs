using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    /*
     *Script for managing and updating the enteties on the current level
     * Coded by Stephen Rhodenizer 
     * GDD2 - Game 2 - Postmortem
     */

    //variables for Camera movement
    public Camera camMain;

    //game over screen
    public GameObject gameOver;

    //variables for the load screen
    public GameObject title;
    public GameObject loadScreen;

    //variables for the control screen
    public GameObject controlBG;
    public GameObject controlProp;
    public GameObject label;

    //variables for level creation
    public GameObject terrain;//the game object for terrain
    public GameObject background;//the game object for the background image
    int terrainNum = 5;//the amount of terrain blocks we want, for tests 2
    List<Vector3> terrainLocations = new List<Vector3>();//list of locations for the terrain in the level
    List<Vector3> terrainScale = new List<Vector3>();//list of all the terrain scales 

    public List<GameObject> platforms = new List<GameObject>();//a list for storing made terrain

    List<GameObject> clones = new List<GameObject>(); //empty game object variable to put instantiated things for deletion

    //player variable
    public GameObject playerPrefab;
    GameObject player = null;

    int lvl = 0;

	// Use this for initialization
	void Start () {

        LevelUp(0);//sets level to the load screen

    }
	
	// Update is called once per frame
	void Update () {

        //Camera movement
        if (player != null)
        {
            //moves right if player progresses 
            if (player.transform.position.x  > camMain.transform.position.x + camMain.pixelWidth / Screen.dpi / 2)
            {
                camMain.transform.position = new Vector3(camMain.transform.position.x + .1f, camMain.transform.position.y, camMain.transform.position.z);
            }

            //moves left if player backtracks
            if (player.transform.position.x < camMain.transform.position.x - camMain.pixelWidth / Screen.dpi / 2)
            {
                camMain.transform.position = new Vector3(camMain.transform.position.x - .1f, camMain.transform.position.y, camMain.transform.position.z);
            }
        }
	}

    //changes the level to the desired screen
    public void LevelUp(int level)
    {
        if (level != int.MinValue)
        {
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
        }
        

        lvl = level;

        if (lvl == 0)//load screen
        {
            //makes the load screen background and the title words appear 
            GameObject bg = Instantiate(loadScreen);
            GameObject clone = Instantiate(title);
            clones.Add(clone);
            clones.Add(bg);
            bg.transform.localScale = new Vector3(Screen.width / 8 / Screen.dpi, Screen.height / 4 / Screen.dpi, 0);
        }

        if (lvl == 1)//test level
        {
            //moves the Camera 
            camMain.transform.position = new Vector3(-8,0,camMain.transform.position.z);

            //sets up the level's terrain
            GameObject bg = Instantiate(background);//makes the background image
            bg.transform.position = new Vector3(bg.transform.position.x,bg.transform.position.y, 9);//send it to the back
            bg.transform.localScale = new Vector3(100*Screen.width / Screen.dpi, 2.5f*Screen.height / Screen.dpi, 0);//makes it fit screen
            clones.Add(bg);

            //hard code the terrain positions
            terrainLocations.Add(new Vector3(-(Screen.width / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3(-(Screen.width / 3 / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3((Screen.width / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3((Screen.width / 3 / Screen.dpi), -(Screen.height / 2 / Screen.dpi),1));
            terrainLocations.Add(new Vector3(((Screen.width/Screen.dpi)+ Screen.width / 2 / Screen.dpi), -(Screen.height / 2 / Screen.dpi), 1));

            //sets up the terrain for the level
            for (int i = 0; i < terrainNum; i++)
            {
                //actually spawns the cubes 
                GameObject currTerr = Instantiate(terrain, terrainLocations[i], new Quaternion(0, 0, 0, 0));
                terrainScale.Add(new Vector3(.75f, .75f, .75f)); //needs to be removed later 

                //hard code the terrain scales 
                currTerr.transform.localScale = terrainScale[i]; //sets their scale

                platforms.Add(currTerr);//adds this terrain to the list 
                clones.Add(currTerr);
            }

            //spawns the player 
            player = Instantiate(playerPrefab);
            player.transform.position = new Vector3(terrainLocations[0].x,(terrainLocations[0].y+(terrainScale[0].y)*2.5f)+((player.transform.localScale.y)),0);//at this position
        }

        //controls screen
        if (lvl == int.MaxValue)
        {
            //make a background
            GameObject bg = Instantiate(controlBG);
            bg.transform.localScale = new Vector3(Screen.width / 3 / Screen.dpi, Screen.height / 2 / Screen.dpi, 0);//makes it fit screen
            clones.Add(bg);

            //make the big controller appear 
            GameObject clone = Instantiate(controlProp);
            clones.Add(clone);

            //make the text appear 
            GameObject text =  Instantiate(label);
            clones.Add(text);

            //make the screen title 
            text =  Instantiate(title);
            text.transform.position = new Vector3(text.transform.position.x-1,text.transform.position.y+1,0);
            for (int i = 0; i < 2; i++)
            {
                text.GetComponentsInChildren<TextMesh>()[i].text = "Game Controls";
            }
            clones.Add(text);
        }

        //death screen
        if (lvl == int.MinValue)
        {
            GameObject gO = Instantiate(gameOver);
            gO.transform.position = new Vector3(camMain.transform.position.x - camMain.pixelWidth / Screen.dpi / 4, camMain.transform.position.y/ Screen.dpi / 2 + 2);
            clones.Add(gO);
        }

    }

    //handles the basic user interface 
    private void OnGUI()
    {
        //title screen menu
        if (lvl == 0)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 2 -40, Screen.height / 2 , 100, 60), "Start"))
            {
                LevelUp(1);
                
            }
            //options button
            if (GUI.Button(new Rect(Screen.width / 2 -40, Screen.height / 2 + 65, 100, 60), "Controls"))
            {
                LevelUp(int.MaxValue);//doesn't work yet but will show the controls screen when it's made 
            }
            //button that quits out of the game
            if (GUI.Button(new Rect(Screen.width / 2 -40, Screen.height / 2 + 130, 100, 60), "Quit Game"))
            {
                Application.Quit(); //closes application
            }
        }

        //game over screen 
        if (lvl == int.MinValue)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height/2 , 150, 60), "Play Again?"))
            {
                clones.Add(player);
                LevelUp(1);

            }

            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height/2 +80, 150, 60), "Return to Menu"))
            {
                clones.Add(player);
                camMain.transform.position = new Vector3(0,0,camMain.transform.position.z);
                LevelUp(0);
            }
        }

        //control screen 
        if (lvl == int.MaxValue)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 2 -80,(Screen.height - Screen.height / 8), 150, 60), "Back to the Menu"))
            {
                LevelUp(0);
            }
        }
    }
}
