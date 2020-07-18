using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPoint : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public GameObject target2;
    public NavMeshPath path;
    public GameObject player;
    public bool pathCalculated;
    public float rotationSpeed;
    public float distanceToPlayer;
    public bool arivedAtPoint;
    public bool talkedToPLayer;
    public bool needsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        player= GameObject.FindGameObjectWithTag("Player");
        target2 = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(needsPlayer){
            target=player;
        }else{
            target=target2;
        }
        if(!arivedAtPoint){
            CheckDistance();
            RotateToTarget();
            DrawPath();
            talkedToPLayer=false;
        }else{
            Debug.Log("ITS HERE!!!!");
            if(Vector3.Distance(player.transform.position,transform.position)<3 && talkedToPLayer==false){
                talkedToPLayer=true;
                GetComponent<EnemyInteract2>().TalkingToPlayer();
                
                GetComponent<EnemyInteract2>().DisableScripts();
                
            }
        }
    }

    void CheckDistance()
    {
        if (target == null)
        {
            anim.SetFloat("Speed", 0f);
        }

        if (pathCalculated == false)
        {
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
            pathCalculated=true;
            //Debug.Log("test");
        }
        
        if (Vector3.Distance(transform.position, target.transform.position) > 1f)
        {
            anim.SetFloat("Speed", 1f);
            RotateToTarget();
        }
        else
        {
            //Debug.Log("SOULD FIND NEW TARGET WHY ISNT IT");
            anim.SetFloat("Speed", 0f);
            arivedAtPoint=true;
        }
    }

    void DrawPath(){
        for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
    }

    void RotateToTarget()
    {
        //Debug.Log(Vector3.Distance (path.corners[1],transform.position));
        if(Vector3.Distance (path.corners[1],transform.position)<0.3f){
            pathCalculated=false;
        }
        Vector3 cornerPosition= new Vector3(path.corners[1].x-transform.position.x,
                                            0,
                                            path.corners[1].z-transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotationSpeed * Time.deltaTime,0f);
        transform.rotation= Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("House")){
            pathCalculated=false;
            Debug.Log("WTF!!!!");
        }
    }
}
