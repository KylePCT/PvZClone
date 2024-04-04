using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ZombieStateManager))]
public class Zombie : MonoBehaviour
{
    private ZombieStateManager stateManager;
    private WaveController waveController;

    public GameObject zombieModel;

    [Header("Movement")]
    [Tooltip("The length of time it will take the Zombie to move one grid space.")]
    public float walkingSpeed;

    [Header("Attacking")]
    public float attackRate;
    public float damagePerAttack;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;
    public float maxExtraHealth;
    public float currentExtraHealth;
    public ArmourData armourData;

    public TextMeshPro healthText;

    [Header("Eating")]
    public bool isEating;

    [HideInInspector] public Collider colTouching;
    [HideInInspector] public Plant plantInCollision;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<ZombieStateManager>();

        waveController = WaveController.instance; 

        currentHealth = maxHealth + armourData.armourHealth;
        currentExtraHealth = maxExtraHealth;

        healthText.text = currentHealth.ToString();
        if (armourData != null) healthText.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //If it collides with a plant.
        if (col.CompareTag("Plant/Default"))
        {
            stateManager.ChangeState(stateManager.EatingState);
            colTouching = col;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //If it collides with a plant.
        if (col.CompareTag("Plant/Default"))
        {
            stateManager.ChangeState(stateManager.WalkingState);
            colTouching = null;
        }
    }

    public void TakeDamage(float damage)
    {
        //Take standard health damage.
        if (currentHealth > damage)
        {
            currentHealth = currentHealth - damage;
            healthText.text = currentHealth.ToString();

            TraceBeans.Info("Zombie: <" + this.gameObject.name + "> was hit for <" + damage + ">! Remaining Health: <" + currentHealth + ">.");
        }

        //When health is depleted...
        else if (currentHealth <= damage) 
        {
            if (stateManager.currentState != stateManager.DyingState) stateManager.ChangeState(stateManager.DyingState);

            //Take damage to extra health.
            currentExtraHealth = currentExtraHealth - damage;
            healthText.text = currentExtraHealth.ToString();
            TraceBeans.Info("Zombie: <" + this.gameObject.name + "> was hit for <" + damage + ">! Remaining Extra Health: <" + currentExtraHealth + ">.");

            //Rip.
            if (currentExtraHealth <= damage)
            {
                stateManager.ChangeState(stateManager.DeathState);
            }
        }
    }

    IEnumerator eatPlant()
    {
        while (isEating)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(attackRate);
            if (plantInCollision != null)
            {
                TraceBeans.Info("Zombie: <" + this.gameObject.name + "> is eating: <" + plantInCollision.name + ". Remaining health: <" + plantInCollision.currentHealth + ">.");
                plantInCollision.currentHealth = plantInCollision.currentHealth - damagePerAttack;
                plantInCollision.healthText.text = plantInCollision.currentHealth.ToString();
            }
            else isEating = false;
        }

        while (!isEating)
        {
            yield return null;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
