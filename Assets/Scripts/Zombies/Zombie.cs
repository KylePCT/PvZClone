using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject zombieModel;

    [Header("Movement")]
    public float walkingSpeed;

    [Header("Attacking")]
    public int attackRate;
    public int damagePerAttack;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    public TextMeshPro healthText;

    [Header("Eating")]
    public bool isEating;

    private Plant plantInCollision;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEating) transform.position -= new Vector3(walkingSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Plant")
        {
            isEating = true;
            plantInCollision = col.gameObject.GetComponent<Plant>();
            StartCoroutine("eatPlant");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Plant")
        {
            isEating = false;
            plantInCollision = null;
            StopCoroutine("eatPlant");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthText.text = currentHealth.ToString();

        Debug.Log("Zombie hit!   " + this.gameObject.name);

        if (currentHealth <= 0) 
        { 
            Destroy(this.gameObject);
            Debug.Log("Zombie dead!   " + this.gameObject.name);
        }
    }

    IEnumerator eatPlant()
    {
        while (isEating)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(attackRate);
            Debug.Log("Eating.  " + plantInCollision.name + "  " + plantInCollision.currentHealth);
            plantInCollision.currentHealth = plantInCollision.currentHealth - damagePerAttack;
            plantInCollision.healthText.text = plantInCollision.currentHealth.ToString();
        }

        while (!isEating)
        {
            yield return null;
        }
    }
}
