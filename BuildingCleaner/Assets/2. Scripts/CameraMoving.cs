using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Transform player;
    private Vector3 originPos;
   
    void Start() 
    {
        player = FindObjectOfType<PlayerController>().transform;
        originPos = transform.position;
    }
    void Update()
    {
        //카메라 위로 이동
        transform.Translate(Vector2.up * GameManager.Instance.CameraSpeed * Time.deltaTime);
        CheckPlayerInCamera();
    }

    public void CameraDown()
    {
        transform.Translate(Vector2.down);
        if(transform.position.y <= originPos.y)
        {
            transform.position = originPos;
        }
    }

    private void CheckPlayerInCamera()
    {
        //플레이어가 위치한 화면의 위치
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(player.position);
        //플레이어가 화면 밑으로 사라지면
        if( screenPoint.y < 0) 
        {
            GameManager.Instance.GameOver();
        }
    }
}
