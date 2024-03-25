using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal class Zombie : MonoBehaviour
{
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

    public TextMeshPro healthText;

    [Header("Eating")]
    public bool isEating;

    private Plant plantInCollision;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentExtraHealth = maxExtraHealth;
        healthText.text = currentHealth.ToString();

        //Walking speed refers to speed/tile, so we divide to get that value accurately. Divide again by 2 for more accuracy.
        walkingSpeed = ((walkingSpeed / PlaceObjOnGrid.instance.gridWidth) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEating) transform.position -= new Vector3(walkingSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        //If it collides with a plant.
        if (col.CompareTag("Plant/Default"))
        {
            isEating = true;
            plantInCollision = col.gameObject.GetComponent<Plant>();
            StartCoroutine("eatPlant");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //If it collides with a plant.
        if (col.CompareTag("Plant/Default"))
        {
            isEating = false;
            plantInCollision = null;
            StopCoroutine("eatPlant");
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
            //Take damage to extra health.
            currentExtraHealth = currentExtraHealth - damage;
            healthText.text = currentExtraHealth.ToString();
            TraceBeans.Info("Zombie: <" + this.gameObject.name + "> was hit for <" + damage + ">! Remaining Extra Health: <" + currentExtraHealth + ">.");

            //Stop movement ("die").
            walkingSpeed = 0;

            //Rip.
            if (currentExtraHealth <= damage)
            {
                TraceBeans.Info("Zombie: <" + this.gameObject.name + "> is now dead!");
                Destroy(gameObject);
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
}
