﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpZone : MonoBehaviour {

    public string targetSceneName;
    public bool startActive = true;
    public float fadeOutTime = 2.0f;
    public GameObject screenContent;

    public TractorBeam tractorBeam;
    public Light light;

    public void SetWarpEnabled(bool _enabled)
    {
        m_isWarpEnabled = _enabled;

        if (m_isWarpEnabled)
        {
            tractorBeam.gameObject.SetActive(true);
            light.enabled = true;
            screenContent.SetActive(true);
        }
        else
        {
            tractorBeam.gameObject.SetActive(false);
            light.enabled = false;
            screenContent.SetActive(false);
        }
    }

	// Use this for initialization
	void Start ()
    {
        SetWarpEnabled(startActive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_isWarpEnabled || m_hasWarped)
            return;

        m_hasWarped = true;

        StartCoroutine(LeaveLevelSequence());
    }

    IEnumerator LeaveLevelSequence()
    {
        Blackout blackout = FindObjectOfType<Blackout>();
        if (blackout)
        {
            blackout.FadeTo(Color.black, fadeOutTime);
        }
        yield return new WaitForSeconds(fadeOutTime);

        SceneManager.LoadScene(targetSceneName);
    }

    bool m_hasWarped = false;
    bool m_isWarpEnabled = false;
}
