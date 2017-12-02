using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] packageSoftImpacts;
    public AudioClip[] packageMidImpacts;
    public AudioClip[] packageHighImpact;

    void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
}
