using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Quest : MonoBehaviour
{
    public static Quest quest = null;

    private void Awake()
    {
        quest = this;
    }
    public string[] quests;
    public GameObject[] targets;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
