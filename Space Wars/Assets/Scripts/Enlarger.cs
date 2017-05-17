using UnityEngine;

namespace Assets.Scripts
{
    public class Enlarger : MonoBehaviour
    {
        private SphereCollider collider;
        public float enlargeFactor;

        void Start()
        {
            collider = GetComponent<SphereCollider>();
        }

        void FixedUpdate () {
            if (collider.radius < 20)
            {
                collider.radius += enlargeFactor;
            }
        }
    }
}
