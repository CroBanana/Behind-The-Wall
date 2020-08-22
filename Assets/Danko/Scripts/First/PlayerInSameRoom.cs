using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSameRoom : MonoBehaviour
{
    RaycastHit hit;
    
    public GameObject player;
    public GameObject follower;
    public GameObject pointToGo;

    public Dialog leadPlayerToRoom;
    public Dialog finalyEnd;
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerDirection = player.transform.position-transform.position;
        Debug.DrawRay(transform.position+transform.up,playerDirection*15,Color.green);
        if(Physics.Raycast(transform.position+transform.up,playerDirection.normalized,out hit)){
                if(hit.collider.gameObject.CompareTag("Player")){
                    gameObject.GetComponent<EnemyFollowPlayer>().enabled=true;
                    enabled=false;
                }
            }
    }

    public void UpdateNPC(){
        follower.GetComponent<GoToPoint>().target= player;
        follower.GetComponent<GoToPoint>().target2 = pointToGo;
        follower.GetComponent<GoToPoint>().needsPlayer=true;
        follower.GetComponent<GoToPoint>().talkedToPLayer=false;
        follower.GetComponent<GoToPoint>().enabled=true;
        follower.GetComponent<DialogTrigger>().howManyTimesTalked=0;

    }
}
