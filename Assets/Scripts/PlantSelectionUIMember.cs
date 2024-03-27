using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantSelectionUIMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlaceObjOnGrid gridController;
    private PlantToolbarController pickerController;
    private TooltipController tooltipController;

    [Tooltip("The plant represented by this UI block.")]
    public PlantData selectedPlant;
    [HideInInspector] public bool hasBeenPlaced;

    private float maxRechargeTime;
    private float rechargeTime;
    private bool isRecharging;
    private bool isInitial; //If level has just begun.

    private void Start()
    {
        gridController = PlaceObjOnGrid.instance;
        pickerController = PlantToolbarController.instance;
        tooltipController = pickerController.tooltip.GetComponent<TooltipController>();

        Initialise();
    }

    private void Initialise()
    {
        //Initialise.
        pickerController.tooltip.SetActive(false);
        SetRechargeTime();

        if (selectedPlant.spawnRechargeTime != PlantData.SpawnRechargeTime.Fast)
        {
            isInitial = true;
            isRecharging = true;
            GetComponent<Image>().fillAmount = 0;
            rechargeTime = 0;
            StartCoroutine("RechargePlantUI");
        }
    }

    public void OnMouseClickOnUI()
    {
        if (isRecharging) return;

        if (gridController.onMousePrefab == null && pickerController.currentSun >= selectedPlant.sunCost)
        {
            gridController.onMousePrefab = Instantiate(selectedPlant.plantPrefab.transform, gridController.mousePos, Quaternion.identity);
        }
    }

    public void PlaceAndBeginRecharging()
    {
        pickerController.DecreaseSunAmount(selectedPlant.sunCost);
        pickerController.currentlyHighlightedUIMember = null;

        SetRechargeTime(); //Uses the spawnRechargeTime Enum to set the recharge time.
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
                    maxRechargeTime = 7.5f;
                }
                break;

            case PlantData.SpawnRechargeTime.Slow:
                {
                    maxRechargeTime = isInitial ? 20f : 30f;
                    isRecharging = isInitial ? true : false;
                }
                break;

            case PlantData.SpawnRechargeTime.Very_Slow:
                {
                    maxRechargeTime = isInitial ? 35f : 50f;
                    isRecharging = isInitial ? true : false;
                }
                break;
        }
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
            else
            {
                isRecharging = false;
            }
        }

        while (!isRecharging)
        {
            GetComponent<Image>().fillAmount = 1f;

            if (isInitial)
            {
                isInitial = false;
                SetRechargeTime();
            }

            yield return null;
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        TraceBeans.Info("Cursor Entering: <" + name + "> UI Object.");

        pickerController.currentlyHighlightedUIMember = this;

        pickerController.tooltip.SetActive(true);
        tooltipController.plantName.text = selectedPlant.plantName;
        tooltipController.plantDescription.text = selectedPlant.plantDescription;
        tooltipController.sunCost.text = "Sun Cost: " + selectedPlant.sunCost.ToString();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        TraceBeans.Info("Cursor Exiting: <" + name + "> UI Object.");

        pickerController.tooltip.SetActive(false);
    }
}
