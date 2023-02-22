using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletSpeed;
    public int bulletLifetime;

    private float damageOnHit;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector3.zero;

        Destroy(gameObject, bulletLifetime);
    }

    public void SetData(float damageOnHit)
    {
        this.damageOnHit = damageOnHit;
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
            col.GetComponent<Zombie>().TakeDamage(damageOnHit);
            Debug.Log("Hit Zombie: " + col.gameObject.name);
            Destroy(gameObject); //Destroy bullet.
        }
    }

}
