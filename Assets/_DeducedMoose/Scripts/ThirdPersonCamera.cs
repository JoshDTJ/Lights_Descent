using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float disFromTarget = 5;
    public float zoomDist = 2.5f;
    public float zoomSpeed = 5;
    public Vector2 pitchMinMax = new Vector2(-15, 40);
    public float rotationSmoothing = .15f;
    Vector3 rotationSmoothTime;
    Vector3 currentRotation;

    public bool zoomFocused = false;

    private float startTime;
    private float zoomLength;
    float yaw;
    float pitch;

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

    void Zoom()
    {
        if (state == State.Normal)
        {
            state += 1;
        }
        else
        {
            state -= 1;
        }

        Debug.Log("this working?");
        switch (state)
        {
            case State.Normal:
                Debug.Log("normal");
                break;
            case State.Zoom:
                Debug.Log("zoom");
                break;
        }
    }
    private void Update()
    {
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

            LayerMask mask = LayerMask.GetMask("FocusArea");

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, mask))
            {
                zoomFocused = true;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        }
    }
}