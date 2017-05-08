using UnityEngine;

namespace Assets.Scripts
{
    public class RandomRotator : MonoBehaviour
    {
        private Rigidbody _body;

        public float Tumble;

        void Start ()
        {
            _body = GetComponent<Rigidbody>();
            _body.angularVelocity = Random.insideUnitSphere * Tumble;
        }
    }
}
