using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Wave", menuName = "PvZRemake/Wave/New Wave", order = 1)]
internal class Wave : ScriptableObject
{
    public string waveNumber;
    public float delayBetweenSegments; //Uses this OR if the wave is defeated to trigger the next.

    [Header("Wave Data")]
    public WaveSegment[] waveSegments;
}

[Serializable]
internal class WaveSegment
{
    public Zombie[] zombiesInSegment;
}
