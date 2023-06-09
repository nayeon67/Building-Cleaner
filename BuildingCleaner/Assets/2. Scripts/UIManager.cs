using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
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


    
    [SerializeField] private GameObject[] screenUIs;
    private Dictionary<string, GameObject> uiDic = new Dictionary<string, GameObject>();
    [SerializeField] private GameObject[] heartUIs;

    private Text scoreText;
    private Text scoreTextShadow;
    private Text scoreResultText;
    private Text scoreResultShadow;
    private Text bestScoreText;
    private Text bestScoreShadow;
    private GameObject menuUI;
    private GameObject newImage;
    private Image redImage;
    private Slider timeLimitSlider;
    public float curTimeLimit;
    public int heartCnt;

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

    private void Start() 
    {
        
        foreach(GameObject ui in screenUIs)
        {
            uiDic.Add(ui.name, ui);
        }

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreTextShadow = GameObject.Find("ScoreTextShadow").GetComponent<Text>();

        scoreResultText = GameObject.Find("ScoreResultText").GetComponent<Text>();
        scoreResultShadow = GameObject.Find("ScoreResultShadow").GetComponent<Text>();

        bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
        bestScoreShadow = GameObject.Find("BestScoreShadow").GetComponent<Text>();

        newImage = GameObject.Find("NewImage");
        newImage.SetActive(false);
        menuUI = GameObject.Find("Option");
        menuUI.SetActive(false);

        timeLimitSlider = GameObject.Find("TimeLimit").GetComponent<Slider>();
        redImage = GameObject.Find("RedImage").GetComponent<Image>();
        

        for(int i = 0; i <screenUIs.Length; i++)
        {
            screenUIs[i].SetActive(false);
        }

        SetScreenUI("MainScene");
    }

    //씬에 맞는 UI 띄우기
    public void SetScreenUI(string name)
    {
        for(int i = 0; i <screenUIs.Length; i++)
        {
            screenUIs[i].SetActive(false);
        }

        if(uiDic.ContainsKey(name))
        {
            uiDic[name].SetActive(true);
        }

    }

    public void SetHeightText(int height)
    {
        scoreText.text = scoreTextShadow.text = height +".M";
    }

    public void SetGameOverUI(int height, int bestHeight, bool changeBestScore)
    {
        if(changeBestScore)
        {
            newImage.SetActive(true);
        }
        else
        {
            newImage.SetActive(false);
        }
        scoreResultText.text = scoreResultShadow.text =  height + ".M";
        bestScoreText.text = bestScoreShadow.text = bestHeight + ".M";
    }

    private void SetGameSceneUI()
    {
        for(int i = 0; i < heartUIs.Length; i++)
        {
            heartUIs[i].SetActive(true);
        }

        heartCnt = 0;
    }

    //버튼 클릭하여 씬 이동
    public void MoveScene(string scene) 
    {
        SoundManager.Instance.PlaySFXSound("ButtonDown");
        menuUI.SetActive(false);
        
        SetScreenUI(scene);
        SceneManager.LoadScene(scene);
        SoundManager.Instance.PlayBGMSound(scene);
        
        if(scene == "GameScene")
        {
            GameManager.Instance.BackOriginState();
            StartCoroutine(SetTimeLimit());
            SetHeightText(0);
            SetGameSceneUI();
        }
    }

    public void ClickMenu(string type = "defalut")
    {
        SoundManager.Instance.PlaySFXSound("ButtonDown");
        menuUI.SetActive(true);
        GameManager.Instance.isGameTime = false;
        StopAllCoroutines();

        if(type == "continue")
        {
            menuUI.SetActive(false);
            newImage.SetActive(false);
            GameManager.Instance.isGameTime = true;
            GarbageSpawner theGS = FindObjectOfType<GarbageSpawner>();
            theGS.StartSpawn();
            StartCoroutine(SetTimeLimit());
        }

        if(type == "exit")
        {
            MoveScene("MainScene");
        }
    }

    public IEnumerator SetTimeLimit()
    {
        Color color;
        while(GameManager.Instance.isGameTime)
        {
            timeLimitSlider.value = curTimeLimit/GameManager.Instance.timeLimit;

            color = redImage.color;
            color.a = 1 - (curTimeLimit/GameManager.Instance.timeLimit);
            redImage.color = color;

            yield return new WaitForSeconds(1f);
            curTimeLimit--;

            if(curTimeLimit <= 0)
            {
                GameManager.Instance.GameOver();
                StopAllCoroutines();
            }
        }
    }
}
