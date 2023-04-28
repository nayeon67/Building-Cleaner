using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObstacle : MonoBehaviour
{
    [SerializeField] GameObject imgGameObject;
    private SpriteRenderer theSR;
    [SerializeField] Sprite[] garbageImgs;
    

    [SerializeField] float fallSpeed = 3f;
    
    void Start()
    {
        theSR = imgGameObject.GetComponent<SpriteRenderer>();

        int garbageNum = Random.Range(0, 3);
        theSR.sprite = garbageImgs[garbageNum];
    }

    
    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        if(transform.position.y <= -1)
        {
            Destroy(this.gameObject);
        }
    }
}
