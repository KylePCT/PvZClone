using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SceneController : MonoBehaviour
{
    //Init.
    private PlaceObjOnGrid gridController;

    [Header("Sun Generation")]
    public Sun plantSun;
    [Space(10)]
    public float naturalSunGenInterval;
    private bool canGenerate;

    // Start is called before the first frame update
    void Start()
    {
        gridController = PlaceObjOnGrid.instance;
        canGenerate = true;

        StartCoroutine("generateSun");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator generateSun()
    {
        while (canGenerate)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(0, gridController.gridWidth), 10f, Random.Range(0, gridController.gridHeight));
            //Every X seconds, spawn the sun.
            Sun sun = Instantiate(plantSun, randomSpawnPos, Quaternion.identity);
            Debug.Log("Spawning Natural Sun at " + randomSpawnPos);

            //Wait for seconds.
            yield return new WaitForSeconds(naturalSunGenInterval);
        }

        while (!canGenerate)
        {
            yield return null;
        }
    }
}
