using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    *Script for handling an enemy
    * Either static or moving
    * Coded by Andrew Murphy
    * GDD2 - Game 2 - Postmortem
    */


    //Comment We can use this for the spike code to or just add it in to the enemy manager. 
    //we could probs even rename this to hazards mngr instead of just enemies - kevin


public class Enemy : MonoBehaviour {

    public GameObject player;
    public GameObject pwerMng;
    
    float xBound;
    float move;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pwerMng = GameObject.Find("PowerupManager");
        xBound = transform.position.x + 1.8f;
        move = 0.03f;


    }
	
	// Update is called once per frame
	void Update () {
        //Based on prefab tag, movement goes here

        if (gameObject.tag == "Roamer")
        {
            transform.position += new Vector3(move, 0, 0);

            if (Mathf.Abs(transform.position.x) > xBound)
            {
                move = -move;
            }
        }

	}


    // When the enemy collides with the player
    //May use AABB collision instead
    /*void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collisioncheck");

        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // player = levelManager.player
        BasicMovement moveScript = player.GetComponent<BasicMovement>();
        if (other.gameObject.tag == "Player")
        {

            //Drop the powerup and kill the player
            Debug.Log("Collisioncheck");
            moveScript.loseLife = true;

        }
         
    }*/
}
