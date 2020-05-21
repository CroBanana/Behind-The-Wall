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
        if(other.gameObject.CompareTag("NPC")){
            joint.useLimits=false;
        }
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("NPC")){
            joint.useLimits=true;
        }
    }
}
