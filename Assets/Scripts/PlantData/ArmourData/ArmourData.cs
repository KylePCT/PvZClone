using UnityEngine;

[CreateAssetMenu(fileName = "ArmourData", menuName = "PvZRemake/Zombies/New Armour", order = 1)]
public class ArmourData : ScriptableObject
{
    [Header("Identity")]
    public string armourName;
    [TextArea(5, 5)]
    public string armourDescription;

    [Header("Visuals")]
    public GameObject armourPrefab;

    [Header("Parameters")]
    public int armourHealth;
    public bool affectedByMagnetism;
}