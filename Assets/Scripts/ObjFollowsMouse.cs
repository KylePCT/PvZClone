using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowsMouse : MonoBehaviour
{
    private PlaceObjOnGrid placeObjectOnGrid;
    public bool isOnGrid;

    // Start is called before the first frame update
    void Start()
    {
        placeObjectOnGrid = FindObjectOfType<PlaceObjOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGrid) transform.position = placeObjectOnGrid.smoothMousePos + new Vector3(0, 0.5f, 0);
    }
}
