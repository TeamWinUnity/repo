using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyByBoundary : MonoBehaviour {
        void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
