using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D playerbody;
    Vector2 mv;
    Vector2 currentpos;
    Vector2 fireoffset;
    SpriteRenderer playerrenderer;
    float downforce;
    bool jumping;
    float lastfire;

    public Animator playeranim;
    public Transform attackreg;
    public Transform Jumpattackreg;
    public GameObject Fire;
    public float firecooldown = 5f;
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
        fireoffset = new Vector2(1, 0.02f);
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

        if (playerrenderer.flipX)
        {
            attackreg.position = transform.position + new Vector3(-.3f, 0, 0);
            Jumpattackreg.position = transform.position + new Vector3(-.6f, .2f, 0);
        }

        else
        {
            attackreg.position = transform.position + new Vector3(1.55f, 0, 0);
            Jumpattackreg.position = transform.position +  new Vector3(.8f, .2f, 0);
        }

        if (Input.GetButton("Fire2") && Time.time > lastfire)
        {
            lastfire = Time.time + firecooldown;
            if (playerrenderer.flipX)
            {
                Vector2 oposingpos = new Vector2(currentpos.x-1.0f, currentpos.y + fireoffset.y);
                Instantiate(Fire, oposingpos, new Quaternion(0, 0, 180, 0));
            }

            else
            {
                Instantiate(Fire, currentpos + fireoffset, new Quaternion(0, 0, 0, 0));
            }
            
        }
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
        else if (Input.GetButton("Fire2") && isGrounded && Time.time > lastfire)
        {
            playeranim.SetTrigger("Skill");
        }
        else if (Input.GetButton("Fire2") && !isGrounded && Time.time > lastfire)
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
