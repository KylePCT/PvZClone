using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject plantModel;

    [Space(10)]
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    [Space(10)]
    public int sunCost;
    public enum SpawnRechargeTime { Fast, Medium, Slow };
    public SpawnRechargeTime spawnRechargeTime;

    // Start is called before the first frame update
    void Start()
    {

    }

}
