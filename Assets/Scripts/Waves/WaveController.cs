using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static WaveController instance = null;
    private PlaceObjOnGrid gridGenerator;

    private bool waveIsActive;

    public Wave currentWave;
    public bool isCurrentWaveComplete;

    private WaveSegment[] segments;

    [Space(10)]
    public int currentZombies;

    private void Awake()
    {
        if (instance == null) instance = this; //Assign instance.
        else if (instance != this) Destroy(gameObject); //If we already have this existing, we don't need another.
    }

    // Start is called before the first frame update
    void Start()
    {
        gridGenerator = PlaceObjOnGrid.instance;

        isCurrentWaveComplete = false;
        StartWave(currentWave);
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveIsActive && currentZombies <= 0 && !isCurrentWaveComplete) isCurrentWaveComplete = true;
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
                    currentZombies++;
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
