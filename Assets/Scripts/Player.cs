using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float jumpPower = 10.0f;
    public float invincibilityTime = 3.0f;
    public int presentScene = 0;

    public int hp = 3;

    bool isHurt = false;

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


        if (Input.GetButton("Horizontal"))
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


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isHurt == false)
        {
            Hurt(collision.transform.position);
        }

        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("jumping", false);
        }

        if(collision.gameObject.tag == "EndBlock")
        {
            SceneManager.LoadScene(presentScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(1+presentScene);
        }
    }

    void Hurt(Vector2 targetPos)
    {
        hp--;
        isHurt = true;
        anim.SetTrigger("Hurt");
        gameObject.layer = 10;

        if (hp <= 0)
        {
            //Collider Disable
            capcoli.enabled = false;
            //Die Effect Jump
            rigid2.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

            SceneManager.LoadScene(presentScene);
        }

        else
        {

            //Reaction Force
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rigid2.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);

            StartCoroutine(HurtRunTime());
            StartCoroutine(alphablink());

        }
    }

    IEnumerator HurtRunTime()
    {
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = 9;
        isHurt = false;
    }

    IEnumerator alphablink()
    {
        while(isHurt)
        {
            yield return new WaitForSeconds(0.2f);
            render.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
            render.color = new Color(1, 1, 1, 1f);
        }
    }
}

    
