using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //싱글톤
    private static ObstacleManager instance = null;
    public static ObstacleManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //최대로 생성할 수 있는 장애물 수
    public int maxObstacleNum;
    //장애물 위치
    public int[,] obstacleLocation = new int[8, 4]; 
    //얼룩 생성 확률
    
    public float[] probs = new float[3]{50.0f, 30.0f, 20.0f}; //장애물 확률
    //전체 장애물을 담을 오브젝트
    [SerializeField] GameObject obstacleObject; 
    //현재 생성된 장애물
    public GameObject obstacle;
    private ObstacleSpawner theOS;
    void Start()
    {
        theOS = FindObjectOfType<ObstacleSpawner>();
        maxObstacleNum = 3;
    }

    public void CreateObstacle()
    {
        //장애물 위치 배열 초기화
        //obstacleLocation.GetLength(0) : 행의 길이
        //obstacleLocation.GetLength(1) : 열의 길이
        for (int i = 0; i < obstacleLocation.GetLength(0); i++)
        {
            for (int j = 0; j < obstacleLocation.GetLength(1); j++)
            {
                obstacleLocation[i,j] = 0;
            }
        }
        obstacle = Instantiate(obstacleObject);
        theOS.SpawnObstacle();
    }
}
