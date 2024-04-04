using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlantStateManager))]
public class Plant : MonoBehaviour
{
    public PlantStateManager stateManager;
    public PlantData selectedPlant;

    public float maxHealth;
    public float currentHealth;
    public TextMeshPro healthText;

    // Start is called before the first frame update
    void Start()    
    {
        stateManager = GetComponent<PlantStateManager>();

        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        if (currentHealth <= 0) { stateManager.ChangeState(stateManager.DeathState); }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
