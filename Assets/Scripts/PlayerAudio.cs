using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAudio : MonoBehaviour
{
    PlayerController player;

    public float walkFootstepsRate;

    private bool walkTrigger;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (player.isWalking && !walkTrigger)
        {
            InvokeRepeating("PlayerWalkFootsteps", 0f, walkFootstepsRate);
            walkTrigger = true;
            
        }
        else if(!player.isWalking)
        {
            CancelInvoke();
            walkTrigger = false;
        }
    }

    private void PlayerWalkFootsteps()
    {
        AudioManager.Instance.Play("Player_Footsteps", this.transform.position);
    }

}
