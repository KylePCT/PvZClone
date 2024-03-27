using UnityEngine;

public class GamePlayPickState : GameBaseState
{
    public override void OnEnterState(GameStateManager game)
    {
        //TODO: Open picking plants UI.
    }

    public override void UpdateState(GameStateManager game)
    {
        //TODO: Probably make sure the button logic is running? idk.
    }

    public override void OnExitState(GameStateManager game)
    {
        //TODO: Close picking plants UI and send results to UI manager in game.
        game.plantToolbarController.gameObject.SetActive(true);
    }
}
