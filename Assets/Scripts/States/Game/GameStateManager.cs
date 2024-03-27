using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameMainMenuState MainMenuState = new GameMainMenuState();

    public GamePlayIntroState PlayIntroState = new GamePlayIntroState();
    public GamePlayPickState PlayPickState = new GamePlayPickState();
    public GamePlayWaveState PlayWaveState = new GamePlayWaveState();
    public GamePlayFailState PlayFailState = new GamePlayFailState();
    public GamePlayCompleteState PlayCompleteState = new GamePlayCompleteState();

    public GameSettingState SettingState = new GameSettingState();
    public GameAlmanacState AlmanacState = new GameAlmanacState();

    public GameBaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        if (currentState == null) currentState = PlayIntroState;

        //References the context - aka this manager.
        currentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(GameBaseState state)
    {
        state.OnExitState(this);
        currentState = state;
        state.OnEnterState(this);
    }
}
