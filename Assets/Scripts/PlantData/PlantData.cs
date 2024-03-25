using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "PvZRemake/Plant/New Plant", order = 1)]
internal class PlantData : ScriptableObject
{
    [Header("Identity")]
    public string plantName;
    [TextArea(5, 5)]
    public string plantDescription;

    [Header("Visuals")]
    public GameObject plantPrefab;

    [Header("Parameters")]
    public int sunCost;

    public enum PlantDesignation { Day, Night, Water_Day, Water_Night, Roof };
    public PlantDesignation plantDesignation;

    public enum SpawnRechargeTime { Fast, Slow, Very_Slow };
    public SpawnRechargeTime spawnRechargeTime;

    public bool isUpgrade;
}