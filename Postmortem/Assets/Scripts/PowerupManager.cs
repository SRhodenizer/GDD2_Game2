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
    public Queue<GameObject> powerups;
    public GameObject spikePrefab;
    public GameObject roamerPrefab;
    public GameObject powerupPrefab;


	// Use this for initialization
	void Start () {
        //enemies.Add(Instantiate(enemyPrefab));
        enemies = new List<GameObject>();
        powerups = new Queue<GameObject>();

        SpawnRoamer(new Vector3(2, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Spawns a single spike at the location
    public void SpawnSpike(Vector3 location)
    {
        enemies.Add(Instantiate(spikePrefab, location, Quaternion.identity));
    }

    //Spawns a single roamer at the location
    public void SpawnRoamer(Vector3 location)
    {
        enemies.Add(Instantiate(roamerPrefab, location, Quaternion.identity));
    }

    //Spawns appropriate powerup when player dies
    public void SpawnPowerup(Vector3 location)
    {
        if (powerups.Count > 3)
        {
            Destroy(powerups.Dequeue());
        }
        powerups.Enqueue(Instantiate(powerupPrefab, location, Quaternion.identity));
    }
}
