using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
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

    public void SetHeartUI()
    {
        hearts[heartCnt].SetActive(false);
        heartCnt++;
    }
}
