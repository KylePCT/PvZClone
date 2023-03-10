using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SuperBasicZombieSpawner : MonoBehaviour
{
    private PlaceObjOnGrid gridGenerator;

    public Zombie zombiePrefab;
    public int maxZombieCount;
    [HideInInspector] public int zombieCounter;
    public float delayBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        gridGenerator = GetComponent<PlaceObjOnGrid>();
        StartCoroutine("spawnZombo");
        zombieCounter = 0;
    }

    IEnumerator spawnZombo()
    {
        while (zombieCounter < maxZombieCount)
        {
            yield return new WaitForSeconds(delayBetweenSpawns);

            Zombie newZombie = Instantiate(zombiePrefab, transform, false);
            newZombie.transform.position = new Vector3(gridGenerator.gridWidth + 5, 0.5f, Random.Range(0, gridGenerator.gridHeight));
            zombieCounter++;
        }
    }
}
