using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;   // 빨간 하트
    private int heartCnt;
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        heartCnt = 0;
    }
    public void SetHeartUI()
    {
        if(heartCnt > 2) { return; }
        hearts[heartCnt].SetActive(false);
        heartCnt++;
    }
}
