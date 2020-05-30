using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocked : MonoBehaviour
{
    public float rotationSpeed;
    public Quaternion qua;
    public bool rotateTo=false;
    public float timer;
    // Start is called before the first frame update

    private void Update() {
        if(rotateTo){
            StartCoroutine(Rotate());
            rotateTo=false;
        }
    }

    public IEnumerator Rotate(){
        Debug.Log("Started");
        while(timer>=0){
            yield return new WaitForEndOfFrame();
            transform.rotation=Quaternion.Slerp(transform.rotation,qua,rotationSpeed*Time.deltaTime);
            timer-=Time.deltaTime;
        }
        Debug.Log("Not moving anymore");
    }
}
