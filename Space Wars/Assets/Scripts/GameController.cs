using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[]  hazards;
    public Vector3 spawnValues; 

	void Start () {
        SpawnWaves();

	}



    void SpawnWaves()
    {


        GameObject hazard = hazards[Random.Range(0, hazards.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z );
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(hazard, spawnPosition, spawnRotation);


    }
}
