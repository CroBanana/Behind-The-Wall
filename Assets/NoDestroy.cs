using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroy : MonoBehaviour
{
    static NoDestroy instance;
    // Start is called before the first frame update
    private void Awake() {
        if(instance== null){
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance !=this){
            Destroy(gameObject);
        }
    }
}
