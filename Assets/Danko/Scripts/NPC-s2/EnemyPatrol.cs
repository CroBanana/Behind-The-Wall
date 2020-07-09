using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Animator anim;

    public GameObject target;
    public List<GameObject> patrolPoints;

    public bool stuckDistanceCorutine;
    public bool pathCalculated;

    public float rotateSpeed;
    public float distanceOld;
    public float distanceNew;
    public int objectInList = 0;


    Quaternion quar;

    public NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToTarget_AndSwitchTarget();
        if(stuckDistanceCorutine==false){
            StopAllCoroutines();
            StartCoroutine(CheckIfStuck());
            stuckDistanceCorutine=true;
        }
        DrawPath();
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
            //Debug.Log("test");
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

    void DrawPath(){
        for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
    }

    IEnumerator CheckIfStuck(){
        while(true){
            //Debug.Log("Cheching if stuck");
            distanceOld=Vector3.Distance(path.corners[1],transform.position);
            yield return new WaitForSeconds(2);
            distanceNew=Vector3.Distance(path.corners[1],transform.position);
            if(distanceNew == distanceOld){
                Debug.Log("        Yes he is stuck");
                pathCalculated=false;
            }
            Debug.Log(distanceNew+ "   "      +distanceOld);
        }
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
}
