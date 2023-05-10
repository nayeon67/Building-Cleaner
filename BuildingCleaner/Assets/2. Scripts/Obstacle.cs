using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    //위로 가기 버튼
    [SerializeField] Button upButton;
    //위로 가기 버튼을 몇번 눌렀는지 확인할 카운트
    private int count;
    private PlayerController thePC;
    void Start()
    {
        //위로 가기 버튼을 찾아 Down 함수 연결
        upButton = GameObject.Find("Top").GetComponent<Button>();
        //upButton.onClick.AddListener(Down);
    }

    private void Update() 
    {
        
    }
    public void Down()
    {
        //y좌표를 한칸 내리기
        transform.Translate(Vector2.down);

        count++;

        //장애물이 화면 밖으로 사라지면 없애기
        if(count % 15 == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
