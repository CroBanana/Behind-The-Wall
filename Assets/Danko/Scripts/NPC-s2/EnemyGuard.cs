using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : MonoBehaviour
{
    public PlayerInteract playerInteract;
    public EnemyGuardFoundPlayer enemyGuardFoundPlayer;
    public EnemyPatrol enemyPatrol;
    public List<GameObject> searchForPlayer;
    public GameObject inzone;

    public bool enemyP;

    //enemy detecting player
    public bool disableRaycast;
    public float fieldOfViewAngle;
    public float viewDistance;
    public float viewDistance_PlayerDetected;
    public float viewDistance_Set;
    public float angle;
    RaycastHit hit;
    public GameObject player;
    public Vector3 playerDirection;
    public bool playerFound;
    public bool playerLost;

    public LayerMask rayable;

    // Start is called before the first frame update
    void Start()
    {
        playerInteract= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
        enemyGuardFoundPlayer = gameObject.GetComponent<EnemyGuardFoundPlayer>();
        enemyPatrol = gameObject.GetComponent<EnemyPatrol>();
        enemyP=true;
        viewDistance_Set=viewDistance;
        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerDirection = player.transform.position-transform.position;
        Vector3 angleBorderLeft = Quaternion.Euler(0,fieldOfViewAngle,0)* transform.forward;
        Vector3 angleBorderRight = Quaternion.Euler(0,-fieldOfViewAngle,0)* transform.forward;
        angle = Vector3.Angle(playerDirection,transform.forward);
        Debug.DrawRay(transform.position+transform.up,playerDirection*viewDistance_Set,Color.green);
        Debug.DrawRay(transform.position+transform.up,angleBorderLeft*viewDistance_Set,Color.yellow);
        Debug.DrawRay(transform.position+transform.up,angleBorderRight*viewDistance_Set,Color.yellow);
        if(angle<fieldOfViewAngle && playerFound== false){
            //Debug.Log(angle);
            //position of mouse on screen
            if(Physics.Raycast(transform.position+transform.up,playerDirection.normalized,out hit,viewDistance_Set,rayable)){
                if(hit.collider.gameObject.CompareTag("Player")){
                    Debug.Log("Player detected");
                    if(!playerFound){
                        DisableScripts();
                        enemyGuardFoundPlayer.enabled=true;
                        viewDistance_Set=viewDistance_PlayerDetected;
                        playerFound=true;
                        playerLost=false;
                    }
                    PlayerDetected();
                }
            }
        }else{
            if(Physics.Raycast(transform.position+transform.up,playerDirection.normalized,out hit,viewDistance_Set,rayable)){
                if(hit.collider.gameObject.CompareTag("Player")){
                    PlayerDetected();
                    
                }
            }
        }
        if(playerLost){
            StartCoroutine(PlayerLost());
            playerFound=false;
            Debug.Log("Player lost");
            playerLost=false;
        }
    }

    public void TalkingToPlayer()
    {
        playerInteract.ePressed=true;
        playerInteract.focusedObject=gameObject;
        playerInteract.Interact();
    }

    public void DisableScripts(){
        //Debug.Log("Scripts disabled");
        if(enemyPatrol.enabled){
            Debug.Log("EnemyPatrol is enabled");
            enemyPatrol.StopAllCoroutines();
            enemyPatrol.anim.SetFloat("Speed", 0f);
            enemyPatrol.enabled=false;
        }

        if(enemyGuardFoundPlayer.enabled){
            Debug.Log("EnemyFollowPlayer is enabled");
            enemyGuardFoundPlayer.anim.SetFloat("Speed", 0f);
            enemyGuardFoundPlayer.StopAllCoroutines();
            enemyGuardFoundPlayer.followPlayer=false;
            enemyGuardFoundPlayer.enabled=false;
        }
    }

    public void DialogeEnded(){
        enemyPatrol.enabled=enemyP;
    }

    public void PlayerDetected(){
        enemyGuardFoundPlayer.playerLastPosition = player.transform.position;
    }
    
    public IEnumerator PlayerLost(){
        DisableScripts();
        yield return new WaitForSeconds(2f);
        enemyPatrol.searchForPlayer=searchForPlayer;
        enemyPatrol.target=null;
        viewDistance_Set=viewDistance;
        enemyPatrol.stuckDistanceCorutine=false;
        enemyPatrol.pathCalculated=false;
        yield return new WaitForSeconds(0.5f);
        enemyPatrol.enabled=true;
    }
}
