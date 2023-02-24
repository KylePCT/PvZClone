using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Plant/New Plant", order = 1)]
public class PlantData : ScriptableObject
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