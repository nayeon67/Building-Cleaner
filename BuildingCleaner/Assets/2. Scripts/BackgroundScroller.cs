using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
   public List<GameObject> backgrounds;
   [SerializeField] private GameObject background;
   private Vector2 topPosition;
   private ObstacleSpawner theOS;
    void Start()
    {
        theOS = FindObjectOfType<ObstacleSpawner>();
        topPosition = backgrounds[backgrounds.Count - 1].transform.position;
    }

    public void BackgorundScroll()
    {
        background.transform.position += Vector3.down;//배경 한 칸 내리기
        backgrounds[0].transform.position = topPosition;

        Transform[] transforms = backgrounds[0].GetComponentsInChildren<Transform>();
        for(int i = 5; i < transforms.Length; i++)
        {
            Destroy(transforms[i].gameObject); //자식 오브젝트 삭제
        } 

        GameObject back = backgrounds[0];

        backgrounds.Remove(back);
        backgrounds.Add(back);

        theOS.SpawnObstacle();
    }
}
