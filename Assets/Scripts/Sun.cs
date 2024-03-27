using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private PlantToolbarController pickerController;

    public int sunLifetime;
    public int sunAmount;

    private Rigidbody objectRigidbody;

    void Start()
    {
        pickerController = PlantToolbarController.instance;
        objectRigidbody = GetComponent<Rigidbody>();

        Destroy(gameObject, sunLifetime);
    }

    //When it hits the ground, remove all movement and parenting.
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            objectRigidbody.useGravity = false;
            objectRigidbody.isKinematic = true;
            transform.parent = null;
        }
    }

    //When clicked, increase sun by X and destroy the object.
    private void OnMouseDown()
    {
        pickerController.IncreaseSunAmount(sunAmount);
        Destroy(gameObject);
        TraceBeans.Info("Sun has been collected. Total Sun Amount is now: <" + pickerController.currentSun + ">.");
    }
}
