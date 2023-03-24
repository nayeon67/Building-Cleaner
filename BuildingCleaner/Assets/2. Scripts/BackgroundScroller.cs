using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
   [SerializeField] private List<GameObject> backgrounds;
   [SerializeField] private GameObject background;
   private Vector2 topPosition;
    void Start()
    {
        topPosition = backgrounds[backgrounds.Count - 1].transform.position;
    }

    public void BackgorundScroll()
    {
        background.transform.position += Vector3.down * 1.1f; //배경 한 칸 내리기
        backgrounds[0].transform.position = topPosition;

        GameObject back = backgrounds[0];

        backgrounds.Remove(back);
        backgrounds.Add(back);
    }
}
