using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            if (Input.GetKeyDown(KeyCode.W))
            {
                this.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                this.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
            }




    }
}
