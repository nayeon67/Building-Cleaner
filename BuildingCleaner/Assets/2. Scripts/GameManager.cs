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

    public int height;
    public float CameraSpeed = 1.0f;

    private void Start() {
        CameraSpeed = 1.0f;
    }

    public void GameOver()
    {
        Debug.Log("이걸 죽네 ㅋ");
    }
}
