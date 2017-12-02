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

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public override float GetForwardAxis()
    {
        float result = 0.0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z))
            result += 1.0f;

        if (Input.GetKey(KeyCode.S))
            result -= 1.0f;

        return result;
    }

    public override float GetRightAxis()
    {
        float result = 0.0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
            result -= 1.0f;

        if (Input.GetKey(KeyCode.D))
            result += 1.0f;

        return result;
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
        return Input.GetMouseButtonDown(0);
    }

    public override bool IsGrabReleased()
    {
        return Input.GetMouseButtonUp(0);
    }

    public override bool IsJumpPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
