using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    *Script for handling an enemy
    * Either static or moving
    * Coded by Andrew Murphy
    * GDD2 - Game 2 - Postmortem
    */


public class Enemy : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // When the enemy collides with the player
    //May use AABB collision instead
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("We touchin, boys");

        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // player = levelManager.player
        BasicMovement moveScript = player.GetComponent<BasicMovement>();
        if (other.gameObject.tag == "Player")
        {

            //Drop the powerup and kill the player
            Debug.Log("We touchin, boys");
            moveScript.loseLife = true;

        }
         
    }
}
