using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUIController : MonoBehaviour
{
    public GameStateManager stateManager;

    private List<GameObject> uiObjects;

    [Header("Main Menu")]
    public GameObject mainMenuObject;
    public Button mainMenuContinueButton;
    public Button mainMenuAlmanacButton;
    public Button mainMenuSettingsButton;

    [Header("Level Intro Screen")]
    public GameObject introObject;
    public Button introButton;

    [Header("Pick Plants Screen")]
    public GameObject pickPlantsObject;
    public Button pickButton;

    [Header("Almanac")]
    public GameObject almanacObject;
    public Button almanacBackButton;

    [Header("Settings")]
    public GameObject settingsObject;
    public Button settingsBackButton;

    // Start is called before the first frame update
    void Start()
    {
        SetupUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupUI()
    {
        mainMenuContinueButton.onClick.AddListener(() =>
        {
            ActivateIntroScreen();
            stateManager.ChangeState(stateManager.PlayIntroState);
        });

        mainMenuAlmanacButton.onClick.AddListener(() =>
        {
            ActivateAlmanacScreen();
            stateManager.ChangeState(stateManager.AlmanacState);
        });

        mainMenuSettingsButton.onClick.AddListener(() =>
        {
            ActivateSettingsScreen();
            stateManager.ChangeState(stateManager.SettingState);
        });

        introButton.onClick.AddListener(() =>
        {
            ActivatePickPlantsScreen();
            stateManager.ChangeState(stateManager.PlayPickState);
        });

        pickButton.onClick.AddListener(() =>
        {
            stateManager.ChangeState(stateManager.PlayWaveState);
        });

        almanacBackButton.onClick.AddListener(() =>
        {
            ActivateMainMenuScreen();
            stateManager.ChangeState(stateManager.MainMenuState);
        });

        settingsBackButton.onClick.AddListener(() =>
        {
            ActivateMainMenuScreen();
            stateManager.ChangeState(stateManager.MainMenuState);
        });

        uiObjects = new List<GameObject>();
        uiObjects.Add(mainMenuObject);
        uiObjects.Add(introObject);
        uiObjects.Add(pickPlantsObject);
        uiObjects.Add(almanacObject);
        uiObjects.Add(settingsObject);

        ActivateMainMenuScreen();
    }

    #region Helpers
    public void ActivateMainMenuScreen()
    {
        ToggleAllScreens(false);
        mainMenuObject.SetActive(true);
    }

    public void ActivateIntroScreen()
    {
        ToggleAllScreens(false);
        introObject.SetActive(true);
    }

    public void ActivatePickPlantsScreen()
    {
        ToggleAllScreens(false);
        pickPlantsObject.SetActive(true);
    }


    public void ActivateAlmanacScreen()
    {
        ToggleAllScreens(false);
        almanacObject.SetActive(true);
    }

    public void ActivateSettingsScreen()
    {
        ToggleAllScreens(false);
        settingsObject.SetActive(true);
    }

    public void ToggleAllScreens(bool state)
    {
        foreach (GameObject go in uiObjects) { go.SetActive(state); }
    }
    #endregion
}
