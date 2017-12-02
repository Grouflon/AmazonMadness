using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PackageShape
{
    Box,
    /*Cylinder,
    Flat,
    Sphere,
    Pyramid,
    Rectangle*/
}

public enum PackageColor
{
    Red,
    Blue,
    Green,
}

public enum PackageLayout
{
    Layout1,
    Layout2,
    Layout3,
}

public class PackageController : MonoBehaviour {

    public cakeslice.Outline outline;

    public PackageShape shape;

    public Color redColor;
    public Color blueColor;
    public Color greenColor;

    public MeshRenderer colorObject;

	// Use this for initialization
	void Start ()
    {
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetColor(PackageColor _color)
    {
        m_color = _color;
        Color c = new Color();
        switch(m_color)
        {
            case PackageColor.Red:
                {
                    c = redColor;
                }
                break;

            case PackageColor.Blue:
                {
                    c = blueColor;
                }
                break;

            case PackageColor.Green:
                {
                    c = greenColor;
                }
                break;
        }
        colorObject.material.color = c;
    }

    PackageColor m_color;
}
