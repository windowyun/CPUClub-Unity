using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float jumpPower = 10.0f;

    Transform trans;
    Rigidbody2D rigid2;
    SpriteRenderer render;
    Animation anim;

    void Start()
    {
        trans = GetComponent<Transform>();
        rigid2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

        /*
        if(Input.GetButton("Horizontal"))
            anim.SetBool("moving", true);

        else
            anim.SetBool("moving", false);
        */

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
            //anim.SetTrigger("Jump");

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
            
            //anim.SetBool("jumping", true);

        }

        /*
        //점프 모션 종료 : 착지 모션 시작
        if (rigid2.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
        }
        */
    }
}
