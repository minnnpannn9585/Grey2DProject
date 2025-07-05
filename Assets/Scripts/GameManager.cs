using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public bool isStarted = false;
    public GameObject[] dialogues;
    int index = 0;
    public Animator enemyAnim;

    void Start()
    {
        dialogues[index].SetActive(true);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isStarted)
        {
            

            if (index < 4)
            {
                dialogues[index].SetActive(false);
                dialogues[index+1].SetActive(true);

                index++;
                //print(index);
            }
            else
            {
                //print(index);
                isStarted = true;
                dialogues[index].SetActive(false);
                enemyAnim.SetTrigger("start");
            }
        }
    }
}
