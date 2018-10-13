using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

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
            //need to change this to some collision based thing to get the char to jump off terrain 
            if (transform.position.y <= 0)
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
            }

        }






        
        transform.position += new Vector3(playerSpeed,playerJump,0.0f);
        frames++;
    }
    

}
