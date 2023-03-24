using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMoving; //움직이는 상태인지 확인
    private float moveTime; //이동 시간
    private float myScaleX; //플레이어 X크기
    private int playerPosX;
    private int playerPosY;
    private BackgroundScroller theBS;
    void Start()
    {
        moveTime = 0.2f;
        myScaleX = transform.localScale.x;
        playerPosX = 2;
        playerPosY = 1;
        theBS = FindObjectOfType<BackgroundScroller>();
    }


    void Update()
    {
        if (!isMoving) //움직이고 있지 않을 때
        {   
            if(Input.GetKeyDown(KeyCode.W)) 
            {
                if(playerPosY == 14) { theBS.BackgorundScroll(); } //배경 스크롤
                else 
                { 
                    StartCoroutine(PlayerMove(Vector2.up));
                    playerPosY++; 
                }
            }    
            else if (Input.GetKeyDown(KeyCode.S) && playerPosY != 1)
            {
                StartCoroutine(PlayerMove(Vector2.down));
                playerPosY--;
            }   
            else if (Input.GetKeyDown(KeyCode.A) && playerPosX != 1)
            {
                StartCoroutine(PlayerMove(Vector2.left * myScaleX)); //플레이어의 x 크기만큼 이동하기 위해
                playerPosX--;
            }  
            else if (Input.GetKeyDown(KeyCode.D) && playerPosX != 4)
            {
                StartCoroutine(PlayerMove(Vector2.right * myScaleX)); //플레이어의 x 크기만큼 이동하기 위해  
                playerPosX++;
            }
                
        }
    }

    private IEnumerator PlayerMove(Vector2 dir)
    {
        isMoving = true;

        float elapsedTime = 0.0f; //경과시간

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = currentPosition + dir; //현재 위치에 목표 위치를 더한 값을 타겟으로 설정

        while(elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime/moveTime); //보간으로 이동
            elapsedTime += Time.deltaTime; //경과시간 증가
            yield return null;

        }

        transform.position = targetPosition;

        isMoving = false;
    }
}
