using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    //objects
    public GameObject cameraObject;
    public Camera camera;
    public Transform alwaysFacingPoint;
    public GameObject focusedObject;
    public GameObject rotateViaMouse;
    public GameObject rotatePLayerFirstPerson;
        //UI canvases
    private GameObject interactCanvas;
    private GameObject lockCanvas;
    private GameObject characterDialog;

    //components
    private Animator anim;
    private Rigidbody rig;


    //Movment and rotation variables
    float horizontal, vertical;
    public float mouseSensetivity;
    public float rotateSpeed;
    private Vector3 rotation;
    public float runSpeed = 0;
    public float runlimitMAX, runlimitMIN;
    public float accelerationSpeed;
    public bool runBackwards=false;


    //camera variables
    private bool firstPerson;
    public float zDistanceFromPlayer = -1f;
    public LayerMask cameraLayers, cameraLayersOriginal;
    public float distanceFromObject;


    //bools lol
    public bool canInteract = false;
    public bool ePressed = false;
    private bool reset = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        camera = Camera.main;
        cameraObject = GameObject.Find("Main Camera");
        interactCanvas = GameObject.Find("InteractCanvas");
        interactCanvas.SetActive(false);
        lockCanvas = GameObject.Find("LockCanvas");
        lockCanvas.SetActive(false);
        characterDialog = GameObject.Find("CharacterDialoge");
        characterDialog.SetActive(false);
        camera.cullingMask = cameraLayersOriginal;
    }

    // Update is called once per frame
    void Update()
    {
        InteractButton();
        

        if (ePressed == false)
        {
            HandleInputs();
            camera.cullingMask = cameraLayersOriginal;
            if (reset)
            {
                ResetCameraPosition();
                reset = false;
            }
            
            rotateViaMouse.GetComponent<RotateViaMouse>().enabled = true;
            
            SetAnimations();
            LookDirection();
        }
        else
        {
            FocusOnAnObject();
         
            
            reset = true;
        }

    }

    void InteractButton()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            ePressed = !ePressed;

        }
    }

    void HandleInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (ePressed == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                runlimitMAX = 2f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)){
                runlimitMAX = 1f;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetCamera();
            }
        }
    }

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

    public void EndConversation()
    {
        focusedObject.GetComponent<EnemyMovment>().isTalking = false;
        ePressed = false;
    }

    void FocusOnAnObject()
    {
        anim.SetFloat("Speed", 0);
        camera.cullingMask = cameraLayers;
        rotateViaMouse.GetComponent<RotateViaMouse>().enabled = false;

        if (focusedObject.CompareTag("Lock"))
        {
            camera.transform.position = focusedObject.transform.position - focusedObject.transform.right * distanceFromObject;

            cameraObject.transform.LookAt(focusedObject.transform);
        }
        else if(focusedObject.CompareTag("NPC"))
        {
            Transform head = focusedObject.transform.Find("Head");
            camera.transform.position = head.position + head.forward * distanceFromObject;

            cameraObject.transform.LookAt(head);
            focusedObject.GetComponent<EnemyMovment>().isTalking = true;
        }

        

    }

    void ResetCameraPosition()
    {
        camera.transform.localPosition = new Vector3(0, 0, 0);
        camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        if (!firstPerson)
        {
            cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y, zDistanceFromPlayer);
        }
        else
        {
            cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y, 0f);
        }
    }

    

    void SetCamera()
    {
            if (firstPerson)
            {
                firstPerson = false;
                cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y, zDistanceFromPlayer);
                firstPerson = false;
            }
            else
            {
                firstPerson = true;
                cameraObject.transform.localPosition = new Vector3(cameraObject.transform.localPosition.x, cameraObject.transform.localPosition.y, 0f);
                firstPerson = true;
            }
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

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("am here!!!");
        if (other.CompareTag("Lock") || other.CompareTag("NPC"))
        {
            focusedObject = other.gameObject;
            interactCanvas.SetActive(true);
            canInteract = true;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lock"))
        {
            if (ePressed)
            {
                other.GetComponentInChildren<LockNumbers>().enabled = true;
                interactCanvas.SetActive(false);
                lockCanvas.SetActive(true);
            }
            else
            {
                interactCanvas.SetActive(true);
                lockCanvas.SetActive(false);
                other.GetComponentInChildren<LockNumbers>().enabled = false;
            }
        }
        if (other.CompareTag("NPC"))
        {
            if (ePressed)
            {
                interactCanvas.SetActive(false);
            }
            else
            {
                interactCanvas.SetActive(true);
            }
        }

        
    }


    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Lock") || other.CompareTag("NPC"))
        {
            focusedObject = null;
            interactCanvas.SetActive(false);
            lockCanvas.SetActive(false);
            canInteract = false;
        }
        //Debug.Log("am here!!!");
    }

}
