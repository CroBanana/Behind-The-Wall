using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    [TextArea(1,3)]
    public string[] questsText;
    [TextArea(1,3)]
    static public string[] questssText;
    public GameObject[] targets;
    static public GameObject[] targetss;
    public static Text text;
    public Text text2;
    static public int currentObjective;
    public GameObject[] enable;
    static public GameObject[] needsEnabling;
    static public int currentActivatedObject;
    static public bool miguelRandomSpawn;
    static public bool playerSpawn;
    public static bool didOnce;
    public static List<GameObject> corn;

    static public bool isItNight;
    // Start is called before the first frame update
    void Start()
    {
        if(!didOnce){
            Debug.Log(currentObjective);
            questssText=questsText;
            targetss=targets;
            text=text2;
            needsEnabling=enable;
            didOnce=true;
            corn= new List<GameObject>();
            GameObject[] objets=GameObject.FindGameObjectsWithTag("Mrkva");
            foreach (var item in objets)
            {
                corn.Add(item);
            }
            text.text=questssText[currentObjective];
            if(targetss[currentObjective]!=null){
                Waypoint.SetWaypoint(targetss[currentObjective].transform);
                if(targetss[currentObjective].CompareTag("Bed")){
                    targetss[currentObjective].layer=13;
                }
            }else{
                Waypoint.SetWaypoint(null);
            }
            foreach(var item in needsEnabling){
                //item.SetActive(false);
            }
        }
    }

    public static void SetNextObjective(){
        currentObjective++;
        Debug.Log(currentObjective+"  "+targetss[currentObjective]);
        text.text=questssText[currentObjective];
        if(targetss[currentObjective]!=null){
            Waypoint.SetWaypoint(targetss[currentObjective].transform);
            if(targetss[currentObjective].CompareTag("Bed")){
                targetss[currentObjective].layer=13;
            }
        }else if(currentObjective==12){
            Debug.Log("it is 12");
            foreach(var item in corn){
                item.layer=13;
                Debug.Log("Layers set");
            }
            Debug.Log("WayPoint set maybe");
            Waypoint.SetWaypoint(corn[0].transform);
        }else{
            Waypoint.SetWaypoint(null);
        }
        
    }
    public static void EnablePuzzle(){
        Debug.Log("Object Sould Be Enabled: "+ needsEnabling[currentActivatedObject].name);
        needsEnabling[currentActivatedObject].SetActive(true);
    }
    public static void DisablePuzzle(){
        needsEnabling[currentActivatedObject].SetActive(false);
    }
}
