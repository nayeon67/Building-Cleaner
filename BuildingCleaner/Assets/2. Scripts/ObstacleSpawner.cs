using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float[] probs = new float[3]{50.0f, 30.0f, 20.0f}; //장애물 확률
    private float totalProbs = 100.0f; //확률 합
    [SerializeField] private GameObject brokenGlass; //깨진 유리창
    [SerializeField] private GameObject[] stains; //얼룩들
    //생성할 얼룩의 개수
    private int stainNum;
    //생성할 유리창의 개수
    private int brokenGlassNum;
    //생성할 얼룩의 위치들
    private Vector2[] statinLocations;
    //생성할 깨진 유리창 위치들
    private Vector2[] brokenGlassLocations;
    private BackgroundScroller theBS; 
    void Start()
    {
        theBS = FindObjectOfType<BackgroundScroller>();

        //장애물 개수 설정
        SetAmount();

       //얼룩 생성
        for(int i = 0; i < stainNum; i++)
        {
            Vector2 spawnPos = SetLocation();

            while(spawnPos.y == 8)
            {
                spawnPos = SetLocation();
            }

            //얼룩 고르기
            int num = ChooseNumber();
            //화면에 생성하기 위해
            spawnPos.y -= 8;

            GameObject stain;
            stain = Instantiate(stains[num], spawnPos, Quaternion.identity);
            //장애물의 부모를 해당 위치의 유리창으로 설정
            stain.transform.parent = theBS.backgrounds[(int)spawnPos.y].transform;

        }

        //깨진 유리창 생성
        for(int i = 0; i < brokenGlassNum; i++)
        {
            Vector2 spawnPos = SetLocation();
            while(spawnPos.y == 8)
            {
                spawnPos = SetLocation();
            }
            //화면에 생성하기 위해
            spawnPos.y -= 8;

            GameObject glass;
            glass = Instantiate(brokenGlass, spawnPos, Quaternion.identity);
            //장애물의 부모를 해당 위치의 유리창으로 설정
            glass.transform.parent = theBS.backgrounds[(int)spawnPos.y].transform;
        }
    }

    //확률에 따른 얼룩 선택
    private int ChooseNumber() 
    {
        //Random.value = 0~1 값 랜덤 반환
        float randomValue = Random.value * totalProbs;

        for(int i=0; i<probs.Length; i++)
        {
            if(randomValue < probs[i])
            {
                return i;
            }
            else 
            {
                randomValue -= probs[i];
            }
        }

        return probs.Length - 1;
    }

    //장애물 개수를 설정
    private void SetAmount()
    {
        //얼룩의 개수를 2~최대 생성할 수 있는 장애물 개수 -1로 지정(깨진 유리창을 하나라도 생성하기 위해)
        stainNum = Random.Range(2, ObstacleManager.Instance.maxObstacleNum - 1);
        //최대 생성할 수 있는 장애물 개수에서 얼룩의 개수를 뺀 수
        brokenGlassNum =  ObstacleManager.Instance.maxObstacleNum - stainNum;
    }

    //얼룩의 위치 설정
    private Vector2 SetLocation()
    { 
        //랜덤 좌표
        int x = Random.Range(0, 4);
        int y = Random.Range(0, 8);
            
        //랜덤 좌표에 이미 장애물이 있는지 확인
        while(ObstacleManager.Instance.obstacleLocation[y, x] != 0)
        {
            x = Random.Range(0, 4);
            y = Random.Range(0, 8);
        }
        //해당 좌표에 장애물이 있음을 표시
        ObstacleManager.Instance.obstacleLocation[y, x] = 1;

        //화면 위에 생성하기 위해 y에 8을 더함
        Vector2 position = new Vector2(x, y+=8);
        return position;
        
    }
    public void SpawnObstacle() // 장애물 생성
    {
        SetAmount();

        //얼룩 생성
        for(int i = 0; i < stainNum; i++)
        {
            Vector2 spawnPos = SetLocation();
            int num = ChooseNumber();

            GameObject stain;
            stain = Instantiate(stains[num], spawnPos, Quaternion.identity);
            stain.transform.parent = ObstacleManager.Instance.obstacle.transform;
        }

        //깨진 유리창 생성
        for(int i = 0; i < brokenGlassNum; i++)
        {
            Vector2 spawnPos = SetLocation();

            GameObject glass;
            glass = Instantiate(brokenGlass, spawnPos, Quaternion.identity);
            glass.transform.parent = ObstacleManager.Instance.obstacle.transform;
        }
        
    }
        
}
