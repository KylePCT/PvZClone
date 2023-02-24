using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    public TextMeshProUGUI plantName;
    public TextMeshProUGUI plantDescription;
    public TextMeshProUGUI sunCost;
    public GameObject pivot;

    private void Update()
    {
        if (isActiveAndEnabled)
        {
            Vector3 mousePosition = Input.mousePosition;
            pivot.transform.position = mousePosition;
        }
    }
}
