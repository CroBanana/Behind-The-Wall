using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Waypoint : MonoBehaviour
{
    public static Waypoint instance = null;

    private void Awake()
    {
        instance = this;
    }

    public Image img;
    public Transform target;
    public Transform player;
    public Text meter;
    public Camera cam;
    public float closeDist;

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
        }else{
            img.enabled=false;
            meter.enabled=false;
        }
    }

    private void GetDistance(){
        float dist = (int) Vector3.Distance(player.position,target.position);
        if(dist>closeDist){
            meter.text = dist.ToString()+"m";
        }else{
            meter.text="";
        }
    }

    private void CheckOnScreen(){
        float th= Vector3.Dot((target.position-cam.transform.position).normalized,cam.transform.forward);
        if(th <=0){
            ToogleUI(false);
        }else{
            ToogleUI(true);
            img.transform.position = cam.WorldToScreenPoint(target.position);
        }
    }
    private void ToogleUI(bool value){
        img.enabled=value;
        meter.enabled=value;
    }
    public void SetWaypoint(Transform nextObjective){
        target= nextObjective;
    }
}
