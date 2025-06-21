using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFade : MonoBehaviour
{
    public float speed;
    float alphaValue = 1;

    void Update()
    {
        alphaValue = alphaValue - Time.deltaTime * speed;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);

    }
}
