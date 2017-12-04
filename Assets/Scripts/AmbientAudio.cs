using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    public bool playMusic = true;

    public GameObject speaker;

    void Start()
    {
        AudioManager.Instance.PlayAmbientSound("Room");

        if (playMusic)
            AudioManager.Instance.Play("Mus_LevelStart", speaker.transform.position);
    }

    void OnDestroy()
    {
        AudioManager.Instance.StopAll();
    }
}
