using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostPoints : MonoBehaviour
{
    public List<GameObject> searchPoints;
    // Start is called before the first frame update
    void Start()
    {
        searchPoints.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            var test= FindObjectsOfType<EnemyGuard>();
            foreach(var point in test){
                point.searchForPlayer =searchPoints;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            var test= FindObjectsOfType<EnemyGuard>();
            foreach(var point in test){
                point.searchForPlayer =null;
            }
        }
    }
}
