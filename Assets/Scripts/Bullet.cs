using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletSpeed;
    public int bulletLifetime;
    private int currentLifetime;

    private int damageOnHit;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("bulletLife");

        damageOnHit = GetComponentInParent<Plant_Offensive>().damagePerShot;
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(bulletSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
            Debug.Log("Hit zombie: " + col.gameObject.name);
        }
    }

    IEnumerator bulletLife()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentLifetime++;
            if (currentLifetime >= bulletLifetime) Destroy(gameObject);
        }
    }
}
