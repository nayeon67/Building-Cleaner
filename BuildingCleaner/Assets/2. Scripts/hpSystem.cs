using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField] GameObject[] hearts;   // 빨간 하트
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void SetHeartUI()
    {
        if(UIManager.Instance.heartCnt > 2) { return; }
        hearts[UIManager.Instance.heartCnt].SetActive(false);
        UIManager.Instance.heartCnt++;
    }
}
