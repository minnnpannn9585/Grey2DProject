using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image healthBar01;
    public Image healthBar02;
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
    public AudioClip skillThreeSFX;
    public AudioClip skillTwoSFX;
    public AudioClip skillOneSFX;
    private bool isBlinking = false; // 用于标记是否正在执行 BlinkRed 协程

    private bool stateOne = true;
    int index = 0;
    
    void Update()
    {
        if(GameManager.instance.isStarted == false)
        {
            return;
        }

        healthBar01.fillAmount = (hp - 50) / 50f;
        if (hp < 50)
        {
            if (stateOne)
            {
                GameManager.instance.ChangeBGHalftime();
                //change state, such as bg picture, music, etc
            }
            stateOne = false;
            healthBar02.fillAmount = hp / 50f;
            
        }
        
            
            
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        enemyMoveTimer -= Time.deltaTime;
        if (enemyMoveTimer <= 0f)
        {
            stopMoving = true;
            enemyMoveTimer = 4f;
            if (stateOne)
            {
                int randomNumber = Random.Range(0, 2); // Randomly choose a skill to use
                if (randomNumber == 0)
                {
                    StartCoroutine(SkillOne());
                    SFXManager.Instance.PlaySFX(skillOneSFX);
                }
                else if (randomNumber == 1) {
                    StartCoroutine(Skill3());
                    SFXManager.Instance.PlaySFX(skillThreeSFX);
                }
            }
            else
            {
                
                
                    if (index == 0)
                    {
                        StartCoroutine(SkillOne());
                        SFXManager.Instance.PlaySFX(skillOneSFX);
                        index++;
                    }
                    else if (index == 1)
                    {
                        StartCoroutine(SkillTwo());
                        SFXManager.Instance.PlaySFX(skillTwoSFX);
                        index++;
                    }
                    else if (index == 2)
                    {
                        StartCoroutine(Skill3());
                        SFXManager.Instance.PlaySFX(skillThreeSFX);
                        index = 0;
                    }
                
            }
        }
        EnemyMove();
    }

    IEnumerator SkillOne()
    {
        animator.SetBool("skillOne", true);

        Vector2 dashTarget = new Vector2(player.position.x, player.position.y);

        if (dashTarget.x > transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1) * 0.19f;
        }
        else if (dashTarget.x < transform.position.x)
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, 1) * 0.19f;
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
            Instantiate(groundCrack, transform.position + new Vector3(0, -1f, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }



        stopMoving = false;
        movingToTarget = false;
    }

    IEnumerator Skill3()
    {

        animator.SetTrigger("skillThree");

        for (int i = 0; i < 5; i++)
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
            if (player.position.x > transform.position.x)
            {
                transform.GetChild(0).localScale = new Vector3(-1, 1, 1) * 0.19f;
            }
            else if (player.position.x < transform.position.x)
            {
                transform.GetChild(0).localScale = new Vector3(1, 1, 1) * 0.19f;
            }
            movingToTarget = true;
        }
        else if (movingToTarget && !stopMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-9f,9f),
            Mathf.Clamp(transform.position.y,-5f,1f), 0f);
    }

    public void EnemyGetHit()
    {
        if (!isBlinking) // 如果没有正在执行的协程，才启动新的协程
        {
            StartCoroutine(BlinkRed());
        }
    }

    private IEnumerator BlinkRed()
    {
        isBlinking = true; // 标记协程正在运行
        // Get the SpriteRenderer component of the enemy
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Save the original color
            Color originalColor = spriteRenderer.color;

            // Change the color to red
            spriteRenderer.color = Color.red;

            // Wait for a short duration
            yield return new WaitForSeconds(0.1f);

            // Revert the color back to the original
            spriteRenderer.color = originalColor;

            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;

            // Wait for a short duration
            yield return new WaitForSeconds(0.1f);

            // Revert the color back to the original
            spriteRenderer.color = originalColor;
        }
        isBlinking = false; // 协程结束，标记为未运行
    }

}
