using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    public PlayerBehaviour player;

    const float grounded_radius = .2f;
    LayerMask ground;

    private void Awake()
    {
        ground = LayerMask.GetMask("Environment");
    }

    private void FixedUpdate()
    {
        player.isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, grounded_radius, ground);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != player.gameObject)
            {
                player.isGrounded = true;
            }
        }
    }
}
