using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpSystem : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;   // 빨간 하트
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    
    void Update()
    {
        if(playerController.hpNum == 3)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(true);
        }
        else if(playerController.hpNum == 2)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
        }
        else if(playerController.hpNum == 1)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
        else if(playerController.hpNum == 0)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
    }
}
