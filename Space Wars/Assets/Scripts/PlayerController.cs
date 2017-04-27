using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Bounds
    {
        public float XMin, XMax;
        public float ZMin, ZMax;
    }

    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _body;

        public Bounds GameBounds;
        public float Speed;
        public float Tilt;

        void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            _body.velocity = movement * Speed;

            _body.position = new Vector3(
                Mathf.Clamp(_body.position.x, GameBounds.XMin, GameBounds.XMax),
                0,
                Mathf.Clamp(_body.position.z, GameBounds.ZMin, GameBounds.ZMax));
            
            _body.rotation = Quaternion.Euler(_body.velocity.z > 0 ? _body.velocity.z * Tilt / 2 : 0, 0, _body.velocity.x * -Tilt);
        }
    }
}
