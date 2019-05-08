using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    Rigidbody2D Firebody;
    Vector2 forward;
    Vector2 currpos;

    private void Awake()
    {
        Firebody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currpos = new Vector2(transform.position.x, transform.position.y);
        forward = new Vector2(5f, 0) * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (transform.rotation.z == 0)
        {
            Firebody.MovePosition(currpos + forward);
        }
        else
        {
            Firebody.MovePosition(currpos - forward);
        }
    }
}
