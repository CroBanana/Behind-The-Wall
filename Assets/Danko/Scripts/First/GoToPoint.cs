using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPoint : MonoBehaviour
{
    public Animator anim;
    public GameObject target;
    public NavMeshPath path;
    public GameObject player;
    public bool pathCalculated;
    public float rotationSpeed;
    public float distanceToPlayer;
    public bool arivedAtPoint;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!arivedAtPoint){
            CheckDistance();
        RotateToTarget();
        DrawPath();
        }else{
            if(Vector3.Distance(player.transform.position,transform.position)<3){
                gameObject.GetComponent<EnemyInteract2>().TalkingToPlayer();
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
