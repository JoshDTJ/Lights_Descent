using UnityEngine;

namespace Invector
{
    public class vHealthItem : MonoBehaviour
    {
        [Tooltip("How much health will be recovery")]
        public float value;
        public string tagFilter = "Player";
        public GameObject gameController;

        void Start()
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");

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
                        healthController.AddHealth((int)value);
                        gameController.GetComponent<scri_GameController>().HealthUp();
                        Destroy(transform.parent.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}