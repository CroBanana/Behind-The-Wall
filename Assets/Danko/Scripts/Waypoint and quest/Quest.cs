using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Quest : MonoBehaviour
{
    public static Quest instance = null;

    private void Awake()
    {
        instance = this;
    }
    public string[] quests;
    public GameObject[] targets;
    public Text questText;
    public int currentObjective;

    // Start is called before the first frame update
    void Start()
    {
        questText.text=quests[currentObjective];
        Waypoint.instance.SetWaypoint(targets[currentObjective].transform);
    }

    public void SetNextObjective(){
        currentObjective++;
        questText.text=quests[currentObjective];
        Waypoint.instance.SetWaypoint(targets[currentObjective].transform);
    }
}
