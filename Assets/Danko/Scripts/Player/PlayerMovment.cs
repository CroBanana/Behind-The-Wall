using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{

    float horizontal, vertical;
    public float mouseSensetivity;
    private Animator anim;
    public Transform alwaysFacingPoint;
    public Vector3 rotation;
    public float rotateSpeed;
    public bool isGrounded = false;
    private Rigidbody rig;
    private float runSpeed = 0;
    private bool firstPerson;
    private GameObject cameraObject;
    private Camera camera;
    public float zDistanceFromPlayer = -1f;
    public GameObject interactCanvas;
    public GameObject lockCanvas;
    public GameObject characterDialog;
    public bool isDoingSometing = false;
    public bool canInteract = false;
    public bool ePressed = false;
    public GameObject focusedObject;
    public GameObject rotateViaMouse;
    public float distanceFromObject;
    public LayerMask cameraLayers, cameraLayersOriginal;
    private Vector3 camPositionOriginal;
    private bool reset = false;
    public Transform parent;
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
        camPositionOriginal = camera.transform.localPosition;
        parent = cameraObject.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();

        if (ePressed == false)
        {
            camera.cullingMask = cameraLayersOriginal;
            if (reset)
            {
                ResetCameraPosition();
                reset = false;
            }
            
            rotateViaMouse.GetComponent<RotateViaMouse>().enabled = true;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            SetAnimations();
            LookDirection();
        }
        else
        {
            FocusOnAnObject();
         
            
            reset = true;
        }

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

    void HandleInputs()
    {
        if (ePressed == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSpeed = 1f;
            }
            else runSpeed = 0f;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetCamera();
            }
        }
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            ePressed = !ePressed;
            
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

    void SetAnimations()
    {
        if (vertical < 0)
        {
            anim.SetFloat("Speed", vertical * 1.5f - (runSpeed / 2));
        }
        else if (vertical == 1 || Mathf.Abs(horizontal) == 1)
        {
            anim.SetFloat("Speed", 1f + runSpeed);
        }
        else if (Mathf.Abs(horizontal) > 0)
        {
            anim.SetFloat("Speed", Mathf.Abs(horizontal) + runSpeed);
        }
        else if (vertical >= 0)
        {
            anim.SetFloat("Speed", vertical + runSpeed);
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
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, alwaysFacingPoint.rotation, Time.deltaTime * rotateSpeed);

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
