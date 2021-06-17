using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ememy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove = -1;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    public float layDirection = 0.3f;
    public float layDistance = 0.5f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        //������ Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * layDirection, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down * layDistance, new Color(0, 1, 0)); // DrawRay : ������ �󿡼��� Ray�� �׷��ִ� �Լ�
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, layDistance, LayerMask.GetMask("Ground")); // RaycastHit : Ray�� ���� ������Ʈ

        if (rayHit.collider == null)
        {
            Turn();
        }

    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
    }

}