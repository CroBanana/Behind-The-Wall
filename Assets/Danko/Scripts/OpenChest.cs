using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public float rotation = -90;
    public float rotationSpeed;
    public Transform topChest;

    private void Start() {
        topChest = transform.Find("reinforced_wooden_chest_top");
    }

    public IEnumerator Open(){


        while (rotation-topChest.eulerAngles.z<=-0.1f || rotation-topChest.eulerAngles.z>=0.1f){
            topChest.rotation = Quaternion.RotateTowards(topChest.rotation,
                                            Quaternion.Euler(0,0,rotation),
                                            rotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            //Debug.Log(transform.eulerAngles.z);
        }
        //Debug.Log("stoped opening chest");
    }

    IEnumerator Stop(){
        yield return new WaitForSeconds(3f);
        StopAllCoroutines();
    }
}
