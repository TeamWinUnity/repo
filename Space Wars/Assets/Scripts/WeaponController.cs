using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject shot;
        public Transform shotSpawn;
        public float fireRate;
        public float delay;

        private AudioSource audioSourse;
    
        void Start ()
        {
            audioSourse = GetComponent <AudioSource > ();
            InvokeRepeating("Fire", delay, fireRate);
        }
	
        void Fire()
        {
            Quaternion Rotation = new Quaternion(0, 180, 0, 0);
            Instantiate(shot, shotSpawn.position,Rotation);
            audioSourse.Play();
        } 
    }
}
