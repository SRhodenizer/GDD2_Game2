using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    *Script for handeling player movement and terrain collision detection
    * Coded by Kevin Lin and Stephen Rhodenizer 
    * GDD2 - Game 2 - Postmortem
    */

public class BasicMovement : MonoBehaviour {

    //list of all the current terrain
    List<GameObject> terrainList;

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
    float playerFall = 0.0f;

    bool playerGrounded = true;


    // Use this for initialization
    void Start () {

        //gets the terrain locations from the level manager
        LevelManager lvlMng = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        terrainList = lvlMng.platforms;

    }

    // Update is called once per frame
    void Update()
    {
        if (frames == 60)
        {
            frames = 0;
            time++;
        }

        //Remove the following code if not needed

        /*
        //Could use this to interact with upward movements
        if (Input.GetKeyDown(KeyCode.W))
        {
            //this.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
            //useless for now
        }

        //could use this to interact with downwards movement
        if (Input.GetKeyDown(KeyCode.S))
        {
            //this.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
            //useless for now
        }

        */

        
        if (Input.GetKey(KeyCode.D))//D key for moving right
        {
            //transform.position += new Vector3(0.1f, 0.0f, 0.0f);

            playerSpeed += playerAccel;
            if (playerSpeed > playerMaxSpeed)
            {
                playerSpeed = playerMaxSpeed;
            }

        }else if (Input.GetKey(KeyCode.A))  // A key for moving left on the screen
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
                if(playerSpeed >= 0)
                {
                    playerSpeed = 0.0f;
                }

            }
            else
            {
                playerSpeed -= (playerAccel + 0.01f) ;
                if(playerSpeed <= 0)
                {
                    playerSpeed = 0.0f;
                }
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
                playerJump = playerMaxJump ;
                if (playerMaxJump > 0.0f)
                {
                    playerMaxJump = playerMaxJump - playerAccel;
                }else if (playerMaxJump <= 0.0f)
                {
                    playerGrounded = false;
                }

            }
        }
        else
        {

            foreach (GameObject platform in terrainList)
            {
                if (AABBCollide(gameObject, platform) == true)
                {
                    playerJump = 0.0f;
                    playerGrounded = true;
                    playerMaxJump = 0.25f;
                    playerFall = 0.0f;
                    break;
                }
                else
                {
                    playerFall -= 0.01f;
                    playerJump = playerFall;
                }
            }

            //need to change this to some collision based thing to get the char to jump off terrain 
            /*if (transform.position.y <= 0)
            {
                playerJump = 0.0f;
                playerGrounded = true;
                playerMaxJump = 0.25f;
                playerFall = 0.0f;
            }
            else
            {
                playerFall -= 0.01f;
                playerJump = playerFall;
            }*/

        }

        transform.position += new Vector3(playerSpeed,playerJump,0.0f);
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


}
