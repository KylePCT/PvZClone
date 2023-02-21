using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plant : MonoBehaviour
{
    public GameObject plantModel;

    [Space(10)]
    public int maxHealth;
    public int currentHealth;
    public TextMeshPro healthText;

    [Space(10)]
    public int sunCost;
    public enum SpawnRechargeTime { Fast, Medium, Slow };
    public SpawnRechargeTime spawnRechargeTime;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
