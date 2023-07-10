using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Transform player;
    private Vector3 originPos;
    private FallObstacle[] fallObstacles;
    private Meteor[] meteors;
   
    void Start() 
    {
        player = FindObjectOfType<PlayerController>().transform;
        originPos = transform.position;
    }
    void Update()
    {
        if(!GameManager.Instance.isGameTime) { return; }
        
        //카메라 위로 이동
        transform.Translate(Vector2.up * GameManager.Instance.CameraSpeed * Time.deltaTime);
        if(!CheckTargetInCamera(player))
        {
            GameManager.Instance.GameOver();
        }
    }

    public void CameraDown()
    {
        transform.Translate(Vector2.down);
        if(transform.position.y <= originPos.y)
        {
            transform.position = originPos;
        }

        fallObstacles = FindObjectsOfType<FallObstacle>();
        meteors = FindObjectsOfType<Meteor>();

        for(int i = 0; i < fallObstacles.Length; i++)
        {
            fallObstacles[i].Down();
        }

        for(int i = 0; i < meteors.Length; i++)
        {
            meteors[i].Down();
        }
    }

    public bool CheckTargetInCamera(Transform target)
    {
        //대상이 위치한 화면의 위치
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);
        //대상이 화면 밑으로 사라지면
        if( screenPoint.y < 0) 
        {
            return false;
        }

        return true;
    }
}
