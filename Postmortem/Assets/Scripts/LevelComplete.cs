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
    GameObject lvlMng;
    LevelManager script;
    public int nextLevel;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        lvlMng = GameObject.Find("Level Manager");
        script = lvlMng.GetComponent<LevelManager>();
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

        //if you collide with this win the game
        if (AABBCollide(gameObject, player))
        {
            script.LevelUp(nextLevel);
        }

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
