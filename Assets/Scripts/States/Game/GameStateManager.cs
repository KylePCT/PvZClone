using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
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

    //References to controllers.
    [HideInInspector] public PlaceObjOnGrid gridController;
    [HideInInspector] public PlantToolbarController plantToolbarController;
    [HideInInspector] public SceneController sceneController;
    [HideInInspector] public WaveController waveController;

    void Start()
    {
        GetControllers();

        currentState = MainMenuState;
        activeState = currentState.ToString();

        //References the context - aka this manager.
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void GetControllers()
    {
        gridController = GameObject.Find("Grid Controller").GetComponent<PlaceObjOnGrid>();
        plantToolbarController = GameObject.Find("Plant Picker Controller").GetComponent<PlantToolbarController>();
        sceneController = GameObject.Find("Scene Controller").GetComponent<SceneController>();
        waveController = GameObject.Find("Wave Controller").GetComponent<WaveController>();

        ToggleAllGameplayControllers(false);
    }

    public void ChangeState(GameBaseState state)
    {
        state.OnExitState(this);
        currentState = state;
        activeState = currentState.ToString();
        state.OnEnterState(this);
    }

    public void ToggleAllGameplayControllers(bool state)
    {
        gridController.gameObject.SetActive(state);
        plantToolbarController.gameObject.SetActive(state);
        sceneController.gameObject.SetActive(state);
        waveController.gameObject.SetActive(state);
    }
}
