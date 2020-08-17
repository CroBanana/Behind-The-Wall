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
    public Canvas_interact canvas_Interact;

    //components
    public Animator anim;
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
    public bool talkTriggerd = false;
    public bool disableRaycast = false;

    // raycast detection
    RaycastHit hit;
    Ray ray;
    public LayerMask selected_mask;

    GameObject test ;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        camera = Camera.main;
        cameraObject = GameObject.Find("Main Camera");
        camera.cullingMask = cameraLayersOriginal;
        canvas_Interact= GameObject.Find("Canvas").GetComponent<Canvas_interact>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfLokingAtObject();
        InteractAndMove();
    }

    // uses ray to check if the player is looking at an objet that cen be interacted with then changes the canvas
    void CheckIfLokingAtObject(){
        if(disableRaycast==false){
                    //position of mouse on screen
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit,5,selected_mask)){
                if(hit.collider.CompareTag("Lock") && ePressed==false){
                    hit.collider.GetComponentInChildren<LockNumbers>().enabled = true;
                    canInteract = true;
                    focusedObject = hit.collider.gameObject;
                    //canvas_Interact.Set_Canvas(true,false,false);
                    Debug.Log(hit.collider.name);
                }
                else if(hit.collider.CompareTag("NPC") && ePressed==false){
                    canInteract=true;
                    focusedObject = hit.collider.gameObject;
                    //canvas_Interact.Set_Canvas(true,false,false);
                    Debug.Log(hit.collider.name);
                }
                else if(hit.collider.CompareTag("Puzzle") && ePressed==false){
                    hit.collider.GetComponentInChildren<Puzzle>().enabled = true;
                    canInteract = true;
                    focusedObject = hit.collider.gameObject;
                    //canvas_Interact.Set_Canvas(true,false,false);
                    Debug.Log(hit.collider.name);
                }
            }
            else if(focusedObject !=null && ePressed==false){
                //canvas_Interact.Set_Canvas(false,false,false);
                if(focusedObject.CompareTag("Lock")){
                    Debug.Log("am here!!!");
                    focusedObject.GetComponentInChildren<LockNumbers>().enabled=false;
                    focusedObject=null;
                }
                else if(focusedObject.CompareTag("Puzzle")){
                    focusedObject.GetComponentInChildren<Puzzle>().enabled=false;
                    focusedObject=null;
                }
                Debug.Log("Nema canvasa valjda");
            }
        }
    }

    // checks interaction and movment when e is pressed or not changes canInteract and then changes everything else
    void InteractAndMove()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            ePressed = !ePressed;
            if(ePressed){
                if(focusedObject.CompareTag("Lock")){
                    //canvas_Interact.Set_Canvas(false,true,false);
                }else if(focusedObject.CompareTag("NPC")){
                    talkTriggerd = true;
                }else if(focusedObject.CompareTag("Puzzle")){
                    //canvas_Interact.Set_Canvas(false,true,false);
                }
            }
        }

        // check if E is pressed if it is disables movment of player and sets camera on the object that was pressed E
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

    // handles inputs for movment and camera
    void HandleInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (ePressed == false)
        {
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
                SetCamera();
            }
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

    // if an conversation has ended sets everything back to normal
    public void EndConversation()
    {
        focusedObject.GetComponent<EnemyMovment>().isTalking = false;
        focusedObject.GetComponent<EnemyMovment>().needsPlayer = false;
        ePressed = false;
        disableRaycast=false;
    }

    // depending on the object that the player is loking at sets the camera in a position that is looking at that object
    void FocusOnAnObject()
    {
        anim.SetFloat("Speed", 0);
        camera.cullingMask = cameraLayers;
        rotateViaMouse.GetComponent<RotateViaMouse>().enabled = false;

        if (focusedObject.CompareTag("Lock") || focusedObject.CompareTag("Puzzle"))
        {
            camera.transform.position = focusedObject.transform.position - focusedObject.transform.right * distanceFromObject;

            cameraObject.transform.LookAt(focusedObject.transform);
            try{
                if(focusedObject.GetComponentInChildren<LockNumbers>().unlocked){
                    Debug.Log("HERE!!!!");
                    canvas_Interact.Set_Canvas(false, true, false, false,false,false,false);
                    reset=true;
                    ePressed=false;
                    focusedObject=null;
                }
            }catch{
                Debug.Log("works");
            }
        }
        else if(focusedObject.CompareTag("NPC"))
        {
            Transform head = focusedObject.transform.Find("Head");
            camera.transform.position = head.position + head.forward * distanceFromObject;
            cameraObject.transform.LookAt(head);
            if(talkTriggerd){
                focusedObject.GetComponent<EnemyMovment>().isTalking = true;
                talkTriggerd=false;
                
            }
        }


    }

    // resets the camera so it looks at the player depending if the player is in first person or third
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

    // sets camera when R is pressed
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

    // rotates the object to look at the direction it sould move
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





/*

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("am here!!!");
        if (other.CompareTag("Lock") || other.CompareTag("NPC"))
        {
            focusedObject = other.gameObject;
            canvas_Interact.Set_Canvas(true,false,false);
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
                canvas_Interact.Set_Canvas(false,true,false);
            }
            else
            {
                canvas_Interact.Set_Canvas(true,false,false);
                other.GetComponentInChildren<LockNumbers>().enabled = false;
            }
        }
        if (other.CompareTag("NPC"))
        {
            if (ePressed)
            {
                canvas_Interact.Set_Canvas(false,false,true);
            }
            else
            {
                canvas_Interact.Set_Canvas(true,false,false);
            }
        }

    }


    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Lock") || other.CompareTag("NPC"))
        {
            focusedObject = null;
            canvas_Interact.Set_Canvas(false,false,false);
            canInteract = false;
        }
        //Debug.Log("am here!!!");
    }
*/
}
