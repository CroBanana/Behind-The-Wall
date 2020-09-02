using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayer : MonoBehaviour
{
    public GameObject escapeObject;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Quest.SetNextObjective();
            if(escapeObject!= null){
                escapeObject.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}
