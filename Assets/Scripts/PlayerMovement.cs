using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    public float dashSpeed;
    public float dashCD;
    public Animator anim;
    bool isDashing = false;

    void Update()
    {
        dashCD -=Time.deltaTime;

        if (!isDashing)
        {
            Move();
        }
		if(Input.GetKeyDown("k") && dashCD<0)
        {

            anim.SetTrigger("dash");
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
            Vector3 dashTarget = transform.position + direction * dashDistance;
            StartCoroutine(Dash(dashTarget));
            dashCD = 2f;
        }
    }
    
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal > 0)
        {
            transform.GetChild(0).localScale = Vector3.one * 0.4f;
        }
        else if (horizontal < 0)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1) * 0.4f;
        }
        if (horizontal == 0 && vertical == 0)
        {
            anim.SetBool("isMoving", false);
        }
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("isMoving", true);
        }
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(direction * Time.deltaTime * speed);
    }

    IEnumerator Dash(Vector3 dashTarget)
    {
        while (Vector3.Distance(transform.position, dashTarget) > 0.1f)
        {
            isDashing = true;
            //transform.position = Vector3.Lerp(transform.position, dashTarget, dashSpeed);
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed);
            yield return null;
        }
        isDashing = false;
    }
}
