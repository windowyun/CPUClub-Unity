using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float jumpPower = 10.0f;

    public int hp = 3;

    Transform trans;
    Rigidbody2D rigid2;
    SpriteRenderer render;
    Animator anim;
    CapsuleCollider2D capcoli;

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        capcoli = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        anim.SetFloat("velocityY", rigid2.velocity.y);
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid2.velocity = new Vector2(h * moveSpeed, rigid2.velocity.y);

       
        if(Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);

        else
            anim.SetBool("moving", false);
        

        FilpX();
    }

    void FilpX() //방향 전환
    {
        if (Input.GetAxis("Horizontal") > 0)
            render.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            render.flipX = true;
    }

    void Jump()
    {
        

        if (Input.GetButtonDown("Jump"))// && isGround == true)
        {
            anim.SetTrigger("Jump");

            //velocity 점프
            //rigid.velocity = Vector2.up * jumpPower;
            //최고 속도 제한
            /*
            if (Mathf.Abs(rigid.velocity.x) > 3)
            {
                rigid.velocity = new Vector2(3.0f * h, rigid.velocity.y);
            }
            */

            //AddForce 점프
            rigid2.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            
            anim.SetBool("jumping", true);

        }

        
        //점프 모션 종료 : 착지 모션 시작
        if (rigid2.velocity.y == 0)
        {
            anim.SetBool("jumping", false);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
        }
    }

    void Hurt(Vector2 targetPos)
    {
        hp--;
        anim.SetTrigger("Hurt");

        if (hp <= 0)
        {
            //Collider Disable
            capcoli.enabled = false;
            //Die Effect Jump
            rigid2.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        }

        else
        {
            
            //Reaction Force
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid2.AddForce(new Vector2(dirc, 1) * 4, ForceMode2D.Impulse);
;
        }
    }
}
