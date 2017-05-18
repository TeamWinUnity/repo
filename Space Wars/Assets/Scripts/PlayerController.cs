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
        public GameController controller;
        private float currentSpeed = 4;
        private Rigidbody _body;
        private float _nextFire;


        public Bounds GameBounds;
        public float Speed;
        public float Tilt;

        public GameObject Shot;
        public Transform ShotsSpawn;

        public float FireRate;
        public bool EnableControl { get; set; }

        void Start()
        {
            _body = GetComponent<Rigidbody>();
            EnableControl = false;
        }

        void Update()
        {
            if (!EnableControl) return;
            
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + FireRate;
                Instantiate(Shot, ShotsSpawn.position, Quaternion.identity);
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
            }
        }

        void FixedUpdate()
        {
            if (!controller.Started) return;
            
            float horizontal, vertical;
            if (EnableControl)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(horizontal, 0, vertical);
                _body.velocity = movement * currentSpeed;

                _body.position = new Vector3(
                    Mathf.Clamp(_body.position.x, GameBounds.XMin, GameBounds.XMax),
                    0,
                    Mathf.Clamp(_body.position.z, GameBounds.ZMin, GameBounds.ZMax));

                _body.rotation = Quaternion.Euler(_body.velocity.z > 0 ? _body.velocity.z * Tilt / 2 : 0, 0, _body.velocity.x * -Tilt);
            }
            else
            {
                horizontal = 0;
                vertical = 1;
                if (transform.position.z >= -5)
                {
                    controller.AddScore(0);
                    currentSpeed = Speed;
                    EnableControl = true;
                }

                Vector3 movement = new Vector3(horizontal, 0, vertical);

                _body.velocity = movement * currentSpeed;
                _body.rotation = Quaternion.Euler(_body.velocity.z > 0 ? _body.velocity.z * Tilt / 2 : 0, 0, _body.velocity.x * -Tilt);
            }
        }
    }
}
