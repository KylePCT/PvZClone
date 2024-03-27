using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region States
    //States.
    public GameMainMenuState MainMenuState = new GameMainMenuState();

    public GamePlayIntroState PlayIntroState = new GamePlayIntroState();
    public GamePlayPickState PlayPickState = new GamePlayPickState();
    public GamePlayWaveState PlayWaveState = new GamePlayWaveState();
    public GamePlayFailState PlayFailState = new GamePlayFailState();
    public GamePlayCompleteState PlayCompleteState = new GamePlayCompleteState();

    public GameSettingState SettingState = new GameSettingState();
    public GameAlmanacState AlmanacState = new GameAlmanacState();

    GameBaseState currentState;
    [SerializeField] private string activeState;
    #endregion

    #region Controllers
    //References to controllers.
    [HideInInspector] public ScreenUIController screenUIController;
    [HideInInspector] public PlaceObjOnGrid gridController;
    [HideInInspector] public PlantToolbarController plantToolbarController;
    [HideInInspector] public SceneController sceneController;
    [HideInInspector] public WaveController waveController;
    #endregion

    void Start()
    {
        GetControllers();

        currentState = MainMenuState;
        currentState.OnEnterState(this);
        activeState = currentState.ToString();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void GetControllers()
    {
        //Main UI controller.
        screenUIController = GameObject.Find("UI Controllers").GetComponent<ScreenUIController>();

        //Gameplay controllers.
        gridController = GameObject.Find("Grid Controller").GetComponent<PlaceObjOnGrid>();
        plantToolbarController = GameObject.Find("Plant Toolbar Controller").GetComponent<PlantToolbarController>();
        sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        waveController = GameObject.Find("Wave Controller").GetComponent<WaveController>();

        ToggleAllGameplayControllers(false);
    }

    public void ChangeState(GameBaseState state)
    { 
        StartCoroutine("DelayedStateChange", state);
    }

    IEnumerator DelayedStateChange(GameBaseState state)
    {
        if (currentState != null) currentState.OnExitState(this);
        TraceBeans.Info("Exiting State <" + currentState.ToString() + ">.");

        yield return new WaitForSeconds(0.1f); //Otherwise ExitState() doesn't fire. Idk.

        currentState = state;
        activeState = currentState.ToString();
        state.OnEnterState(this);
        TraceBeans.Info("Entering State <" + currentState.ToString() + ">.");
    }

    public void ToggleAllGameplayControllers(bool state)
    {
        gridController.gameObject.SetActive(state);
        plantToolbarController.gameObject.SetActive(state);
        sceneController.gameObject.SetActive(state);
        waveController.gameObject.SetActive(state);
    }
}
