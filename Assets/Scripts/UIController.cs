using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal class UIController : MonoBehaviour
{
    public static UIController instance = null;
    public TextMeshProUGUI sunCountText;
    public GameObject tooltip;

    public PlantSelectionUIMember currentlyHighlightedUIMember;

    [Header("Global Values")]
    public int startingSun;
    public int currentSun;

    private void Awake()
    {
        if (instance == null) instance = this; //Assign instance.
        else if (instance != this) Destroy(gameObject); //If we already have this existing, we don't need another.
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSun = startingSun;
        sunCountText.text = currentSun.ToString();
    }

    public void IncreaseSunAmount(int amount)
    {
        currentSun = currentSun + amount;
        sunCountText.text = currentSun.ToString();
    }

    public void DecreaseSunAmount(int amount)
    {
        currentSun = currentSun - amount;
        sunCountText.text = currentSun.ToString();
    }
}
