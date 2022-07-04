using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // variables
    public GameObject[] powerups;

    // Coordanates
    private float maxX = -20f;
    private float minX = 20f;
    private float minZ = -20f;
    private float maxZ = 20f;
    private float spawnY = 5f;

    // Time
    private float minTime = 10f;
    private float maxTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Spawning the power ups
        StartCoroutine(spawnTimer());
    }


    // Getting random Coordanates
    Vector3 makeRandomCoordanates()
    {
        Vector3 newC = new Vector3(Random.Range(minX, maxX), spawnY, Random.Range(minZ, maxZ));
        return newC;
    }

    // Making a spawn timer for the powerups
    IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        
        // Getting a random power up from the array
        GameObject currentPowerup = powerups[Random.Range(0, powerups.Length)];

        // Instantiating a new power up
        Instantiate(currentPowerup, makeRandomCoordanates(), currentPowerup.transform.rotation);
    }
}
