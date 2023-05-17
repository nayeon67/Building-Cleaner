using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start() {
        CameraSpeed = 0.5f;
    }

    public void SetHeight(int value)
    {
        height +=  value;

        if (height == 50) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 7; 
        }
        if (height == 100) 
        { 
            ObstacleManager.Instance.maxObstacleNum = 10;
            ObstacleManager.Instance.probs = new float[3]{30.0f, 40.0f, 30.0f};    
        }
    }

    public void GameOver()
    {
        Debug.Log("이걸 죽네 ㅋ");
    }
}
