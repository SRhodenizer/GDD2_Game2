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
    bool respawn = false;
    float width;//width of the screen
    

    //game over screen
    public GameObject gameOver;

    //variables for the load screen
    public GameObject title;
    public GameObject loadScreen;
    public GameObject creditScreen;
    public GameObject creditLabel;
    GameObject credit;
    public bool creditFade = true;

    //variables for the control screen
    public GameObject controlBG;
    public GameObject controlProp;
    public GameObject label;

    //variables for level creation
    public GameObject terrain;//the game object for terrain
    public GameObject background;//the game object for the background image
    int terrainNum = 2;//the amount of terrain blocks we want, for tests 2
    int terrainNumLvl1 = 6;
    List<Vector3> terrainLocations = new List<Vector3>();//list of locations for the terrain in the level
    List<Vector3> terrainScale = new List<Vector3>();//list of all the terrain scales 
    Vector3 playerStart = new Vector3(); //starting point for the player
    public GameObject lvlEnd;//the gameObject for completing the level

    public List<GameObject> platforms = new List<GameObject>();//a list for storing made terrain

    public List<GameObject> clones = new List<GameObject>(); //empty game object variable to put instantiated things for deletion
    public List<GameObject> lives = new List<GameObject>();//list of the life markers for the player 
    PowerupManager spawnHazards;


    //player variable
    public GameObject playerPrefab;
    GameObject player = null;

    //the current level of the game 
    int lvl = 0;

    //variables for background music
    AudioSource[] themes;

	// Use this for initialization
	void Start () {

        camMain.aspect = 10.0f / 6.0f;//MAS IMPORTANTE

        themes = gameObject.GetComponents<AudioSource>();

        width = camMain.orthographicSize * 2.0f * Screen.width / Screen.height;

        spawnHazards = GameObject.Find("PowerupManager").GetComponent<PowerupManager>();

        LevelUp(0);//sets level to the load screen
    }
	
	// Update is called once per frame
	void Update () {

        //Camera movement
        if (player != null)
        {
            if (respawn == false)
            {
                //moves right if player progresses 
                if (player.transform.position.x > camMain.transform.position.x + camMain.pixelWidth / Screen.dpi/5 )
                {
                    camMain.transform.position = new Vector3(camMain.transform.position.x + .1f, camMain.transform.position.y, camMain.transform.position.z);
                    foreach (GameObject life in lives)//moves the lives on screen
                    {
                        life.transform.position = new Vector3(life.transform.position.x + .1f, life.transform.position.y, life.transform.position.z);
                    }
                }

                //moves left if player backtracks
                if (player.transform.position.x < camMain.transform.position.x - camMain.pixelWidth / Screen.dpi/5 )
                {
                    camMain.transform.position = new Vector3(camMain.transform.position.x - .1f, camMain.transform.position.y, camMain.transform.position.z);
                    foreach (GameObject life in lives)//moves the lives on screen
                    {
                        life.transform.position = new Vector3(life.transform.position.x - .1f, life.transform.position.y, life.transform.position.z);
                    }
                }
            }
            else
            {
                //moves right if player progresses 
                if (player.transform.position.x > camMain.transform.position.x + camMain.pixelWidth / Screen.dpi/5 )
                {
                    camMain.transform.position = new Vector3(camMain.transform.position.x + .5f, camMain.transform.position.y, camMain.transform.position.z);
                    foreach (GameObject life in lives)//moves the lives on screen
                    {
                        life.transform.position = new Vector3(life.transform.position.x + .5f, life.transform.position.y, life.transform.position.z);
                    }
                }

                //moves left if player backtracks
                if (player.transform.position.x < camMain.transform.position.x - camMain.pixelWidth / Screen.dpi/5 )
                {
                    camMain.transform.position = new Vector3(camMain.transform.position.x - .5f, camMain.transform.position.y, camMain.transform.position.z);
                    foreach (GameObject life in lives)//moves the lives on screen
                    {
                        life.transform.position = new Vector3(life.transform.position.x - .5f, life.transform.position.y, life.transform.position.z);
                    }
                }
            }
            
            //resets the respawn bool for the player 
            if (player.transform.position.y <=  playerStart.y -.02f)
            {
                respawn = false;
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

            foreach (GameObject enemy in spawnHazards.enemies)
            {
                Destroy(enemy);
            }
            
            if (player != null)
            {
                foreach (GameObject pellet in player.GetComponent<BasicMovement>().pellets)
                {
                    Destroy(pellet);
                }
            }
           
        }
        

        lvl = level;

        if (lvl == 0)//load screen
        {
            if (themes[0].isPlaying == false)
            {
                themes[1].Stop();
                themes[0].Play();
            }
            //makes the load screen background and the title words appear 
            GameObject bg = Instantiate(loadScreen);
            GameObject clone = Instantiate(title);
            clones.Add(clone);
            clones.Add(bg);
            bg.transform.localScale = new Vector3(width/10.0f,width/12.0f, 0);


            if (creditFade == true)//if you need to see the credits 
            {
                credit = Instantiate(creditScreen);
                clones.Add(credit);
                GameObject credLab = Instantiate(creditLabel);
                clones.Add(credLab);
                credit.transform.localScale = new Vector3(Screen.width / 2.0f / Screen.dpi, Screen.height / 2.5f / Screen.dpi, 0);

                //hides the normal load screen 
                bg.GetComponent<SpriteRenderer>().enabled = false;
                for (int i = 0; i < 2; i++)
                {
                    Color temp = clone.GetComponentsInChildren<TextMesh>()[i].color;
                    temp.a = 0;
                    clone.GetComponentsInChildren<TextMesh>()[i].color = temp;
                }
            }
        }

        if (lvl == 9001)//test level
        {
            //
            if (themes[1].isPlaying == false)
            {
                themes[0].Stop();
                themes[1].Play();
            }
            //moves the Camera 
            camMain.transform.position = new Vector3(-8,0,camMain.transform.position.z);

            //sets up the level's terrain
            GameObject bg = Instantiate(background);//makes the background image
            bg.transform.position = new Vector3(bg.transform.position.x,bg.transform.position.y, 9);//send it to the back
            bg.transform.localScale = new Vector3(10* width, width/2.5f, 0);//makes it fit screen
            clones.Add(bg);
            //


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
            playerStart = new Vector3(terrainLocations[0].x, (terrainLocations[0].y + (terrainScale[0].y) * 2.5f) + ((player.transform.localScale.y)), 0);
            player.tag = "Player";

            //makes player life ui
            int lifeNum = player.GetComponent<BasicMovement>().lives;
            for (int i = 0; i < lifeNum; i++)
            {
                GameObject life = Instantiate(playerPrefab);//makes a life marker 
                life.transform.localScale = new Vector3(1f,1f,1f);//makes him tiny 
                //puts him on screen
                life.transform.position = new Vector3((camMain.transform.position.x - (camMain.pixelWidth / Screen.dpi)) + (i *2*life.transform.localScale.x), (Screen.height / Screen.dpi) - 2*life.transform.localScale.y, 0);
                Destroy(life.GetComponent<BasicMovement>());//makes it so he doesnt move like a player
                lives.Add(life);//add it to a list for later use 
                
            }
        }


        //Level 2
        if (lvl == 1)
        {
            if (themes[1].isPlaying == false)
            {
                themes[0].Stop();
                themes[1].Play();
            }
            //moves the Camera 
            camMain.transform.position = new Vector3(-2, 0, camMain.transform.position.z);

            //sets up the level's terrain
            GameObject bg = Instantiate(background);//makes the background image
            bg.transform.position = new Vector3(bg.transform.position.x, bg.transform.position.y, 9);//send it to the back
            bg.transform.localScale = new Vector3(10 * width, 2 * width / 2.5f, 0);//makes it fit screen
            clones.Add(bg);

            terrainLocations.Add(new Vector3(0,-3, 1));
            terrainLocations.Add(new Vector3(14, -3, 1));
            terrainLocations.Add(new Vector3(20, -3, 1));
            terrainLocations.Add(new Vector3(27, -3, 1));
            terrainLocations.Add(new Vector3(33, -3, 1));
            terrainLocations.Add(new Vector3(40, -3, 1));


            for (int i = 0; i < terrainNumLvl1; i++)
            {
                //actually spawns the cubes 
                GameObject currTerr = Instantiate(terrain, terrainLocations[i], new Quaternion(0, 0, 0, 0));
                if (i==0)
                {
                    terrainScale.Add(new Vector3(.75f, .75f, .75f)); //needs to be removed later 
                }
                else
                {
                    terrainScale.Add(new Vector3(1.5f, .75f, .75f)); //needs to be removed later 
                }


                //hard code the terrain scales 
                currTerr.transform.localScale = terrainScale[i]; //sets their scale
                
                platforms.Add(currTerr);//adds this terrain to the list 
                clones.Add(currTerr);
            }

            //spawn roamer
            spawnHazards.SpawnRoamer(new Vector3(14, -1, 1));

            spawnHazards.SpawnSpike(new Vector3(26, -1.3f, 1));
            spawnHazards.SpawnSpike(new Vector3(27, -1.3f, 1));

            //spawns the player 
            player = Instantiate(playerPrefab);
            player.transform.position = new Vector3(terrainLocations[0].x, (terrainLocations[0].y + (terrainScale[0].y) * 2.5f) + ((player.transform.localScale.y)), 0);//at this position
            playerStart = new Vector3(terrainLocations[0].x, (terrainLocations[0].y + (terrainScale[0].y) * 2.5f) + ((player.transform.localScale.y)), 0);
            player.tag = "Player";

            //spawns the end level object 
            GameObject endLvl = Instantiate(lvlEnd);
            endLvl.transform.position = new Vector3(20,0,0);//wherever you wanna put it 
            endLvl.GetComponent<LevelComplete>().nextLevel = 100000000;//this ends the game
            clones.Add(endLvl);

            //makes player life ui
            int lifeNum = player.GetComponent<BasicMovement>().lives;
            for (int i = 0; i < lifeNum; i++)
            {
                GameObject life = Instantiate(playerPrefab);//makes a life marker 
                life.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);//makes him tiny 
                //puts him on screen
                life.transform.position = new Vector3(-width/6 - (i * life.transform.localScale.x), width/2 - 4 * life.transform.localScale.y, 0);
                Destroy(life.GetComponent<BasicMovement>());//makes it so he doesnt move like a player
                lives.Add(life);//add it to a list for later use 

            }

        }


        //controls screen
        if (lvl == int.MaxValue)
        {
            //make a background
            GameObject bg = Instantiate(controlBG);
            bg.transform.localScale = new Vector3(width/4, width/4, 0);//makes it fit screen
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

        //win screen
        if (lvl == 100000000)
        {
            Destroy(player);
            GameObject gO = Instantiate(gameOver);
            gO.transform.position = new Vector3(camMain.transform.position.x - camMain.pixelWidth / Screen.dpi / 4, camMain.transform.position.y / Screen.dpi / 2 + 2);
            TextMesh[] texts = gO.GetComponentsInChildren<TextMesh>();
            foreach (TextMesh word in texts)
            {
                word.text = "You Win!";
            }
            clones.Add(gO);
        }

        //death screen
        if (lvl == int.MinValue)
        {
            Destroy(player);
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
            if (creditFade == false)
            {
                //button that starts the game code
                if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2, 100, 60), "Start"))
                {
                    spawnHazards.enemies.Clear();
                    LevelUp(1);

                }
                //options button
                if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 65, 100, 60), "Controls"))
                {
                    LevelUp(int.MaxValue);//doesn't work yet but will show the controls screen when it's made 
                }
                //button that quits out of the game
                if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 130, 100, 60), "Quit Game"))
                {
                    Application.Quit(); //closes application
                }
            }
        }

        //game over screen 
        if (lvl == int.MinValue)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height/2 , 150, 60), "Play Again?"))
            {
                spawnHazards.enemies.Clear();
                LevelUp(1);

            }

            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height/2 +80, 150, 60), "Return to Menu"))
            {
                clones.Add(player);
                camMain.transform.position = new Vector3(0,0,camMain.transform.position.z);
                LevelUp(0);
            }
        }

        //game win screen 
        if (lvl == 100000000)
        {
            //button that starts the game code
            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height / 2, 150, 60), "Play Again?"))
            {
                spawnHazards.enemies.Clear();
                LevelUp(1);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 80, Screen.height / 2 + 80, 150, 60), "Return to Menu"))
            {
                clones.Add(player);
                camMain.transform.position = new Vector3(0, 0, camMain.transform.position.z);
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

    //respawns the player  
    public void RespawnPlayer()
    {
        BasicMovement script = player.GetComponent<BasicMovement>();
        player.transform.position = playerStart+ new Vector3(0,10,0);
        script.playerGrounded = false;
        
        script.playerFall = 0;
        script.loseLife = false;
        respawn = true;
        
    }

}
