using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scri_CameraRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    public Material highlightMaterial;
    public Material defaultMaterial;
    public GameObject popUp;
    public GameObject player;

    public float sphereRadius = 0.5f;
    public float maxDistance = 10f;

    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    private Text text;
    private Transform _selection;

    public LayerMask layerMask;

    private void Start()
    {
        //Disable popup text on start
        text = popUp.GetComponent<Text>();
        text.enabled = false;
    }
    private void FixedUpdate()
    {
        //Defining the values for the vectors fopr the sphere cast
        origin = transform.position;
        direction = transform.forward;
    
        
        if(_selection != null)
        {
            //While th sphere cast is not over a selectable object
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            text.enabled = false;
            _selection = null;
        }
        //Defining the sphere cast
        var ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f,0.5f,0f));
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            var selection = hit.transform;
            //Checking the tag of the game object the sphere cast is colliding with
            if (selection.CompareTag("Weapon"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if(selectionRenderer != null)
                {
                    //Swapping the gameobject that the sphere is colliding with, and enabling the UI Text
                    selectionRenderer.material = highlightMaterial;
                    text.enabled = true;
                    //Press 'E' to equip the selected weapon
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Call the PickUp function from the selected object in the 'Player PickUp' Script
                        selection.gameObject.SendMessage("PickUp");
                    }
                }
                _selection = selection;
            }
        }
        else
        {
            currentHitDistance = maxDistance;
        }
    }
    //Drawing the sphere cast to see with Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
