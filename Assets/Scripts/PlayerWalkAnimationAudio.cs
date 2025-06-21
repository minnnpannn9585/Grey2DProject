using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnimationAudio : MonoBehaviour
{
    public void StartMoving()
    {
        transform.parent.GetComponent<AudioSource>().Play();
    }
}
