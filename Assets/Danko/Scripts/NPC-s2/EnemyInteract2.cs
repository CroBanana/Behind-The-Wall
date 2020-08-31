using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract2 : MonoBehaviour
{
    public PlayerInteract playerInteract;
    public EnemyFollowPlayer enemyFollowPlayer;
    public EnemyPatrol enemyPatrol;
    public GoToPoint goToPoint;
    public PlayerInSameRoom playerInSame;
    public Farmer farmer;
    public Worker worker;

    public bool enemyP;
    public bool gotoP;
    // Start is called before the first frame update
    void Start()
    {
        playerInteract= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        enemyFollowPlayer = gameObject.GetComponent<EnemyFollowPlayer>();
        enemyPatrol = gameObject.GetComponent<EnemyPatrol>();
        goToPoint = gameObject.GetComponent<GoToPoint>();
        enemyP=true;
        farmer = gameObject.GetComponent<Farmer>();
        worker =  gameObject.GetComponent<Worker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TalkingToPlayer()
    {
        playerInteract.ePressed=true;
        playerInteract.focusedObject=gameObject;
        playerInteract.Interact();
    }

    public void DisableScripts(){
        if(enemyPatrol!=null){
            if(enemyPatrol.enabled){
            //Debug.Log("EnemyPatrol is enabled");
            enemyPatrol.anim.SetFloat("Speed", 0f);
            enemyPatrol.enabled=false;
        }
        }
        if(enemyFollowPlayer!=null){
            if(enemyFollowPlayer.enabled){
            //Debug.Log("EnemyFollowPlayer is enabled");
            enemyFollowPlayer.anim.SetFloat("Speed", 0f);
            enemyFollowPlayer.enabled=false;
        }
        }
        if(farmer!=null){
            farmer.anim.SetFloat("Speed",0f);
            farmer.enabled=false;
        }
        if(worker!= null){
            worker.anim.SetFloat("Speed",0f);
            worker.enabled=false;
        }

    }


    public void DialogeEnded(){
        if(enemyPatrol!=null)
            enemyPatrol.enabled=enemyP;
        if(goToPoint!=null){
            //Debug.Log("THIS IS SET!!!");
            if(Vector3.Distance(goToPoint.target2.transform.position,transform.position)<5){
                goToPoint.enabled=false;
                goToPoint.arivedAtPoint=false;
                goToPoint.needsPlayer=false;
                goToPoint.talkedToPLayer=true;
            }else{
                goToPoint.arivedAtPoint=false;
                goToPoint.needsPlayer=false;
                goToPoint.talkedToPLayer=true;
                goToPoint.enabled=gotoP;
            }
        }
        if(playerInSame != null){
                //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA");
                GetComponent<PlayerInSameRoom>().UpdateNPC();
                GetComponent<PlayerInSameRoom>().enabled=false;
        }
        if(farmer!=null){
            farmer.enabled=true;
        }
        if(worker!=null){
            worker.enabled=true;
        }

    }
}
