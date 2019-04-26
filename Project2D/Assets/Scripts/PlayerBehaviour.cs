using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D playerbody;
    Vector2 mv;
    Vector2 currentpos;
    float mvspeed = 10f;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = new Vector2(transform.position.x,transform.position.y);
        mv = new Vector2(Input.GetAxis("Horizontal"), 0) * mvspeed * Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (mv.x != 0)
        {
            Move(mv);
        }
    }

    private void Move(Vector2 mv)
    {
        playerbody.MovePosition(currentpos + mv);
    }
}
