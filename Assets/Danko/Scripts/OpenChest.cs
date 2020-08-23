using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public float rotation = -90;
    public float rotationSpeed;
    public Transform topChest;
    public GameObject item1, item2;
    public bool itemsPicked;

    private void Start() {
        topChest = transform.Find("reinforced_wooden_chest_top");
    }

    public IEnumerator Open(){

        StartCoroutine(Check());
        while (rotation-topChest.eulerAngles.z<=-0.1f || rotation-topChest.eulerAngles.z>=0.1f){
            topChest.localRotation = Quaternion.RotateTowards(topChest.localRotation,
                                            Quaternion.Euler(0,0,rotation),
                                            rotationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            //Debug.Log(transform.eulerAngles.z);
        }
        //Debug.Log("stoped opening chest");
    }

    public IEnumerator Check(){
        itemsPicked=false;
        while(!itemsPicked){
            if(item1!=null || item2 !=null){
                if(item2 !=null){
                    if(item1.transform.parent!=transform && item2.transform.parent!=transform){
                        Quest.SetNextObjective();
                        itemsPicked=true;
            }
            }
            else if(item1.transform.parent!=transform && item2==null){
                Quest.SetNextObjective();
                itemsPicked=true;
            }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void OpenChestCorutine(){
        StartCoroutine(Open());
    }

    IEnumerator Stop(){
        yield return new WaitForSeconds(3f);
        StopAllCoroutines();
    }
}
