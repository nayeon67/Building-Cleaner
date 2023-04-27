using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Button upButton;
    private int count;
    void Start()
    {
        upButton = GameObject.Find("Top").GetComponent<Button>();
        upButton.onClick.AddListener(Down);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Down()
    {
        float y = transform.position.y;
        y--;
        transform.position = new Vector2(transform.position.x, y);

        count++;

        if(count % 15 == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
