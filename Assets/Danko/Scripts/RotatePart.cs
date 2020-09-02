using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePart : MonoBehaviour
{
    public float rotation;
    public float rotationSpeed;
    public float distanceToRotation;
    public bool stillRotating;

    private void Start() {
        rotation = transform.localEulerAngles.z;
    }

    private void Update() {
        if(rotation == transform.localEulerAngles.z){
                //Debug.Log("NIGGA WHY YOU NO WORK");
                stillRotating=false;
            }
    }
    // Start is called before the first frame update
    IEnumerator RotateTowards(){

        while (rotation-transform.localEulerAngles.z<=-0.01f || rotation-transform.localEulerAngles.z>=0.01f){
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation,
                                            Quaternion.Euler(0,0,rotation),
                                            rotationSpeed * Time.deltaTime);
            
            yield return new WaitForEndOfFrame();
            //Debug.Log(transform.eulerAngles.z);
        }
        //transform.GetComponentInParent<Puzzle>().CheckIfCorrect();

    }
    public void RotateAndWait(){
        stillRotating=true;
        if(rotation==360){
            rotation=0;
        }
        StopAllCoroutines();
        StartCoroutine(RotateTowards());
        //StartCoroutine(StopRotation());
    }
}
