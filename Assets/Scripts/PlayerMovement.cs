using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float dashDistance;
    public float dashSpeed;
    void Start()
    {

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(direction * Time.deltaTime * speed);

		if(Input.GetKeyDown("k"))
        {
            Vector3 dashTarget = transform.position + direction * dashDistance;
            StartCoroutine(Dash(dashTarget));
        }
		
    }
    
    IEnumerator Dash(Vector3 dashTarget)
    {
        while (Vector3.Distance(transform.position, dashTarget) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, dashTarget, dashSpeed);
            yield return null;
        }
    }
}
