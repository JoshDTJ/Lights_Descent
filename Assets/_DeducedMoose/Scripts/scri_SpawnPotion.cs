using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri_SpawnPotion : MonoBehaviour
{
    public GameObject potion;
    public float waitTime = 15f;
    public List<GameObject> _potions;
    // Start is called before the first frame update
    private void Start()
    {
        //Spawn new potion
        InvokeRepeating("SpawnPotion", 0f , waitTime);
    }

    private void Update()
    {
        
    }
    //Spawn potion if this spawn point has no potions in it's list
    void SpawnPotion()
    {
        if (!_potions.Contains(potion))
        {
            
            Instantiate(potion, transform.position, Quaternion.identity);
            //Add potion to list
            _potions.Add(potion);
        }
    }
}
