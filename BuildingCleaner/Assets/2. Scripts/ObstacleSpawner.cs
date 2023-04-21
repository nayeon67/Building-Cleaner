using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float[] probs = new float[3]{50.0f, 30.0f, 20.0f}; //장애물 확률
    private float totalProbs = 100.0f; //확률 합
    [SerializeField] private GameObject brokenGlass; //깨진 유리창
    [SerializeField] private GameObject[] stains; //얼룩들
    private int[] obstacleLocations = new int[4];//장애물 위치들
    private BackgroundScroller theBS; 
    void Start()
    {
        theBS = FindObjectOfType<BackgroundScroller>();

        //깨진 유리창 생성
        for(int i = 0; i < 8; i++)
        {
            int num = Random.Range(0, 2);

            for(int j = 0; j < num; j++)
            {
                int x = Random.Range(0, 4);
                GameObject obstacle = Instantiate(brokenGlass, new Vector2(x, i), Quaternion.identity);
                obstacle.transform.parent = theBS.backgrounds[i].transform;
            }
        }
    }

    private int ChooseNumber() //확률에 따른 얼룩 선택
    {
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

    public void SpawnObstacle()
    {
        int choice = Random.Range(0, 2);
        if(choice == 0)
        {
            int num = Random.Range(0, 2);
        
            //깨진 유리창 생성
            for(int i = 0; i < num; i++)
            {
                int x = Random.Range(0, 4);
                GameObject obstacle =  Instantiate(brokenGlass, new Vector2(x, 7), Quaternion.identity);
                obstacle.transform.parent = theBS.backgrounds[7].transform;

                obstacleLocations[x] = 1;
            }

        }
        
        choice = Random.Range(0,2);
        if(choice == 0)
        {
            int num = Random.Range(0, 2);

            //얼룩 생성
            for(int i = 0; i < num; i++)
            {
                //랜덤 위치 설정
                int x = Random.Range(0, 4);
                while(obstacleLocations[x] == 1)
                {
                x = Random.Range(0, 4); //해당 위치에 이미 다른 것이 있는지 확인
                }
                
                GameObject stain;

                //얼룩 단계 설정
                int randomPoint = ChooseNumber();
                stain = Instantiate(stains[randomPoint], new Vector3(x, 7, 0), Quaternion.identity); 
                //얼룩 부모를 제일 위 유리창으로 설정
                stain.transform.parent = theBS.backgrounds[7].transform;
                obstacleLocations[x] = 1;
            }
        }

        for(int i = 0; i < obstacleLocations.Length; i++) { obstacleLocations[i] = 0; }
    }
        
}
