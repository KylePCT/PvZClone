using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Plant_SunProducing : Plant
{
    [Header("Sun-Producing")]
    public Sun plantSun;
    [Space(10)]
    public float generationInterval;
    [Space(10)]
    public float animationForceAmount;

    public bool canGenerate;

    void Start()
    {
 
    }

    IEnumerator generateSun()
    {
        while (canGenerate)
        {
            Debug.Log("Spawning Sun.");

            //Every X seconds, spawn the sun.
            Sun sun = Instantiate(plantSun, transform, true);
            sun.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            //Add force to send it flying in a random direction at spawntime.
            sun.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * (animationForceAmount * 2));

            //Wait for seconds.
            yield return new WaitForSeconds(generationInterval);
        }

        while (!canGenerate)
        {
            yield return null;
        }
    }
}
