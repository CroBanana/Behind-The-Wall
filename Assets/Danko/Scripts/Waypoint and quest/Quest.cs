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

    static public bool isItNight;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(currentObjective);
        questssText=questsText;
        targetss=targets;
        text=text2;
        needsEnabling=enable;
        text.text=questssText[currentObjective];
        if(targetss[currentObjective]!=null){
            Waypoint.instance.SetWaypoint(targetss[currentObjective].transform);
            if(targetss[currentObjective].CompareTag("Bed")){
                targetss[currentObjective].layer=13;
            }
        }else{
            Waypoint.instance.SetWaypoint(null);
        }
        if(isItNight){
            Debug.Log("Object Sould Be Enabled: "+ needsEnabling[currentActivatedObject].name);
            needsEnabling[currentActivatedObject].SetActive(true);
        }
    }

    public static void SetNextObjective(){
        currentObjective++;
        Debug.Log(currentObjective);
        text.text=questssText[currentObjective];
        if(targetss[currentObjective]!=null){
            Waypoint.instance.SetWaypoint(targetss[currentObjective].transform);
            if(targetss[currentObjective].CompareTag("Bed")){
                targetss[currentObjective].layer=13;
            }
        }else{
            Waypoint.instance.SetWaypoint(null);
        }
        if(isItNight){
            Debug.Log("Object Sould Be Enabled");
            needsEnabling[currentActivatedObject].SetActive(true);
        }
    }
}
