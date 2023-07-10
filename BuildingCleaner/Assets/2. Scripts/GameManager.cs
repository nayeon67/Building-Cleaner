using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
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
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    public bool  isGameTime;

    public int height;
    private int bestHeight;
    private bool changeBestScore;
    public float CameraSpeed;
    private GarbageSpawner theGS;
    private void Start() 
    {
        isGameTime = true;
        CameraSpeed = 0.15f;
        

        if(PlayerPrefs.HasKey("BestScore")) 
        {
            bestHeight = PlayerPrefs.GetInt("BestScore"); 
        }
    }

    public void BackOriginState()
    {    
        ObstacleManager.Instance.maxObstacleNum = 3;
        CameraSpeed = 0.15f;
        ObstacleManager.Instance.probs = new float[3]{50.0f, 30.0f, 20.0f};  
        height = 0;
        isGameTime = true;
    }

    public void SetHeight(int value)
    {
        height +=  value;
        UIManager.Instance.SetHeightText(height);

        if (height == 50) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 5; 
            CameraSpeed = 0.5f;
            theGS = FindObjectOfType<GarbageSpawner>();
            StartCoroutine(theGS.SpawnMeteor());
            
        }
        if (height == 100) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 7;
            CameraSpeed = 1f;
            ObstacleManager.Instance.probs = new float[3]{30.0f, 40.0f, 30.0f};  
        }
        if (height == 200)
        {
            CameraSpeed = 1.5f;
        }
    }

    public void GameOver()
    {
        isGameTime = false;
        UIManager.Instance.SetScreenUI("GameOver");

        PlayerPrefs.SetInt("BestScore", bestHeight);

        //베스트 스코어 갱신
        if(height > bestHeight)
        {
            bestHeight = height;
            changeBestScore = true;
        }

        else { changeBestScore = false; }

        //게임 오버 화면에 스코어 띄우기
        UIManager.Instance.SetGameOverUI(height, bestHeight, changeBestScore);
        SoundManager.Instance.PlayBGMSound("GameOver");
    }
}
