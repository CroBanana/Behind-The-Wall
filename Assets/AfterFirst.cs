using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterFirst : MonoBehaviour
{
    public static bool needsTOBeClosed = false;
    public Transform rotation;
    // Update is called once per frame
    void Update()
    {
        if(needsTOBeClosed){
            transform.eulerAngles=new Vector3(0,-22,0);
            needsTOBeClosed=false;
        }
    }
}
