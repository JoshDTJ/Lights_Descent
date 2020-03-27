using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRaycast : MonoBehaviour
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
        text = popUp.GetComponent<Text>();
        text.enabled = false;
    }
    private void FixedUpdate()
    {
        origin = transform.position;
        direction = transform.forward;
    
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            text.enabled = false;
            _selection = null;
        }
        var ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f,0.5f,0f));
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Weapon"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if(selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                    text.enabled = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
