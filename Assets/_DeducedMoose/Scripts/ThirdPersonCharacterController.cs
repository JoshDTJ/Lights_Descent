using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// a basic third person character controller that move relative to the camera and can jump
// gameObject MUST have a rigidBody and freeze X and Z rotation
public class ThirdPersonCharacterController : MonoBehaviour
{
    [Header("Camera Relation")]
    public Transform myCamera;

    [Header("Movement Variables")]
    public float moveAcceleration = 10;
    public float maxMoveSpeed = 10;
    public float turnSpeed = 7.5f;
    public float jumpForce = 250;

    [Header("Grounded Debug")]
    public bool grounded;

    Rigidbody player;
    float distToGround = 1.05f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = (myCamera.right * Input.GetAxis("Horizontal")) + (myCamera.forward * Input.GetAxis("Vertical"));
        dir.y = 0;

        //change or add to these layer masks if you have more than a "ground" layer for the player to walk on
        LayerMask mask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), distToGround, mask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
            player.AddForce(transform.forward * moveAcceleration);
        }
        else if (grounded)
        {
            player.velocity = Vector3.zero * Time.deltaTime;
        }

        if (player.velocity.magnitude > maxMoveSpeed)
        {
            player.velocity = Vector3.ClampMagnitude(player.velocity, maxMoveSpeed);
        }


        if (Input.GetButton("Jump") && grounded)
        {
            player.AddForce(0, jumpForce, 0);
        }

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * distToGround, Color.red);  //grounded debug
    }
}
