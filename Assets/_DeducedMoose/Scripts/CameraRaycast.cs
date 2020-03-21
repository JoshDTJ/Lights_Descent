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
    private Text text;
    private Transform _selection;

    private void Start()
    {
        text = popUp.GetComponent<Text>();
        text.enabled = false;
    }
    private void FixedUpdate()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            text.enabled = false;
            _selection = null;
        }
        var ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f,0.5f,0f));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        if(Physics.Raycast(ray,out hit))
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
                        player.gameObject.SendMessage("PickUp");
                    }
                }
                _selection = selection;
            }
        }
    }
}
