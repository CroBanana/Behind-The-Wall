using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollisionNPC : MonoBehaviour
{
    private HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {
        joint=GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Farmer") || other.gameObject.CompareTag("Workers")){
            joint.useLimits=false;
        }
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Farmer") || other.gameObject.CompareTag("Workers")){
            joint.useLimits=true;
        }
    }
}
