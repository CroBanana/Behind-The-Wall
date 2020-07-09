using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    public NavMeshPath path;
    public GameObject player;
    public Animator anim;
    public float rotateSpeed;

    public EnemyInteract2 enemyInteract2;

    public bool pathCalculated;
    public bool followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        path = new NavMeshPath();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyInteract2 = gameObject.GetComponent<EnemyInteract2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer == false)
        {
            StartCoroutine(FollowPlayer());
            anim.SetFloat("Speed", 1f);
            followPlayer = true;
        }
        DrawPath();
        RotateToTarget();
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
        while (true)
        {
            //agent.enabled=true;
            NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path);
            //agent.enabled=false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void RotateToTarget()
    {
        if (Vector3.Distance(path.corners[1], transform.position) < 0.2f)
        {
            pathCalculated = false;
        }

        Vector3 cornerPosition = new Vector3(path.corners[1].x - transform.position.x,
                                            0,
                                            path.corners[1].z - transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void PlayerInRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f)
        {
            Debug.Log("WTF!!");
            enemyInteract2.TalkingToPlayer();
            enemyInteract2.DisableScripts();
        }
    }
}
