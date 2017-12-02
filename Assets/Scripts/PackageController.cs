using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        cakeslice.Outline outline = GetComponent<cakeslice.Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
