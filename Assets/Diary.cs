﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public GameObject key;
    public GameObject picture;

    private void Update() {
        if(key==null && picture == null){
            Quest.SetNextObjective();
        }
    }
}
