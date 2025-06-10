using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaoGuang : MonoBehaviour
{
    Transform pl;
    Vector3 initialPlayerPosition;
    Vector3 initialPosition;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        pl = GameObject.Find("Player").transform;
        initialPlayerPosition = pl.position;
        transform.right = pl.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = initialPlayerPosition - initialPosition;
        transform.position += new Vector3(dir.normalized.x * speed * Time.deltaTime, dir.normalized.y * speed * Time.deltaTime,0);
    }
}
