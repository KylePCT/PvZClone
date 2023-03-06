using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class WaveController : MonoBehaviour
{
    private PlaceObjOnGrid gridGenerator;

    private bool waveIsActive;

    public Wave currentWave;

    private WaveSegment[] segments;

    // Start is called before the first frame update
    void Start()
    {
        gridGenerator = PlaceObjOnGrid.instance;

        StartWave(currentWave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave(Wave waveToStart)
    {
        segments = waveToStart.waveSegments;

        waveIsActive = true;

        StartCoroutine("GenerateWaves", segments);
    }

    IEnumerator GenerateWaves(WaveSegment[] segments)
    {
        while (waveIsActive)
        {
            for (int i = 0; i < segments.Length; ++i)
            {
                Zombie[] segmentZombies = segments[i].zombiesInSegment;

                foreach (Zombie zombie in segmentZombies)
                {
                    Zombie zombieSpawned = Instantiate(zombie, transform, true);
                    zombieSpawned.transform.parent = transform;
                    zombieSpawned.transform.position = new Vector3(gridGenerator.gridWidth + 3, 0.5f, Random.Range(0, gridGenerator.gridHeight));
                }

                if (i >= segments.Length - 1) 
                { 
                    waveIsActive = false; 
                    break; 
                }

                yield return new WaitForSeconds(currentWave.delayBetweenSegments);

            }
        }

        while (!waveIsActive)
        {
            yield return null;
        }
    }
}
