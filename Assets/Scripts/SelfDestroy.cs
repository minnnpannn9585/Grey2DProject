using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyTime = 0.58f;

    void Update()
    {
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
