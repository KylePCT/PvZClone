using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Offensive : Plant
{
    [Header("Offensive")]
    public Bullet plantBullet;
    [Space(10)]
    public int damagePerShot;
    public int shotInterval;

    public bool isFiring;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator fireBullet()
    {
        while (isFiring)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(shotInterval);
            Debug.Log("Spawning Bullet.");

            //Every X seconds, spawn the bullet.
            Bullet bullet = Instantiate(plantBullet, transform, true);
            bullet.SetData(damagePerShot);
            bullet.transform.parent = transform;
        }

        while (!isFiring)
        {
            yield return null;
        }
    }
}
