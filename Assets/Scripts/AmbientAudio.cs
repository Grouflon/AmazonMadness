using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayAmbientSound("Room");
        AudioManager.Instance.PlayMusic("Mus_LevelStart");
    }
}
