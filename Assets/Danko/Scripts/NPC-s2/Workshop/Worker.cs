using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public Animator anim;
    public List<Transform> workPoints;
    public Transform workPoint;
    public float distanceToWorkPoint;
    public bool startWork;
    public int workInt=0;

    public NavMeshPath path;
    public bool pathCalculated;

    public float rotateSpeed;
    public float distanceNew;
    public float distanceOld;
    bool stuckDistanceCorutine;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfNearWorkPlace();
        if(stuckDistanceCorutine==false){
            StartCoroutine(CheckIfStuck());
            stuckDistanceCorutine=true;
        }
        //Debug.Log(Vector3.Distance(transform.position,workPoint.position));
        DrawPath();
    }

    void CheckIfNearWorkPlace()
    {
        //ako crop ne postoji uzme prvog iz liste
        if (workPoint == null)
        {
            workPoint = workPoints[0];
        }

        //kalkulira put prema biljki
        if (pathCalculated == false)
        {
            NavMesh.CalculatePath(transform.position, workPoint.position, NavMesh.AllAreas, path);
            pathCalculated=true;
            //Debug.Log("test");
        }

        //ako je udaljenost veca od neke vrijednosti npc ce se kretati do biljke. 
        // ako dođe u blizinu pokrene se corutine koji pobere biljku nakon nekoliko sekundi
        if (Vector3.Distance(transform.position, workPoint.position) > distanceToWorkPoint)
        {
            startWork=false;
            anim.SetFloat("Speed", 1f);
            RotateToNextObjective();
        }
        else
        {
            if(startWork==false){
                startWork=true;
                StartCoroutine(DoJob());
            }
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
            yield return new WaitForSeconds(10);
            distanceNew=Vector3.Distance(path.corners[1],transform.position);
            if(distanceNew == distanceOld){
                pathCalculated=false;
            }
        }
    }


    IEnumerator DoJob(){
        //Debug.Log("Doing job");
        anim.SetFloat("Speed", 0f);
        yield return new WaitForSeconds(10);
        //Debug.Log("Job Ended");
        workInt++;
        if(workInt>workPoints.Count-1){
            workInt=0;
        }
        workPoint=workPoints[workInt];
    }



    void RotateToNextObjective()
    {
        if(Vector3.Distance (path.corners[1],transform.position)<0.2f){
            pathCalculated=false;
        }
        Vector3 cornerPosition= new Vector3(path.corners[1].x-transform.position.x,
                                            0,
                                            path.corners[1].z-transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime,0f);
        transform.rotation= Quaternion.LookRotation(newDirection);
    }
}
