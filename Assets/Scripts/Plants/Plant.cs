using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

internal class Plant : MonoBehaviour
{
    public PlantData selectedPlant;

    public float maxHealth;
    public float currentHealth;
    public TextMeshPro healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (currentHealth <= 0) { Destroy(gameObject); }
    }
}
