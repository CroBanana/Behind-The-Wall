using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterFirst : MonoBehaviour
{
    public static bool needsTOBeClosed;
    public Transform rotation;
    // Update is called once per frame
    private void Start() {
        if(needsTOBeClosed){
            Debug.Log("Closing");
            transform.localEulerAngles=new Vector3(0,-22,0);
            needsTOBeClosed=false;
        }
    }
}
