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

    private int height;
    public float CameraSpeed;
    private Text scoreText;
    private Text scoreTextShadow;
    private SkyScroller theSS;

    private void Start() {
        CameraSpeed = 0.1f;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("ScoreTextShadow").GetComponent<Text>();
        theSS = FindObjectOfType<SkyScroller>();
    }

    public void SetHeight(int value)
    {
        height +=  value;
        scoreText.text = scoreTextShadow.text = height +".M";

        if (height == 50) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 5; 
            CameraSpeed = 0.15f;
            theSS.SetSkyState(height);
        }
        if (height == 100) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 7;
            CameraSpeed = 0.3f;
            ObstacleManager.Instance.probs = new float[3]{30.0f, 40.0f, 30.0f};  
            theSS.SetSkyState(height);  
        }
        if (height == 200)
        {
            CameraSpeed = 0.5f;
            theSS.SetSkyState(height);  
        }
    }

    public void GameOver()
    {
        Debug.Log("이걸 죽네 ㅋ");
    }
}
