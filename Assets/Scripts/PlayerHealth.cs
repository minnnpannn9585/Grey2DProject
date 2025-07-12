using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 5;

    public GameObject[] hearts;

    public void DecreaseHealth()
    {
        currentHealth--;
        hearts[currentHealth].SetActive(false);
        
        if (currentHealth == 0)
        {
            //game over
        }
    }
}
