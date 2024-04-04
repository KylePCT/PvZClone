using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Wave", menuName = "PvZRemake/Zombies/New Zombie Wave", order = 1)]
public class Wave : ScriptableObject
{
    public string waveNumber;
    public float delayBetweenSegments; //Uses this OR if the wave is defeated to trigger the next.

    [Header("Wave Data")]
    public WaveSegment[] waveSegments;
}

[Serializable]
public class WaveSegment
{
    public Zombie[] zombiesInSegment;
}
