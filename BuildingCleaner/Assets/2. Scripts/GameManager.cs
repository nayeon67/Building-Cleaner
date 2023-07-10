using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public float timeLimit;
    private GarbageSpawner theGS;
    private Animator playerAnim;
    private void Start() 
    {
        isGameTime = true;
        
        if(PlayerPrefs.HasKey("BestScore")) 
        {
            bestHeight = PlayerPrefs.GetInt("BestScore"); 
        }
        timeLimit = UIManager.Instance.curTimeLimit = 30.0f;
    }

    public void BackOriginState()
    {    
        ObstacleManager.Instance.maxObstacleNum = 3;
        timeLimit = UIManager.Instance.curTimeLimit = 30.0f;
        StartCoroutine(UIManager.Instance.SetTimeLimit());
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
            timeLimit = UIManager.Instance.curTimeLimit = 15.0f;
            theGS = FindObjectOfType<GarbageSpawner>();
            StartCoroutine(theGS.SpawnMeteor());
            
        }
        if (height == 100) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 7;
            timeLimit = UIManager.Instance.curTimeLimit = 10.0f;
            ObstacleManager.Instance.probs = new float[3]{30.0f, 40.0f, 30.0f};  
        }
        if (height == 200)
        {
            timeLimit = UIManager.Instance.curTimeLimit = 5.0f;
        }
    }

    public void GameOver()
    {
        isGameTime = false;
        UIManager.Instance.SetScreenUI("GameOver");
        playerAnim = FindObjectOfType<Animator>();
        playerAnim.SetTrigger("Die");
        playerAnim.gameObject.transform.DOMoveY(-3.0f, 3f);

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
