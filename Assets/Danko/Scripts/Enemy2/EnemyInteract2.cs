using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract2 : MonoBehaviour
{
    public PlayerInteract playerInteract;
    public EnemyFollowPlayer enemyFollowPlayer;
    public EnemyPatrol enemyPatrol;

    public bool enemyP;
    // Start is called before the first frame update
    void Start()
    {
        playerInteract= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        enemyFollowPlayer = gameObject.GetComponent<EnemyFollowPlayer>();
        enemyPatrol = gameObject.GetComponent<EnemyPatrol>();
        enemyP=true;
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
        if(enemyPatrol.enabled){
            Debug.Log("EnemyPatrol is enabled");
            enemyPatrol.anim.SetFloat("Speed", 0f);
            enemyPatrol.enabled=false;
        }

        if(enemyFollowPlayer.enabled){
            Debug.Log("EnemyFollowPlayer is enabled");
            enemyFollowPlayer.anim.SetFloat("Speed", 0f);
            enemyFollowPlayer.enabled=false;
        }
    }

    public void DialogeEnded(){
        enemyPatrol.enabled=enemyP;
    }
}
