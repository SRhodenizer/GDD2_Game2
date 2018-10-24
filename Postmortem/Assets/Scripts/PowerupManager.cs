using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    *Script for tracking the list of enemies and powerups
    * Either static or moving
    * Coded by Andrew Murphy
    * GDD2 - Game 2 - Postmortem
    */
public class PowerupManager : MonoBehaviour {

    public List<GameObject> enemies;
    public List<GameObject> powerups;
    public GameObject enemyPrefab;


	// Use this for initialization
	void Start () {
        //enemies.Add(Instantiate(enemyPrefab));
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Spawns a single enemy
    void SpawnEnemy()
    {
        enemies.Add(Instantiate(enemyPrefab));
    }
}
