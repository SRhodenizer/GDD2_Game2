using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     *Script for fading the credit scene out 
     * Coded by Stephen Rhodenizer 
     * GDD2 - Game 2 - Postmortem
     */

public class Fade : MonoBehaviour {

    LevelManager lvlMng;
    bool switched = false;

	// Use this for initialization
	void Start () {
        //play the animation
        StartCoroutine("FadeOutAnim");
        lvlMng = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }
	
	// Update is called once per frame
	void Update () {
        
        //when the animation is finished 
        if (gameObject.GetComponent<SpriteRenderer>().color.a < -0.00000005f && switched == false)
        {
            lvlMng.creditFade = false;
            lvlMng.LevelUp(0);
            switched = true;
        }

    }

    //fade Animation
    IEnumerator FadeOutAnim()
    {
        for (float f = 1f; f >= -0.1f; f -= 0.05f)
        {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = f;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FadeInAnim()
    {
        for (float f = -0.1f; f <= 1f; f -= 0.05f)
        {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = f;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

}
