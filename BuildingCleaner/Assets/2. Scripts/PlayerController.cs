using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private bool isMoving; //움직이는 상태인지 확인
    private float moveTime; //이동 시간
    private int playerPosX; //플레이어가 위치한 X
    private int playerPosY; //플레이어가 위치한 Y
    private int playerHp = 3; //플레이어 목숨
    private BackgroundScroller theBS;
    //플레이어 애니메이터
    private Animator playerAnim;
    //닦아야 하는 횟수
    private int num;
    //얼룩과 부딪혔는지
    private bool isContact;
    //몇번 닦았는지
    private int count;
    //부딪힌 얼룩
    private GameObject stain;
    private CameraMoving theCM;
    private HpSystem theHS;
    private SkyScroller theSS;
    void Start()
    {
        moveTime = 0.2f; 
        playerPosX = 2;
        playerPosY = 1;
        theBS = FindObjectOfType<BackgroundScroller>();
        playerAnim = GetComponent<Animator>();
        theCM = Camera.main.GetComponent<CameraMoving>();
        theHS = FindObjectOfType<HpSystem>();
        theSS = FindObjectOfType<SkyScroller>();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.W)) { SetPos("up"); }
        else if(Input.GetKeyDown(KeyCode.A)) { SetPos("left"); }
        else if(Input.GetKeyDown(KeyCode.S)) { SetPos("down"); }
        else if(Input.GetKeyDown(KeyCode.D)) { SetPos("right"); }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "stain1")
        {
            num = 1;
            isContact = true;
            stain = other.gameObject;
        }
        else if (other.tag == "stain2")
        {
            num = 2;
            isContact = true;
            stain = other.gameObject;
        }

        else if (other.tag == "stain3")
        {
            num = 3;
            isContact = true;
            stain = other.gameObject;
        }

        if (other.tag == "Garbage" || other.tag == "BrokenGlass")
        {
            GetDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "stain1" || other.tag == "stain2" || other.tag == "stain3")
        {
            isContact = false;
            num = 0;
        }
    }

    public void GetDamage(string reason = "defalut")
    {
        playerHp--;
        theHS.SetHeartUI();
        SoundManager.Instance.PlaySFXSound("GetDamage");

        if(playerHp <= 0)
        {
            playerAnim.SetTrigger("Die");
            transform.DOMoveY(-3.0f, 3f);

            if(transform.position.y <= -3.0f) 
            {
                GameManager.Instance.GameOver();
            }
        }

        else 
        { 
            //얼룩이 화면 밖으로 나갔을 때도 재생을 하면 부자연스러우니까 재생하지 않기 위해서
            if(reason != "stain")
            {
                playerAnim.SetTrigger("getDamage");
            }
        }

        
    }

    public void Cleanning()
    {
        playerAnim.SetTrigger("Clean");
        SoundManager.Instance.PlaySFXSound("Swing");

        if(isContact)
        {
            count++;

            Color color  =  stain.GetComponent<SpriteRenderer>().color;
            color.a -= (float)1/num;
            stain.GetComponent<SpriteRenderer>().color = color;

            if(count == num)
            {
                Destroy(stain);
                count = 0;
            }
        }
        
    }

    public void SetPos(string button) 
    {
        if(playerHp <= 0) { return; }
        if (isMoving) { return; }
        
        if (button == "up") 
        {
            //배경 스크롤
            if(playerPosY == 6) 
            { 
                theBS.BackgorundScroll(); 
                Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

                for(int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i].Down();
                }
                //하늘 한 칸 내리기
                theSS.SkyDown();
                //카메라 한 칸 내리기
                theCM.CameraDown();
            } 
            else 
            { 
                StartCoroutine(PlayerMove(Vector2.up));
                playerPosY++; 
            }

            GameManager.Instance.SetHeight(1);
            theSS.SetSkyState(GameManager.Instance.height);
        }
        else if (button == "down" && playerPosY != 1)
        {
            StartCoroutine(PlayerMove(Vector2.down));
            playerPosY--;
            GameManager.Instance.SetHeight(-1);
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
