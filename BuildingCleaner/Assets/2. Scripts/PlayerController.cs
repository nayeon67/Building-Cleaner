using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private bool isMoving; //움직이는 상태인지 확인
    private float moveTime; //이동 시간
    private int playerPosX; //플레이어가 위치한 X
    private int playerPosY; //플레이어가 위치한 Y
    private BackgroundScroller theBS;

    public int hpNum = 3;   // hp 수

    void Start()
    {
        moveTime = 0.2f; 
        playerPosX = 2;
        playerPosY = 1;
        theBS = FindObjectOfType<BackgroundScroller>();
    }

    public void SetPos(string button) 
    {
        if (isMoving) { return; }
        
        if (button == "up") 
        {
            //배경 스크롤
            if(playerPosY == 7) { theBS.BackgorundScroll(); } 
            else 
            { 
                StartCoroutine(PlayerMove(Vector2.up));
                playerPosY++; 
            }
        }
        else if (button == "down" && playerPosY != 1)
        {
            StartCoroutine(PlayerMove(Vector2.down));
            playerPosY--;
        }
        else if (button == "left" && playerPosX != 1)
        {
            StartCoroutine(PlayerMove(Vector2.left)); 
            playerPosX--;
        }

        else if (button == "right" && playerPosX != 4)
        {
            StartCoroutine(PlayerMove(Vector2.right));  
            playerPosX++;
        }
    }

    private IEnumerator PlayerMove(Vector2 dir)
    {
        isMoving = true;
        //경과시간
        float elapsedTime = 0.0f; 

        Vector2 currentPosition = transform.position;
        //현재 위치에 목표 위치를 더한 값을 타겟으로 설정
        Vector2 targetPosition = currentPosition + dir; 

        while(elapsedTime < moveTime)
        {
            //보간으로 이동
            transform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime/moveTime);
             
            //경과시간 증가
            elapsedTime += Time.deltaTime; 
            yield return null;

        }

        transform.position = targetPosition;

        isMoving = false;
    }
}
