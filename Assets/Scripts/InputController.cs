using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour {

    public abstract float GetForwardAxis();
    public abstract float GetRightAxis();
    public abstract bool IsLookEnabled();
    public abstract float GetLookUpAxis();
    public abstract float GetLookRightAxis();

    public abstract bool IsGrabPressed();
    public abstract bool IsGrabReleased();
}
