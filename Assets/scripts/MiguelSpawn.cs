using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiguelSpawn : MonoBehaviour
{
    public GameObject[] miguelSpawnLocations;
    // Start is called before the first frame update
    public void teleportTO(){
        if(Quest.miguelRandomSpawn){
            int index= Random.Range(0,miguelSpawnLocations.Length);
            transform.position = miguelSpawnLocations[index].transform.position;
        }
    }
}
