using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D playerbody;
    Vector2 mv;
    Vector2 currentpos;
    SpriteRenderer playerrenderer;
    float downforce;
    bool jumping;

    public Animator playeranim;
    public float mvmultiplier = 300f;
    public bool isGrounded;
    public float mvspeed = 50f;
    public float jumpHeight = 2f;

    void Awake()
    {
        playerbody = GetComponent<Rigidbody2D>();
        playerrenderer = GetComponent<SpriteRenderer>();
        downforce = playerbody.gravityScale;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = new Vector2(transform.position.x,transform.position.y);
        mv = new Vector2(Input.GetAxisRaw("Horizontal"),0) * mvspeed * Time.fixedDeltaTime;
        
        if (mv.x > 0)
        {
            playerrenderer.flipX = false;
        }
        else if (mv.x < 0)
        {
            playerrenderer.flipX = true;
        }
        animate();
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        Move(mv, jumping);
    }

    private void animate()
    {
        if (mv.x == 0)
        {
            playeranim.SetFloat("Speed", 0.0f);
        }
        else
        {
            playeranim.SetFloat("Speed", 1.0f);
        }

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            playeranim.SetTrigger("Attack");
        }
        else if (Input.GetButtonDown("Fire1") && !isGrounded)
        {
            playeranim.SetTrigger("JumpAttack");
        }
        else if (Input.GetButtonDown("Fire2") && isGrounded)
        {
            playeranim.SetTrigger("Skill");
        }
        else if (Input.GetButtonDown("Fire2") && !isGrounded)
        {
            playeranim.SetTrigger("JumpSkill");
        }

        if (!isGrounded)
        {
            playeranim.SetBool("Grounded", false);
        }
        else
        {
            playeranim.SetBool("Grounded", true);
        }
    }

    private void Move(Vector2 mv)
    {
        playerbody.AddForce(mv*mvmultiplier);
    }

    private void Move(Vector2 mv, bool jump)
    {
        if (jump)
        {
            Vector2 jumpstr = new Vector2(0, jumpHeight);
            playerbody.AddForce(jumpstr, ForceMode2D.Impulse);
            playerbody.AddForce(mv*mvmultiplier);
        }
        else
        {
            playerbody.AddForce(mv*mvmultiplier);
        }
    }
}
