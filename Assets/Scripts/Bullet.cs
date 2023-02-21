using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletSpeed;
    public int bulletLifetime;

    private int damageOnHit;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = Vector3.zero;

        Destroy(gameObject, bulletLifetime);
    }

    public void SetData(int damageOnHit)
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
            Destroy(gameObject);
            Debug.Log("Hit zombie: " + col.gameObject.name);
        }
    }
}
