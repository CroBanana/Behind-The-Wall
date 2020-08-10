using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Transform player;
    public Text meter;
    public Camera cam;
    float closeDist;

    private void Start() {
        player=  GameObject.FindGameObjectWithTag("Player").transform;
        cam=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null){
            GetDistance();
            CheckOnScreen();
        }
    }

    private void GetDistance(){
        float dist = Vector3.Distance(player.position,target.position);
        meter.text = dist.ToString("f1")+"m";
        if(dist<closeDist){
            
        }
    }

    private void CheckOnScreen(){
        float th= Vector3.Dot((target.position-cam.transform.position).normalized,cam.transform.forward);
        if(th <=0){
            ToogleUI(false);
        }else{
            ToogleUI(true);
            transform.position = cam.WorldToScreenPoint(target.position);
        }
    }
    private void ToogleUI(bool value){
        img.enabled=value;
        meter.enabled=value;
    }
}
