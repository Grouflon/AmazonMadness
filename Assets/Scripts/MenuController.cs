using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Blackout blackout = FindObjectOfType<Blackout>();
        if (blackout)
        {
            blackout.FadeTo(Color.black, 0.0f);

            Color c = Color.black;
            c.a = 0.0f;
            blackout.FadeTo(c, 1.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
