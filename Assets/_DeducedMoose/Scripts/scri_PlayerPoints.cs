using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scri_PlayerPoints : MonoBehaviour
{
    public float points = 0;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = points.ToString();
    }

    void AddPoint()
    {
        points += 1;
        
    }
}
