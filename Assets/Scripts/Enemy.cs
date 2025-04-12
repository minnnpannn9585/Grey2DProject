using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform player;
    public int hp;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
