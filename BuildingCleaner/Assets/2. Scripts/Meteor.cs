using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float fallSpeed = 4f;  // 쓰레기가 떨어지는 속도
    
    void Update()
    {
        if(!GameManager.Instance.isGameTime) { return; }
        // 떨어지는 스크립트
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // 특정위치 도달시 이 게임오브젝트 삭제
        if(transform.position.y <= -1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Down()
    {
        transform.Translate(Vector3.down);
    }
}
