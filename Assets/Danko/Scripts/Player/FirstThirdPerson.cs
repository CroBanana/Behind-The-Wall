using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstThirdPerson : MonoBehaviour
{
    Transform camera;
    float z;
    bool firstPerson = false;
    PlayerMovment changeFirstPerson;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.GetChild(0);
        z = camera.localPosition.z;
        changeFirstPerson = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (firstPerson)
            {
                firstPerson = false;
                camera.localPosition = new Vector3(camera.localPosition.x,camera.localPosition.y,z);
                //changeFirstPerson.firstPerson = false;
            }
            else
            {
                firstPerson = true;
                camera.localPosition = new Vector3(camera.localPosition.x, camera.localPosition.y, 0);
                //changeFirstPerson.firstPerson = true;
            }
        }
        
    }
}
