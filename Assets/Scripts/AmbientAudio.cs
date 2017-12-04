using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    public GameObject speaker;

    void Start()
    {
        AudioManager.Instance.PlayAmbientSound("Room");
        AudioManager.Instance.Play("Mus_LevelStart", speaker.transform.position);
    }
}
