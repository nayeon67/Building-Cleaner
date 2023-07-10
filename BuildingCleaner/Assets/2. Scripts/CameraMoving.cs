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
