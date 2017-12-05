using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseInputController : InputController
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public override float GetForwardAxis()
    {
        return Input.GetAxis("Vertical");
    }

    public override float GetRightAxis()
    {
        return Input.GetAxis("Horizontal");
    }

    public override bool IsLookEnabled()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    public override float GetLookUpAxis()
    {
        return -Input.GetAxis("Mouse Y");
    }

    public override float GetLookRightAxis()
    {
        return Input.GetAxis("Mouse X");
    }

    public override bool IsGrabPressed()
    {
        return Input.GetButtonDown("Grab");
    }

    public override bool IsGrabReleased()
    {
        return Input.GetButtonUp("Grab");
    }

    public override bool IsJumpPressed()
    {
        return Input.GetButtonDown("Jump");
    }
}
