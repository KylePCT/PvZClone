using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Plant_Offensive : Plant
{
    [Header("Offensive")]
    public Bullet plantBullet;
    [Space(10)]
    public float damagePerShot;
    public float shotInterval;
    [Space(10)]
    public int bulletsPerShotframe;
    public float shotframeInterval;

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

            for (int i = 0; i < bulletsPerShotframe; i++)
            {
                //Every X seconds, spawn the bullet.
                Bullet bullet = Instantiate(plantBullet, transform, true);
                bullet.SetData(damagePerShot);
                bullet.transform.parent = transform;
                yield return new WaitForSeconds(shotframeInterval);
            }
        }

        while (!isFiring)
        {
            yield return null;
        }
    }
}
