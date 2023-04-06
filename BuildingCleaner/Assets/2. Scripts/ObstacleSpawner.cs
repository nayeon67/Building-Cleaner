using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject brokenGlass; //깨진 유리창
    [SerializeField] private GameObject[] stains; //얼룩들
    private BackgroundScroller theBS; 
    private int[] obstacleLocations = new int[4];//장애물 위치들
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
            
            int n = Random.Range(1, 11);
            
            GameObject stain;

            //얼룩 단계 설정
            if(n <= 5) { stain = Instantiate(stains[0], new Vector3(x, 7, 0), Quaternion.identity); }
            else if(n <= 8) { stain = Instantiate(stains[1], new Vector3(x, 7, 0), Quaternion.identity); }
            else { stain = Instantiate(stains[2], new Vector3(x, 7, 0), Quaternion.identity); }
            
            //얼룩 부모를 제일 위 유리창으로 설정
            stain.transform.parent = theBS.backgrounds[7].transform;
            obstacleLocations[x] = 1;
        }

        for(int i = 0; i < obstacleLocations.Length; i++) { obstacleLocations[i] = 0; }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
