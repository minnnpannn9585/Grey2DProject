using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image healthBar;
    public float speed;
    public Transform player;
    public int hp;
    public Animator animator;

    void Update()
    {
        healthBar.fillAmount = hp / 100f;
        Vector3 direction = player.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) <= 1f)
        {
            Attack();
        }

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Attack()
    {
        animator.SetBool("isattacking", true);
        //animation
        //用碰撞检测伤害
    }
}
