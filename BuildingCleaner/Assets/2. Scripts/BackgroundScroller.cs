using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
   public List<GameObject> backgrounds;
   [SerializeField] private GameObject background;
   private Vector2 topPosition;
   private ObstacleSpawner theOS;
   private int count;
    void Start()
    {
        theOS = FindObjectOfType<ObstacleSpawner>();
        topPosition = backgrounds[backgrounds.Count - 1].transform.position;
        count = 7;
    }

    public void BackgorundScroll()
    {
        count++;
        if(count % 8 == 0)
        {
            ObstacleManager.Instance.CreateObstacle();
        }
        //배경 한 칸 내리기
        background.transform.position += Vector3.down;
        //유리창 한줄 이동
        backgrounds[0].transform.position = topPosition;

        //자식 오브젝트의 transform 가져오기
        Transform[] transforms = backgrounds[0].GetComponentsInChildren<Transform>();
        for(int i = 5; i < transforms.Length; i++)
        {
            //자식 오브젝트(얼룩, 장애물) 삭제
            Destroy(transforms[i].gameObject); 
        } 
        GameObject back = backgrounds[0];

        //오브젝트 풀링
        backgrounds.Remove(back);
        backgrounds.Add(back);
    }
}
