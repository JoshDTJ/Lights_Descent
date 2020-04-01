using UnityEngine;

namespace Invector
{
    public class scri_BackupHealth : MonoBehaviour
    {
        public float closeDistance = 2;
        public GameObject closestSpawn;
        [Tooltip("How much health will be recovery")]
        public float value;
        public string tagFilter = "Player";
        scri_SpawnPotion potionScript;

        public void Update()
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
            foreach(GameObject point in spawnPoints)
            {
                if(Vector3.Distance(transform.position, point.transform.position) <= closeDistance)
                {
                    closestSpawn = point.gameObject;
                    potionScript = closestSpawn.GetComponent<scri_SpawnPotion>();
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagFilter))
            {
                // access the basic character information
                var healthController = other.GetComponent<vHealthController>();
                if (healthController != null)
                {

                    // heal only if the character's health isn't full
                    if (healthController.currentHealth < healthController.maxHealth)
                    {
                        // limit healing to the max health
                        potionScript._potions.Clear();
                        healthController.ChangeHealth((int)value);
                        Destroy(gameObject);
                        Destroy(transform.parent.gameObject);
                    }                    
                }
            }
        }
    }
}