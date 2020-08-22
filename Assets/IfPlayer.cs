using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Quest.SetNextObjective();
            Destroy(gameObject);
        }
    }
}
