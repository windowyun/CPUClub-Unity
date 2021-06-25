using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public float range = 4f;
    public float speed = 3f;
    public float waitSecond = 3f;

    int nextMove = 1;
    float rememberSpeed;
    float ctime = 0f;
    Vector2 rememberPosition;
    Rigidbody2D rigid2;

    // Start is called before the first frame update
    void Start()
    {
        rememberSpeed = speed;
        rigid2 = GetComponent<Rigidbody2D>();
        rememberPosition = transform.position;

        //StartCoroutine(eagleStop());
    }

    private void Update()
    {
        ctime += Time.deltaTime;

        if(ctime > range )//+ waitSecond)
        {
            ctime = 0;
            //StartCoroutine(eagleStop());
            nextMove *= -1;
        }
        
    }

    void FixedUpdate()
    {
        rigid2.velocity = new Vector2(0f, nextMove * speed);
    }

    IEnumerator eagleStop()
    {
        speed = 0;
        yield return new WaitForSeconds(waitSecond);
        speed = rememberSpeed;
        yield return null;
    }

}

