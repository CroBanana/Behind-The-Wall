using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostPoints : MonoBehaviour
{
    public List<GameObject> searchPoints;
    // Start is called before the first frame update
    public EnemyGuard[] test;
    void Start()
    {
        searchPoints.Add(gameObject);
        test= FindObjectsOfType<EnemyGuard>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            foreach(var point in test){
                point.searchForPlayer =searchPoints;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            foreach(var point in test){
                point.searchForPlayer =null;
            }
        }
    }
}
