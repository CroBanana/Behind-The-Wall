using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGuardFoundPlayer : MonoBehaviour
{
    public Animator anim;
    public NavMeshPath path;
    public Vector3 playerLastPosition;
    public GameObject player;
    public EnemyGuard enemyGuard;
    public float distanceToPlayer;
    public bool followPlayer;
    public bool pathCalculated;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        path = new NavMeshPath();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyGuard = gameObject.GetComponent<EnemyGuard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer == false)
        {
            followPlayer = true;
            StartCoroutine(FollowPlayer());
            anim.SetFloat("Speed", 1f);
        }
        RotateToTarget();
        DrawPath();
        PlayerInRange();
    }

    void DrawPath()
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
    }
    IEnumerator FollowPlayer()
    {
        bool suck=true;
        //Debug.Log("FollowPlayer");
        while (suck)
        {
            //agent.enabled=true;
            if(Vector3.Distance(transform.position,playerLastPosition)<distanceToPlayer/2f){
                //Debug.Log("Last player location was here");
                gameObject.GetComponent<EnemyGuard>().playerLost=true;
                suck=false;
            }else{
                NavMesh.CalculatePath(transform.position, playerLastPosition, NavMesh.AllAreas, path);
            }
            //Debug.Log("how many times");
            yield return new WaitForSeconds(0.5f);
        }
    }

    void RotateToTarget()
    {
        //Debug.Log("Rotate");

        Vector3 cornerPosition = new Vector3(path.corners[1].x - transform.position.x,
                                            0,
                                            path.corners[1].z - transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime,0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void PlayerInRange()
    {
        /*
        if (Vector3.Distance(player.transform.position, transform.position) < distanceToPlayer)
        {
            Debug.Log("WTF!!");
            enemyGuard.TalkingToPlayer();
            enemyGuard.DisableScripts();
        }
        */
    }

}
