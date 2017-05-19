using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpown;
    public float fireRate;
    public float delay;


    private AudioSource audioSourse;

	// Use this for initialization
	void Start () {
        audioSourse = GetComponent < AudioSource > ();
        InvokeRepeating("Fire", delay, fireRate);
	}
	
	// Update is called once per frame
	void Fire()
    {
        Quaternion Rotation = new Quaternion(0, 180, 0, 0);
        Instantiate(shot, shotSpown.position,Rotation);
        audioSourse.Play();
    } 
}
