using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 *  Create a level exit that the player can collide with 
 *  to end the level.
 *  Coded by Kevin Lin and
 */


public class LevelComplete : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        /*if(player collides with THIS = true)
         * {
         *      have code that either sends the player
         *      to the next level if there are any available
         *      or just call a end function write out the score and
         *      send player back to start screen.
         *      this could possibly go into the level manager code 
         *      
         *      -Kevin
         * }
         */ 

	}
}
