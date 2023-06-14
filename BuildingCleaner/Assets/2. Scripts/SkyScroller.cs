using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroller : MonoBehaviour
{
    //전체 하늘
    [SerializeField] private GameObject sky;
    [SerializeField] private List<GameObject> trees = new List<GameObject>();
    [SerializeField] private List<GameObject> blueSkys = new List<GameObject>();
    [SerializeField] private List<GameObject> spaceSkys = new List<GameObject>();
    [SerializeField] private List<GameObject> starSkys = new List<GameObject>();
    [SerializeField] private GameObject gradationImage;
    private int count;
    private bool gradation;
    private Vector2 topPos;
    private enum SkyState
    {
        tree,
        blueSky,
        space,
        star
    };

    private SkyState skyState;
    void Start()
    {
        skyState = SkyState.tree;
        topPos = trees[trees.Count-1].transform.position;
    }

    public void SetSkyState(int height)
    {
        switch(height)
        {
            case 50: skyState = SkyState.blueSky; break;
            case 100: skyState = SkyState.space; break;
            case 200: skyState = SkyState.star; break;
        }
    }

    public void SkyDown()
    {
        sky.transform.Translate(Vector2.down);
        count++;

        if(count % 8 == 0)
        {
            SpawnSky();
        }
    }

    private void SpawnSky()
    {
        if(skyState == SkyState.tree)
        {
            trees[0].transform.position = topPos;
            trees.Add(trees[0]);
            trees.RemoveAt(0);
        }

        else if (skyState == SkyState.blueSky)
        {
            blueSkys[0].transform.position = topPos;
            blueSkys[0].SetActive(true);
            blueSkys.Add(blueSkys[0]);
            blueSkys.RemoveAt(0);
        }

        else if (skyState == SkyState.space)
        {
            if(gradation) 
            { 
                gradationImage.SetActive(true); 
                gradation = false;
                gradationImage.SetActive(false); 
            }

            spaceSkys[0].transform.position = topPos;
            spaceSkys[0].SetActive(true);
            spaceSkys.Add(spaceSkys[0]);
            spaceSkys.RemoveAt(0);
        }

         else if (skyState == SkyState.star)
        {
            starSkys[0].transform.position = topPos;
            starSkys[0].SetActive(true);
            starSkys.Add(starSkys[0]);
            starSkys.RemoveAt(0);
        }
    }
}
