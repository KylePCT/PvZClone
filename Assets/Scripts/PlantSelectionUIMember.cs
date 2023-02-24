using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantSelectionUIMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlaceObjOnGrid gridController;
    private UIController uiController;
    private TooltipController tooltipController;

    [Tooltip("The plant represented by this UI block.")]
    public PlantData selectedPlant;
    [HideInInspector] public bool hasBeenPlaced;

    private float maxRechargeTime;
    private float rechargeTime;
    private bool isRecharging;

    private void Start()
    {
        gridController = PlaceObjOnGrid.instance;
        uiController = UIController.instance;
        tooltipController = uiController.tooltip.GetComponent<TooltipController>();

        //Initialise.
        uiController.tooltip.SetActive(false);
        isRecharging = false;
    }

    public void OnMouseClickOnUI()
    {
        if (isRecharging) return;

        if (gridController.onMousePrefab == null && uiController.currentSun >= selectedPlant.sunCost)
        {
            gridController.onMousePrefab = Instantiate(selectedPlant.plantPrefab.transform, gridController.mousePos, Quaternion.identity);
        }
    }

    public void PlaceAndBeginRecharging()
    {
        uiController.DecreaseSunAmount(selectedPlant.sunCost);
        uiController.currentlyHighlightedUIMember = null;

        SetRechargeTime();
        isRecharging = true;
        GetComponent<Image>().fillAmount = 0;
        rechargeTime = 0;
        StartCoroutine("RechargePlantUI");
    }

    private void SetRechargeTime()
    {
        switch (selectedPlant.spawnRechargeTime)
        {
            case PlantData.SpawnRechargeTime.Fast:
                {
                    rechargeTime = 7.5f;
                }
                break;

            case PlantData.SpawnRechargeTime.Medium:
                {
                    rechargeTime = 30f;
                }
                break;

            case PlantData.SpawnRechargeTime.Slow:
                {
                    rechargeTime = 50f;
                }
                break;
        }

        maxRechargeTime = rechargeTime;
    }

    IEnumerator RechargePlantUI()
    { 
        while (isRecharging)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(1f);

            if (rechargeTime < maxRechargeTime)
            {
                rechargeTime++;
                GetComponent<Image>().fillAmount = rechargeTime / maxRechargeTime;
            }
            else isRecharging = false;
        }

        while (!isRecharging)
        {
            GetComponent<Image>().fillAmount = 1f;
            yield return null;
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");

        uiController.currentlyHighlightedUIMember = this;

        uiController.tooltip.SetActive(true);
        tooltipController.plantName.text = selectedPlant.plantName;
        tooltipController.plantDescription.text = selectedPlant.plantDescription;
        tooltipController.sunCost.text = "Sun Cost: " + selectedPlant.sunCost.ToString();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");

        uiController.tooltip.SetActive(false);
    }
}
