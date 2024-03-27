using UnityEngine;

public class GamePlayWaveState : GameBaseState
{
    public override void OnEnterState(GameStateManager game)
    {
        //TODO: Load and begin game wave logic.
    }

    public override void UpdateState(GameStateManager game)
    {
        //TODO: If wave finishes, move to complete, if house is reached, move to fail.
    }

    public override void OnExitState(GameStateManager game)
    {
        //TODO: Close and clean-up game enviro and logic.

    }
}
