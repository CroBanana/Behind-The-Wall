using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment2 : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rig;

    //kretanje ljevo desno naprijed nazad
    public float vertical;
    public float horizontal;

    //limitiranje koja se animacija kada izvrsava
    public float runlimitMAX;
    public float runlimitMIN;
    public float runSpeed;
    public float accelerationSpeed;

    //rotacija
    private Vector3 rotation;
    public float rotateSpeed;

    //ostalo
    public Transform alwaysFacingPoint;
    public GameObject rotatePLayerFirstPerson;

    //provjera je li player u prvom licu
    public bool firstPerson;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        alwaysFacingPoint = GameObject.Find("facingTowards").transform;
        rotatePLayerFirstPerson = GameObject.Find("CameraObject");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        HandleInputs();
        SetAnimations();
        LookDirection();
    }

    // handles inputs for movment and camera
    void HandleInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // if shift is pressed can run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            runlimitMAX = 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)){
            runlimitMAX = 1f;
        }

        // sets camera to first person or third person
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SetCamera();
        }
    }

    // since it uses root animations the speed of the player depends on the animation speed
    void SetAnimations()
    {
        if (vertical != 0 || horizontal != 0)
        {
            if (runSpeed > runlimitMAX)
            {
                runSpeed = Mathf.Clamp(runSpeed -= accelerationSpeed * Time.deltaTime, runlimitMIN, 2);
            }
            else
            {
                runSpeed = Mathf.Clamp(runSpeed += accelerationSpeed * Time.deltaTime, runlimitMIN, runlimitMAX);
            }
        }
        else
        {
            runSpeed = Mathf.Clamp(runSpeed -= accelerationSpeed * Time.deltaTime, runlimitMIN, runlimitMAX);
        }

        if (vertical < 0)
        {
            anim.SetFloat("Speed", -(runSpeed*1.5f));
        }
        else
        {
            anim.SetFloat("Speed", runSpeed);
        }
        //Debug.Log(runSpeed);
    }

    void LookDirection()
    {
        rotation = alwaysFacingPoint.transform.rotation.eulerAngles;
        if (!firstPerson)
        {
            if (vertical != 0)
            {
                rotation.x = 0;
                rotation.z = 0;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, alwaysFacingPoint.rotation, Time.deltaTime * rotateSpeed);
            }
            if (horizontal != 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                              new Quaternion(alwaysFacingPoint.rotation.x,
                                                              alwaysFacingPoint.rotation.y,
                                                              alwaysFacingPoint.rotation.z,
                                                              alwaysFacingPoint.rotation.w),
                                                              Time.deltaTime * rotateSpeed);
            }
        }
        else
        {
            Vector3 angle = rotatePLayerFirstPerson.transform.eulerAngles;
            int rotAmount = 0;
            if(vertical!=0 && horizontal == 0)
            {
                rotAmount = 0;
            }
            else if(horizontal != 0 && vertical == 0)
            {
                rotAmount = 90 * horizontal.CompareTo(0);
            }
            else if(vertical>0 && horizontal > 0)
            {
                rotAmount = 45;
            }
            else if(vertical<0 && horizontal < 0)
            {
                rotAmount = 45;
            }
            else if (vertical > 0)
            {
                rotAmount = -45;
            }
            else if(vertical < 0)
            {
                rotAmount = -45;
            }
            Vector3 rotateTo = new Vector3(transform.rotation.x,
                           angle.y + rotAmount,
                           transform.rotation.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                         Quaternion.Euler(rotateTo.x, rotateTo.y, rotateTo.z),
                                                         Time.deltaTime * rotateSpeed);

        }

    }
}
