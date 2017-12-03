using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PackageShape
{
    Box,
    Pyramid,
    Cylinder,
    Flat,
    Sphere,
    Rectangle
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

public class PackageController : MonoBehaviour
{
    [SerializeField] PackageShape packageShape;
    [SerializeField] PackageColor packageColor;
    [SerializeField] PackageLayout packageLayout;

    [Header("Game")]
    public bool canValidateWinCondition = true;
    public float destructionTime = 1.0f;
    public AnimationCurve destructionScaleEasing;

    [Header("Library")]
    public Mesh boxMesh;
    public Mesh pyramidMesh;
    public Mesh cylinderMesh;
    public Mesh flatMesh;
    public Mesh sphereMesh;
    public Mesh rectangleMesh;

    public Texture2D boxTexture;
    public Texture2D pyramidTexture;
    public Texture2D cylinderTexture;
    public Texture2D flatTexture;
    public Texture2D sphereTexture;
    public Texture2D rectangleTexture;

    public Texture2D boxColorTexture;
    public Texture2D pyramidColorTexture;
    public Texture2D cylinderColorTexture;
    public Texture2D flatColorTexture;
    public Texture2D sphereColorTexture;
    public Texture2D rectangleColorTexture;

    public Color redColor;
    public Color blueColor;
    public Color greenColor;

    public Texture2D boxLayout1Texture;
    public Texture2D boxLayout2Texture;
    public Texture2D boxLayout3Texture;
    public Texture2D pyramidLayout1Texture;
    public Texture2D pyramidLayout2Texture;
    public Texture2D pyramidLayout3Texture;
    public Texture2D cylinderLayout1Texture;
    public Texture2D cylinderLayout2Texture;
    public Texture2D cylinderLayout3Texture;
    public Texture2D flatLayout1Texture;
    public Texture2D flatLayout2Texture;
    public Texture2D flatLayout3Texture;
    public Texture2D sphereLayout1Texture;
    public Texture2D sphereLayout2Texture;
    public Texture2D sphereLayout3Texture;
    public Texture2D rectangleLayout1Texture;
    public Texture2D rectangleLayout2Texture;
    public Texture2D rectangleLayout3Texture;

    [Header("InternalObjects")]
    public cakeslice.Outline outline;
    public MeshRenderer cardboardObject;
    public MeshRenderer colorObject;
    public MeshRenderer layoutObject;

    public delegate void PackageAction(PackageController _package);
    public event PackageAction Destroyed;

    // Use this for initialization
    void Start ()
    {
        outline.enabled = false;
        SetShape(packageShape);
        SetColor(packageColor);
        SetLayout(packageLayout);
    }

    // Update is called once per frame
    void Update ()
    {
		if (m_hasFinishedDestroying)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Destroyed != null) Destroyed(this);
    }

    public void PrettyDestroy()
    {
        if (m_isDestroying)
            return;

        m_isDestroying = true;
        StartCoroutine(PrettyDestroySequence());
    }

    IEnumerator PrettyDestroySequence()
    {
        float destroyTimer = 0.0f;
        Vector3 startScale = transform.localScale;
        while (destroyTimer < destructionTime)
        {
            destroyTimer += Time.deltaTime;

            float easedT = destructionScaleEasing.Evaluate(Mathf.Clamp01(destroyTimer / destructionTime));
            transform.localScale = startScale * easedT;
            yield return new WaitForEndOfFrame();
        }
        m_hasFinishedDestroying = true;
    }

    public PackageShape GetShape()
    {
        return packageShape;
    }

    public void SetShape(PackageShape _shape)
    {
        packageShape = _shape;

        Mesh m = null;
        Texture2D t = null;
        Texture2D colorT = null;

        switch (packageShape)
        {
            case PackageShape.Box:
                {
                    m = boxMesh;
                    t = boxTexture;
                    colorT = boxColorTexture;
                }
                break;

            case PackageShape.Pyramid:
                {
                    m = pyramidMesh;
                    t = pyramidTexture;
                    colorT = pyramidColorTexture;

                }
                break;

            case PackageShape.Flat:
                {
                    m = flatMesh;
                    t = flatTexture;
                    colorT = flatColorTexture;
                }
                break;

            case PackageShape.Cylinder:
                {
                    m = cylinderMesh;
                    t = cylinderTexture;
                    colorT = cylinderColorTexture;
                }
                break;

            case PackageShape.Rectangle:
                {
                    m = rectangleMesh;
                    t = rectangleTexture;
                    colorT = rectangleColorTexture;
                }
                break;

            case PackageShape.Sphere:
                {
                    m = sphereMesh;
                    t = sphereTexture;
                    colorT = sphereColorTexture;
                }
                break;

            default:
                break;
        }

        GetComponent<MeshCollider>().sharedMesh = m;
        cardboardObject.GetComponent<MeshFilter>().sharedMesh = m;
        cardboardObject.GetComponent<MeshRenderer>().material.mainTexture = t;
        colorObject.GetComponent<MeshFilter>().sharedMesh = m;
        colorObject.GetComponent<MeshRenderer>().material.mainTexture = colorT;
        layoutObject.GetComponent<MeshFilter>().sharedMesh = m;
    }

    public PackageColor GetColor()
    {
        return packageColor;
    }

    public void SetColor(PackageColor _color)
    {
        packageColor = _color;
        Color c = new Color();
        switch(packageColor)
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

    public PackageLayout GetLayout()
    {
        return packageLayout;
    }
   
    public void SetLayout(PackageLayout _layout)
    {
        packageLayout = _layout;
        Texture2D t = null;
        switch(GetShape())
        {
            case PackageShape.Box:
                {
                    switch(packageLayout)
                    {
                        case PackageLayout.Layout1: t = boxLayout1Texture; break;
                        case PackageLayout.Layout2: t = boxLayout2Texture; break;
                        case PackageLayout.Layout3: t = boxLayout3Texture; break;
                    }
                }
                break;

            case PackageShape.Pyramid:
                {
                    switch (packageLayout)
                    {
                        case PackageLayout.Layout1: t = pyramidLayout1Texture; break;
                        case PackageLayout.Layout2: t = pyramidLayout2Texture; break;
                        case PackageLayout.Layout3: t = pyramidLayout3Texture; break;
                    }
                }
                break;

            case PackageShape.Flat:
                {
                    switch (packageLayout)
                    {
                        case PackageLayout.Layout1: t = flatLayout1Texture; break;
                        case PackageLayout.Layout2: t = flatLayout2Texture; break;
                        case PackageLayout.Layout3: t = flatLayout3Texture; break;
                    }
                }
                break;

            case PackageShape.Cylinder:
                {
                    switch (packageLayout)
                    {
                        case PackageLayout.Layout1: t = cylinderLayout1Texture; break;
                        case PackageLayout.Layout2: t = cylinderLayout2Texture; break;
                        case PackageLayout.Layout3: t = cylinderLayout3Texture; break;
                    }
                }
                break;

            case PackageShape.Rectangle:
                {
                    switch (packageLayout)
                    {
                        case PackageLayout.Layout1: t = rectangleLayout1Texture; break;
                        case PackageLayout.Layout2: t = rectangleLayout2Texture; break;
                        case PackageLayout.Layout3: t = rectangleLayout3Texture; break;
                    }
                }
                break;

            case PackageShape.Sphere:
                {
                    switch (packageLayout)
                    {
                        case PackageLayout.Layout1: t = sphereLayout1Texture; break;
                        case PackageLayout.Layout2: t = sphereLayout2Texture; break;
                        case PackageLayout.Layout3: t = sphereLayout3Texture; break;
                    }
                }
                break;
        }

        layoutObject.material.mainTexture = t;
    }

    bool m_isDestroying = false;
    bool m_hasFinishedDestroying = false;
}
