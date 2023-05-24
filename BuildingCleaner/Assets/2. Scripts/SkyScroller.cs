using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroller : MonoBehaviour
{
    //전체 하늘
    [SerializeField] private GameObject sky;
    [SerializeField] private List<GameObject> skys;
    private int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkyDown()
    {
        sky.transform.Translate(Vector2.down);
        count++;

        if(count % 8 == 0)
        {
            skys[0].SetActive(false);
        }
    }
}
