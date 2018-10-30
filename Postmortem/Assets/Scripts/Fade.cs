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
    int switched = 0;

	// Use this for initialization
	void Start () {
        //play the animation
        StartCoroutine("FadeInAnim");
        lvlMng = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }
	
	// Update is called once per frame
	void Update () {

        //when the in  animation is finished 
         if (gameObject.GetComponent<SpriteRenderer>().color.a > .9f && switched == 0)
        {
            StartCoroutine("FadeOutAnim");
            switched = 1;
        }

        //when the out animation is finished 
        if (gameObject.GetComponent<SpriteRenderer>().color.a < -0.01f && switched == 1)
        {
            lvlMng.creditFade = false;
            lvlMng.LevelUp(0);
            switched = 2;
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
        for (float f = -0.1f; f <= 1f; f += 0.05f)
        {
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = f;
            gameObject.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

}
