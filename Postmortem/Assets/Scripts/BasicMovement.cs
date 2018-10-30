using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    *Script for handeling player movement and terrain collision detection
    * Coded by Kevin Lin and Stephen Rhodenizer 
    * GDD2 - Game 2 - Postmortem
    */

public class BasicMovement : MonoBehaviour
{

    int frames = 0;
    int time = 0;
    //player max horizontal speed
    float playerMaxSpeed = 0.1f;
    //player acceleration 
    float playerAccel = 0.01f;
    //player horizontal speed
    float playerSpeed = 0.0f;
    //player max jump velocity
    float playerMaxJump = 0.0f;
    //player jump velocity
    float playerJump = 0.0f;
    //player specifc gravity
    public float playerFall = 0.0f;
    float playerFallMax = 0.05f;

    public bool playerGrounded = true;//if the player is able to jump or not

    public bool dead = false;//is the player dead
    public bool loseLife = false;//has the player just lost a life
    bool getScreen = false;//do we need to get the game over screen

    public int lives = 3;//the amount of lives the player has

    public bool orbEquipped; //If the player has equipped the orb or not

    public GameObject lvlMng;
    List<GameObject> terrainList;

    public GameObject pwerMng;
    //List<GameObject> enemyList;

    private void Awake()
    {
        lvlMng = GameObject.Find("LevelManager");
        terrainList = lvlMng.GetComponent<LevelManager>().platforms;
        pwerMng = GameObject.Find("PowerupManager");
        //enemyList = pwerMng.GetComponent<PowerupManager>().enemies;

        orbEquipped = false;
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (frames == 60)
        {
            frames = 0;
            time++;
        }

        //if you're not dead
        if (dead == false)
        {
            //the movement code -it looked messy-
            InputMovement();
        }

        //checks for Player Death
        if (gameObject.transform.position.y < -30)
        {
            loseLife = true;
        }
        

        //checks if player hits a hazard
        foreach (GameObject enemy in pwerMng.GetComponent<PowerupManager>().enemies)
        {
            
            if (AABBCollide(gameObject, enemy))
            {
                loseLife = true;
                //Spawns different powerup depending on hazard that killed the player
                switch (enemy.tag)
                {
                    case "Spike":
                        pwerMng.GetComponent<PowerupManager>().SpawnBounce(transform.position);
                        break;
                    case "Roamer":
                        pwerMng.GetComponent<PowerupManager>().SpawnOrb(transform.position);
                        break;

                }
                
                
                break;
            }
        }

        foreach (GameObject powerup in pwerMng.GetComponent<PowerupManager>().powerups)
        {
            if (AABBCollide(gameObject, powerup))
            {
                switch (powerup.tag)
                {
                    case "Bounce":
                        //Treat player as grounded
                        playerJump = 0.0f;
                        playerGrounded = true;
                        playerMaxJump = 0.4f;
                        playerFall = 0.0f;
                        //Force immediate bounce
                        playerJump = playerMaxJump;
                        if (playerMaxJump > 0.0f)
                        {
                            playerMaxJump = playerMaxJump - playerAccel;
                        }
                        else if (playerMaxJump <= 0.0f)
                        {
                            playerGrounded = false;
                        }
                        break;
                    case "Orb":
                        orbEquipped = true;
                        break;
                }
               

                break;
            }
        }


        if (loseLife == true)
        {
            lives--;//lose a life
            List<GameObject> lifeList = lvlMng.GetComponent<LevelManager>().lives;//gets the list of lives from the manager 
            Destroy(lifeList[lifeList.Count - 1]);//gets rid of the marker
            lifeList.Remove(lifeList[lifeList.Count - 1]);//removes the last life from the list

            lvlMng.GetComponent<LevelManager>().RespawnPlayer();//resets player

        }

        if (lives <= 0)//if you're out of lives
        {
            foreach (GameObject enemy in pwerMng.GetComponent<PowerupManager>().enemies)
            {
                Destroy(enemy);
            }
            foreach (GameObject powerup in pwerMng.GetComponent<PowerupManager>().powerups)
            {
                Destroy(powerup);
            }
            pwerMng.GetComponent<PowerupManager>().enemies.Clear();
            pwerMng.GetComponent<PowerupManager>().powerups.Clear();


            dead = true;//you ded
            if (getScreen == false)
            {
                lvlMng.GetComponent<LevelManager>().LevelUp(int.MinValue);//get the you died screen
                getScreen = true;
            }
        }

        transform.position += new Vector3(playerSpeed, playerJump, 0.0f);
        frames++;
    }

    //AABB collision detection
    public bool AABBCollide(GameObject g1, GameObject g2)
    {

        bool result = false;
        //sets bounds of the game objects that may or may not be colliding
        Bounds bounds1 = g1.GetComponent<SpriteRenderer>().bounds;
        Bounds bounds2 = g2.GetComponent<SpriteRenderer>().bounds;

        //uses AABB logic to determine if the objects are colliding 
        if (bounds1.min.x < bounds2.max.x && bounds1.max.x > bounds2.min.x && bounds1.min.y < bounds2.max.y && bounds1.max.y > bounds2.min.y)
        {
            result = true;
        }

        return result;
    }

    //checks for collsions on the player's feet
    public bool footCollide(GameObject g1, GameObject g2)
    {
        bool result = false;
        if (g1 != null && g2 != null)
        {

            //sets bounds of the game objects that may or may not be colliding
            Bounds bounds1 = g1.GetComponent<SpriteRenderer>().bounds;
            Bounds bounds2 = g2.GetComponent<SpriteRenderer>().bounds;

            //uses AABB logic to determine if the object 1's minimum y is colliding with object 2's max y
            if (bounds1.min.x < bounds2.max.x && bounds1.max.x > bounds2.min.x && bounds1.min.y < bounds2.max.y + .5f && bounds1.min.y >= bounds2.max.y)
            {
                result = true;
            }
        }
        return result;
    }

    public void InputMovement()
    {
        if (Input.GetKey(KeyCode.D))//D key for moving right
        {
            //transform.position += new Vector3(0.1f, 0.0f, 0.0f);

            playerSpeed += playerAccel;
            if (playerSpeed > playerMaxSpeed)
            {
                playerSpeed = playerMaxSpeed;
            }

        }
        else if (Input.GetKey(KeyCode.A))  // A key for moving left on the screen
        {
            //transform.position += new Vector3(-0.1f, 0.0f, 0.0f);

            playerSpeed -= playerAccel;
            if (playerSpeed < -playerMaxSpeed)
            {
                playerSpeed = -playerMaxSpeed;
            }
        }
        else
        {
            if (playerSpeed < 0)
            {
                playerSpeed += (playerAccel + 0.01f);
                if (playerSpeed >= 0)
                {
                    playerSpeed = 0.0f;
                }
            }
            else
            {
                playerSpeed -= (playerAccel + 0.01f);
                if (playerSpeed <= 0)
                {
                    playerSpeed = 0.0f;
                }
            }
        }

        foreach (GameObject platform in terrainList)
        {

            if (footCollide(gameObject, platform) == true)
            {
                playerJump = 0.0f;
                playerGrounded = true;
                playerMaxJump = 0.28f;
                playerFall = 0.0f;
                break;
            }
            else
            {
                if (playerFall != playerFallMax)

                {
                    //playerFall = playerFallMax;
                    playerFall -= 0.001f;
                }

                if (playerFall > playerFallMax)
                {
                    playerFall = playerFallMax;
                }

                playerJump = playerFall;
            }

        }

        //Space key, for jumping
        if (Input.GetKey(KeyCode.Space) == true && playerGrounded == true)
        {
            //transform.position += new Vector3(0.0f,1.0f,0.0f);

            if (transform.position.y > transform.position.y + 5.0f)
            {
                playerJump = 0.0f;
                playerGrounded = false;
            }
            else if (playerGrounded == true)
            {
                playerJump = playerMaxJump;
                if (playerMaxJump > 0.0f)
                {
                    playerMaxJump = playerMaxJump - playerAccel;
                }
                else if (playerMaxJump <= 0.0f)
                {
                    playerGrounded = false;
                }
            }
        }

    }

}
