using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public void ResetNow(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>().ResetEPress();
    }
}
