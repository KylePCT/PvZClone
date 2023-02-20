using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_Offensive : Plant
{
    [Header("Offensive")]
    public GameObject plantBullet;
    [Space(10)]
    public int damagePerShot;
    public int shotInterval;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("fireBullet");
    }

    IEnumerator fireBullet()
    {
        while (true)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(shotInterval);
            Debug.Log("Spawning Bullet.");

            //Every X seconds, spawn the bullet.
            GameObject bullet = Instantiate(plantBullet, transform, true);
            bullet.transform.parent = transform;
        }
    }
}
