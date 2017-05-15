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
        private float _nextFire;

        public Bounds GameBounds;
        public float Speed;
        public float Tilt;

        public GameObject Shot;
        public Transform ShotsSpawn;

        public float FireRate;

        void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        void Update()
        {
            bool firePressed = Input.GetKey(KeyCode.Space);

            if (Time.time > _nextFire && firePressed)
            {
                _nextFire = Time.time + FireRate;
                Instantiate(Shot, ShotsSpawn.position, Quaternion.identity);
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
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
