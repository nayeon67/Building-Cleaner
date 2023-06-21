using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObstacle : MonoBehaviour
{
    [SerializeField] GameObject imgGameObject;  // 스프라이트 렌더러가 있는 게임오브젝트
    private SpriteRenderer theSR;   // 스프라이트 렌더러
    [SerializeField] Sprite[] garbageImgs;  // 쓰레기 이미지들
    

    [SerializeField] float fallSpeed = 3f;  // 쓰레기가 떨어지는 속도
    
    void Start()
    {
        theSR = imgGameObject.GetComponent<SpriteRenderer>();

        // 랜덤수(garbageNum)의 범위를 정해주고 램덤수에 있는 스프라이트로 이미지 변경
        int garbageNum = Random.Range(0, 3);
        theSR.sprite = garbageImgs[garbageNum];
    }

    
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
