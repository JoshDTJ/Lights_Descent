using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// a third person camera that focuses on a target, can "zoom-in" 
// on that target and spits out a raycast that can detect a layer mask

public class ThirdPersonCamera : MonoBehaviour
{
    //these settings where the camera is focused one 
    //tip: focus on a game object parented to the palyer
    [Header("Camera Target Settings")]
    public Transform target;
    public float disFromTarget = 5;

    //this is for focusing the camera, a ray cast spits out but this can all be disabled with 2 bool checks
    [Header("Camera Zoom Settings")]
    public bool ZoomEnabled = true; //enables or disables zooming
    public bool zoomRayCastOn; //enables or disables the raycast when zooming
    public string zoomRayCastMask = "FocusArea"; //the mask the ray cast is looking for
    public bool zoomFocused = false;
    public float zoomDist = 2.5f;
    public float zoomSpeed = 5;

    //these control how sensitive the camera is and how much it can look up or down
    [Header("Player Mouse Settings")]
    public float mouseSensitivity = 10;
    public float rotationSmoothing = .15f;
    public Vector2 pitchMinMax = new Vector2(-15, 40);


    Vector3 rotationSmoothTime;
    Vector3 currentRotation;


    float yaw;
    float pitch;

    private float startTime;
    private float zoomLength;

    //these are the states for camera zooming
    enum State
    {
        Normal,
        Zoom,
    }

    private State state;

    private void Start()
    {
        state = State.Normal;

        zoomLength = Vector3.Distance(target.position - transform.forward * disFromTarget, target.position - transform.forward * zoomDist);
    }

    //this zooms or un-zooms the camera
    void Zoom()
    {
        if (ZoomEnabled == true)
        {
            if (state == State.Normal)
            {
                state += 1;
            }
            else
            {
                state -= 1;
            }
        }

        //Debug.Log("this working?");
        switch (state)
        {
            case State.Normal:
                //Debug.Log("normal");
                break;
            case State.Zoom:
                //Debug.Log("zoom");
                break;
        }
    }
    private void Update()
    {
        //this is where zoom is activated
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Zoom();
            startTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Zoom();
        }
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothTime, rotationSmoothing);
        transform.eulerAngles = currentRotation;

        if (state == State.Normal)
        {
            transform.position = target.position - transform.forward * disFromTarget;
            zoomFocused = false;
        }
        else if (state == State.Zoom)
        {
            float distCovered = (Time.time - startTime) * zoomSpeed;
            float fractOfJourney = distCovered / zoomLength;

            transform.position = Vector3.Lerp(target.position - transform.forward * disFromTarget, target.position - transform.forward * zoomDist, fractOfJourney);

            if (zoomRayCastOn == true)
            {
                //change this layer mask in the inspector if you want the raycast to look for a certin object
                LayerMask mask = LayerMask.GetMask(zoomRayCastMask);

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, mask))
                {
                    zoomFocused = true;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
        }
    }
}