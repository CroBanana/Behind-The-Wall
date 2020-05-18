using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovment : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public NavMeshAgent agent;
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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
        path = new NavMeshPath();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isTalking == false)
        {
            didOnce = false;
            chatCanvas.SetActive(false);
            if (target == null)
            {
                target = patrolPoints[0];
            }
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);

            for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
            //Debug.Log(Vector3.Distance(transform.position, target.transform.position));

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
        else
        {
            if (didOnce == false)
            {

                TalkingToTarget();
            }
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
        quar = Quaternion.LookRotation(path.corners[1] - transform.position);
        //Debug.Log(target.name);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quar, rotateSpeed * Time.deltaTime);
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
    }
}
