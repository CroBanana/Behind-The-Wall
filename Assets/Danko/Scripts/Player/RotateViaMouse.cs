using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateViaMouse : MonoBehaviour
{
    public float mouseSensetivity;

    public Transform alwaysFacing;
    public Transform camera;
    Vector3 cameraRotation;

    public float xAxis=0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraRotation = camera.transform.rotation.eulerAngles;
        RotateCamera();
        RotateAlwaysFacingDirection();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotAmountX = mouseX * mouseSensetivity * Time.deltaTime; 
        float rotAmountY = mouseY * mouseSensetivity * Time.deltaTime; 
        
        xAxis -= rotAmountY;
        
        cameraRotation.x -= rotAmountY;
        cameraRotation.y +=rotAmountX;
        cameraRotation.z = 0;
        camera.rotation = Quaternion.Euler(cameraRotation);
    }
    void RotateAlwaysFacingDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (vertical == 0)
        {
            cameraRotation.y += horizontal * 2 * 45;
        }else
        {
            cameraRotation.y += horizontal * 45;
        }

        if (vertical < 0)
        {
            cameraRotation.y += -horizontal * 2 * 45;
        }
        cameraRotation.x = 0;
        alwaysFacing.rotation = Quaternion.Euler(cameraRotation);
        Debug.DrawRay(alwaysFacing.position, Vector3.forward, Color.blue);
    }
}
