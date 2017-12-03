using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour {

    public float speed = 1f;

    private bool isOn = false;
    private float counter = 0f;
    private new Light light;
    private AudioSource source;

    void Start()
    {
        light = GetComponent<Light>();
        source = GetComponent<AudioSource>();
    }

	public void On ()
    {
        isOn = true;
        source.Play();
	}

    public void Off()
    {
        isOn = false;
        source.Stop();
    }

    void Update()
    {
        

        if (isOn)
        {
            counter += (Time.deltaTime * speed) % (2 * Mathf.PI);
            light.intensity = ((Mathf.Sin(counter) + 1f) * 10f);
        }
        else
        {
            counter = 0f;
            light.intensity = 0f;
        }

    }

}
