using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpSystem : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;   // 빨간 하트
    [SerializeField] int heartCnt;
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    
    void Update()
    {

    }

    public void HpSystem()
    {
        hearts[heartCnt].SetActive(false);
        heartCnt++;
    }
}
