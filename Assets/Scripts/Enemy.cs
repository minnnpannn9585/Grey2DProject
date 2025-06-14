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
    public float dashSpeed = 10f;

    public GameObject groundCrack;
    public GameObject daoguang;
    bool movingToTarget = false;
    bool stopMoving = false;
    Vector2 targetPosition;
    float enemyMoveTimer = 2f;

    void Update()
    {
        healthBar.fillAmount = hp / 100f;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        enemyMoveTimer -= Time.deltaTime;
        if (enemyMoveTimer <= 0f)
        {
            stopMoving = true;
            enemyMoveTimer = 4f;
            StartCoroutine(Skill3());
            //StartCoroutine(SkillTwo());
            //StartCoroutine(SkillOne());
        }
        EnemyMove();
    }

    IEnumerator SkillOne()
    {
        animator.SetBool("skillOne", true);

        Vector2 dashTarget = new Vector2(player.position.x, player.position.y);

        if (dashTarget.x > transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1) * 0.45f;
        }
        else if (dashTarget.x < transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1) * 0.45f;
        }

        while (Vector2.Distance(transform.position, dashTarget) > 0.1f)
        {
            // Move towards the player at a constant speed
            transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
            yield return null;
        }

        

        animator.SetBool("skillOne", false);
        yield return new WaitForSeconds(0.5f); // Optional delay after dashing

        stopMoving = false;
        movingToTarget = false;
    }

    IEnumerator SkillTwo()
    {
        animator.SetTrigger("skillTwo");

        yield return new WaitForSeconds(1.25f);

        for (int i = 0; i < 2; i++)
        {
            Instantiate(groundCrack, transform.position + new Vector3(0,-1f,0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        

        stopMoving = false;
        movingToTarget = false;
    }

    IEnumerator Skill3()
    {

        animator.SetTrigger("skillThree");

        for(int i = 0; i < 5; i++)
        {
            Instantiate(daoguang, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        
        yield return new WaitForSeconds(0.5f);

        stopMoving = false;
        
        movingToTarget = false;
    }

    public void EnemyMove()
    {
        
        if (!movingToTarget && !stopMoving)
        {
            targetPosition = new Vector2(player.position.x, player.position.y);
            if(player.position.x > transform.position.x)
            {
                transform.GetChild(0).localScale = new Vector3(-1,1,1) * 0.45f;
            }
            else if (player.position.x < transform.position.x)
            {
                transform.GetChild(0).localScale = new Vector3(1, 1, 1) * 0.45f;
            }
            movingToTarget = true;
        }
        else if(movingToTarget && !stopMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

}
