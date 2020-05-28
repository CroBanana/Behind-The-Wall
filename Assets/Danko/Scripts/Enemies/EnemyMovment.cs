using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovment : MonoBehaviour
{
    //path movment related
    public GameObject target;
    public GameObject player;
    //public NavMeshAgent agent;
    public Animator anim;
    public NavMeshPath path;
    public bool createdOnce=false;
    public float rotateSpeed;
    Quaternion quar;
    public List<GameObject> patrolPoints;
    int objectInList = 0;
    public bool isTalking=false;
    public bool didOnce = false;
    public GameObject chatCanvas;
    public bool pathCalculated=false;
    public bool patroling = true;
    public bool needsPlayer = false;
    public bool findingPlayer = false;
    float stuckCheck;
    float distanceOld;
    float distanceNew;
    bool stuckDistanceCorutine;

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(needsPlayer==false){
            StopCoroutine(FollowPlayer());
        }
        if(isTalking == false && needsPlayer)
        {
            if(findingPlayer==false){
                StopAllCoroutines();
                StartCoroutine(FollowPlayer());
                findingPlayer=true;
            }
            DrawPath();
            RotateToTarget();
            NeedsToTalkToPlayer();
            stuckDistanceCorutine=true;
        }
        else if (isTalking == false && patroling)
        {
            findingPlayer=false;
            if(stuckDistanceCorutine==true){
                StopAllCoroutines();
                StartCoroutine(CheckIfStuck());
                stuckDistanceCorutine=false;
            }
            didOnce = false;
            chatCanvas.SetActive(false);
            
            CheckDistanceToTarget_AndSwitchTarget();
            DrawPath();
        }
        else
        {
            if (didOnce == false)
            {

                TalkingToTarget();
            }
        }
    }

    void DrawPath(){
        for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
    }
    IEnumerator FollowPlayer(){
        while (true){
            //agent.enabled=true;
            NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path);
            //agent.enabled=false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator CheckIfStuck(){
        while(patroling){
            Debug.Log("Cheching if stuck");
            distanceOld=Vector3.Distance(path.corners[1],transform.position);
            yield return new WaitForSeconds(3);
            distanceNew=Vector3.Distance(path.corners[1],transform.position);
            if(distanceNew == distanceOld){
                Debug.Log("        Yes he is stuck");
                pathCalculated=false;
            }
            Debug.Log(distanceNew+ "   "      +distanceOld);
        }
    }

    void CheckDistanceToTarget_AndSwitchTarget()
    {
        if (target == null)
        {
            target = patrolPoints[0];
        }

        if (pathCalculated == false)
        {
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
            pathCalculated=true;
            Debug.Log("test");
        }
        
        if (Vector3.Distance(transform.position, target.transform.position) > 2f)
        {
            anim.SetFloat("Speed", 1f);
            RotateToTarget();
        }
        else
        {
            FindNewTarget();
            anim.SetFloat("Speed", 0f);
        }
    }

    void NeedsToTalkToPlayer(){
        if(Vector3.Distance(player.transform.position,transform.position)<=1f){
            player.GetComponent<PlayerMovment>().ePressed=true;
        }
    }

    void TalkingToTarget()
    {
        chatCanvas.SetActive(true);
        gameObject.GetComponent<DialogTrigger>().TriggerDialog();
        anim.SetFloat("Speed", 0f);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        didOnce = true;
    }

    void RotateToTarget()
    {
        if(Vector3.Distance (path.corners[1],transform.position)<0.2f){
            pathCalculated=false;
        }
        quar = Quaternion.LookRotation(path.corners[1] - transform.position);
        Vector3 cornerPosition= new Vector3(path.corners[1].x-transform.position.x,
                                            0,
                                            path.corners[1].z-transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime,0f);
        transform.rotation= Quaternion.LookRotation(newDirection);
    }

    void FindNewTarget()
    {
        objectInList++;
        try
        {
            target = patrolPoints[objectInList];
        }
        catch
        {
            objectInList = 0;
            target = patrolPoints[objectInList];
        }
        pathCalculated = false;
    }
}
