using Unity.VisualScripting;
using UnityEngine;

namespace Archery
{
    public class Arrow : MonoBehaviour
    {
        public float lifetime = 5f; // Arrow lifetime before it is deactivated

        private void OnEnable()
        {
            // Schedule deactivation after a lifetime
            Invoke("DeactivateArrow", lifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Deactivate the arrow immediately upon collision
            DeactivateArrow();
        }

        void DeactivateArrow()
        {
            // Disable the arrow and cancel any remaining Invoke calls
            gameObject.SetActive(false);
            CancelInvoke();
        }

        void OnDisable()
        {
            CancelInvoke("DeactivateArrow");
        }
    }
}