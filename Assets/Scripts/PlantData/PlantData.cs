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

    public enum SpawnRechargeTime { Fast, Medium, Slow };
    public SpawnRechargeTime spawnRechargeTime;
}