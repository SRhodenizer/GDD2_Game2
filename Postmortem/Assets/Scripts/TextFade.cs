using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     *Script for fading the credit text out 
     * Coded by Stephen Rhodenizer 
     * GDD2 - Game 2 - Postmortem
     */

public class TextFade : MonoBehaviour {

    int switched = 0;

    // Use this for initialization
    void Start () {
        //play the animation
        StartCoroutine("FadeInAnim");
    }
	
	// Update is called once per frame
	void Update () {
        //when the animation is finished 
        if (gameObject.GetComponent<TextMesh>().color.a > .9f && switched == 0)
        {
            StartCoroutine("FadeOutAnim");
            switched = 1;
        }
    }

    //fade Animation
    IEnumerator FadeOutAnim()
    {
        for (float f = 1f; f >= -0.1f; f -= 0.05f)
        {
            Color c = gameObject.GetComponent<TextMesh>().color;
            c.a = f;
            gameObject.GetComponent<TextMesh>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator FadeInAnim()
    {
        for (float f = -0.1f; f <= 1f; f += 0.05f)
        {
            Color c = gameObject.GetComponent<TextMesh>().color;
            c.a = f;
            gameObject.GetComponent<TextMesh>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
