using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 1f;
    public float speedToForce = 1f;

    public MeshRenderer conveyorRenderer;
    private Material conveyorMaterial
    {
        get
        {
            return conveyorRenderer.material;
        }
    }
    float textureOffset;

    void OnCollisionStay(Collision col)
    {
        Rigidbody _colliderRb = col.gameObject.GetComponent<Rigidbody>();
        _colliderRb.velocity = speed * speedToForce * transform.forward;
    }

    void Update()
    {
        textureOffset -= Time.deltaTime * speed % 1;
        conveyorMaterial.mainTextureOffset = new Vector2(textureOffset, 0f);
    }
}
