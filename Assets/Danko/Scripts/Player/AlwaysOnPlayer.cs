using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = new Vector3(player.transform.position.x,
                                       player.transform.position.y + 1.7f,
                                       player.transform.position.z);
        transform.position = position;
    }
}
