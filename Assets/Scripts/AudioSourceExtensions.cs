using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtensions
{
    /// <summary>
    /// Play a OneShot Random Sound from an array of AudioClips
    /// </summary>
    public static void PlayOneShot(this AudioSource source, AudioClip[] clips)
    {
        int _indexToPlay = Random.Range(0, clips.Length);
        AudioClip _clipToplay = clips[_indexToPlay];
        source.PlayOneShot(_clipToplay, 1f);
    }

    /// <summary>
    /// Play a OneShot Random Sound from an array of AudioClips
    /// </summary>
    public static void PlayOneShot(this AudioSource source, AudioClip[] clips, float volumeScale)
    {
        int _indexToPlay = Random.Range(0, clips.Length);
        AudioClip _clipToplay = clips[_indexToPlay];
        source.PlayOneShot(_clipToplay, volumeScale);
    }

    /// <summary>
    /// Play a OneShot Random Sound from an array of AudioClips
    /// </summary>
    public static void PlayOneShot(this AudioSource source, AudioClip[] clips, float volumeScale, float volumeRandomness)
    {
        int _indexToPlay = Random.Range(0, clips.Length);
        AudioClip _clipToplay = clips[_indexToPlay];

        float _volumeRandomValue = Random.Range(0f, volumeRandomness);

        source.PlayOneShot(_clipToplay, volumeScale * _volumeRandomValue);
    }
}