using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {

        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // player = levelManager.player
        BasicMovement movescript = player.GetComponent<BasicMovement>();
        if (other.gameObject.tag == "player")
        {
            //Drop the powerup and kill the player
        }

        

        
    }
}
