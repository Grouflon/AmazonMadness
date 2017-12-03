using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 1f;
    private float speedToForce = 5f;

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
        PlayerController player = col.collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AddForce(speed * speedToForce * transform.forward);
            return;
        }

        Rigidbody _colliderRb = col.gameObject.GetComponent<Rigidbody>();
        _colliderRb.velocity = speed * speedToForce * transform.forward;
    }

    void Update()
    {
        textureOffset -= Time.deltaTime * speed % 1;
        conveyorMaterial.mainTextureOffset = new Vector2(textureOffset, 0f);
    }
}
