using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {
    private Vector3 startPosition;
    public float scrollSpeed;
    public float tileSizeZ;

    public Material[] materials;
    public GameObject part1;
    public GameObject part2;

    private Queue<int> order;
    private int background;
    private bool onChange;

    private float onChangeSetValue;
    private float onMaterialSet;

    void Start () {
        order = new Queue<int>(6);
        startPosition = transform.position;
        onChangeSetValue = tileSizeZ / 5;
        onMaterialSet = tileSizeZ * 0.8f;

        ChangeState();
        ChangeState();
    }


    void Update () {
        var newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
	    transform.position = startPosition + Vector3.forward * newPosition;

        if (transform.position.z >= onMaterialSet && onChange)
        {
            ChangeBackground();
            onChange = false;
        }
        if (transform.position.z <= onChangeSetValue)
        {
            onChange = true;
        }
    }

    private void ChangeBackground()
    {
        if (order.Count < 2) return;
        int m1 = order.Dequeue(), m2 = order.Dequeue();

        part1.GetComponent<MeshRenderer>().material = materials[m1];
        part2.GetComponent<MeshRenderer>().material = materials[m2];
    }

    public void ChangeState()
    {
        if(background + 2 >= materials.Length) return;
        order.Enqueue(background+1);
        order.Enqueue(background+0);
        order.Enqueue(background + 2);
        order.Enqueue(background + 1);
        order.Enqueue(background + 2);
        order.Enqueue(background + 2);
        background += 2;
    }
}
