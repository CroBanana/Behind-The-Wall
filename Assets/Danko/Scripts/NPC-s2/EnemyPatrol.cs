using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Animator anim;

    public GameObject target;
    public List<GameObject> patrolPoints;

    // za zastitara mjesta koja mora proći ako je izgubio igraca

    public bool stuckDistanceCorutine;
    public bool pathCalculated;

    public float rotateSpeed;
    public float distanceOld;
    public float distanceNew;
    public int objectInList = 0;


    public NavMeshPath path;

    [Header ("Izgubio igraca")]
    public List<GameObject> searchForPlayer;
    public float secondsMin,secondsMax;
    public bool waitAFewSeconds;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        searchForPlayer=null;
        //Debug.Log("start");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!waitAFewSeconds){
            CheckDistanceToTarget_AndSwitchTarget();
        }else{
            transform.Rotate(Vector3.up * rotateSpeed*10f*Time.deltaTime);
        }
        DrawPath();
    }

    void CheckDistanceToTarget_AndSwitchTarget()
    {
        if (target == null)
        {
            if(searchForPlayer !=null){
                
                target = searchForPlayer[(int) Random.Range(0,searchForPlayer.Count)];
            }else{
                target = patrolPoints[0];
            }
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
            Debug.Log("SOULD FIND NEW TARGET WHY ISNT IT");
            anim.SetFloat("Speed", 0f);
            FindNewTarget();
        }
    }

    void DrawPath(){
        for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
    }

    void FindNewTarget()
    {
        try
        {
            if(searchForPlayer!=null){
                StartCoroutine(WaitABit());
                searchForPlayer.Remove(target);
                target = searchForPlayer[(int) Random.Range(0,searchForPlayer.Count)];
            }else{
                Debug.Log("set OtherObject");
                objectInList++;
                target = patrolPoints[objectInList];
            }
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
        //Debug.Log(Vector3.Distance (path.corners[1],transform.position));
        if(Vector3.Distance (path.corners[1],transform.position)<0.3f){
            pathCalculated=false;
        }
        Vector3 cornerPosition= new Vector3(path.corners[1].x-transform.position.x,
                                            0,
                                            path.corners[1].z-transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime,0f);
        transform.rotation= Quaternion.LookRotation(newDirection);
    }

    IEnumerator WaitABit(){
        waitAFewSeconds=true;
        yield return new WaitForSeconds(Random.Range(secondsMin,secondsMin));
        waitAFewSeconds=false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("House")){
            pathCalculated=false;
            Debug.Log("WTF!!!!");
        }
    }
}
