using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorkPoints : MonoBehaviour
{
    public GameObject[] workshops;
    public GameObject[] workers;
    // Start is called before the first frame update
    void Awake()
    {
        workshops = GameObject.FindGameObjectsWithTag("WorkshopPoints");
        workers= GameObject.FindGameObjectsWithTag("Workers");
        int test=0;
        foreach(var worker in workers){
            List<Transform> children= new List<Transform>();
            foreach(Transform child in workshops[test].transform){
                children.Add(child);
            }
            worker.GetComponent<Worker>().workPoints = children;
            test++;
        }
    }

}
