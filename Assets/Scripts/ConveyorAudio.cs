using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Conveyor))]
public class ConveyorAudio : MonoBehaviour
{
    private Conveyor conveyor;

    public AudioSource sourceLow;
    public AudioSource sourceHigh;

    public AnimationCurve volumeLowCurve;
    public AnimationCurve volumeHighCurve;
    public AnimationCurve pitchCurve;

    void Start()
    {
        conveyor = GetComponent<Conveyor>();
    }

    void Update()
    {
        sourceLow.volume = volumeLowCurve.Evaluate(conveyor.speed);
        sourceLow.pitch = pitchCurve.Evaluate(conveyor.speed);
        sourceHigh.volume = volumeHighCurve.Evaluate(conveyor.speed);
        sourceHigh.pitch = pitchCurve.Evaluate(conveyor.speed);
    }
}
