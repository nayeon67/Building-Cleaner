using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    private PlayerController thePC;
    private CameraMoving theCM;
    void Start()
    {
        thePC = FindObjectOfType<PlayerController>();
        theCM = FindObjectOfType<CameraMoving>();
    }

    // Update is called once per frame
    void Update()
    {
        //화면에서 벗어나면
        if (!theCM.CheckTargetInCamera(this.transform))
        {
            thePC.GetDamage();
            Destroy(this.gameObject);
        }
    }
}
