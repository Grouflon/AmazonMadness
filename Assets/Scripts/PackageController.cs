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
}

public class PackageController : MonoBehaviour {

    public cakeslice.Outline outline;

    public PackageShape shape;

    public Color redColor;
    public Color blueColor;
    public Color greenColor;

    public Texture2D layout1Texture;
    public Texture2D layout2Texture;

    public MeshRenderer colorObject;
    public MeshRenderer layoutObject;

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
   
    public void SetLayout(PackageLayout _layout)
    {
        m_layout = _layout;
        Texture2D t = null;
        switch(m_layout)
        {
            case PackageLayout.Layout1:
                {
                    t = layout1Texture;
                }
                break;

            case PackageLayout.Layout2:
                {
                    t = layout2Texture;
                }
                break;

            default:
                break;
        }

        layoutObject.material.mainTexture = t;
    }

    PackageColor m_color;
    PackageLayout m_layout;
}
