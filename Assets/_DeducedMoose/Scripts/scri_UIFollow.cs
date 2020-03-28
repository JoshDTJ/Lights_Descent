using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri_UIFollow : MonoBehaviour
{
    public Camera mainCamera;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.down);
    }
}
