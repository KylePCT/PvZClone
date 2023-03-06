using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Sun : MonoBehaviour
{
    private UIController uiController;

    public int sunLifetime;
    public int sunAmount;

    private Rigidbody objectRigidbody;

    void Start()
    {
        uiController = UIController.instance;
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
        uiController.IncreaseSunAmount(sunAmount);
        Destroy(gameObject);
    }
}
