using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallAutoTiler : MonoBehaviour
{
    public float tilingRatio = 1.0f;

    void Start ()
    {
        m_renderer = GetComponent<MeshRenderer>();
	}
	
	void Update ()
    {
        m_renderer.material.mainTextureScale = new Vector2(transform.localScale.x / tilingRatio, transform.localScale.z / tilingRatio);
    }

    MeshRenderer m_renderer;
}
