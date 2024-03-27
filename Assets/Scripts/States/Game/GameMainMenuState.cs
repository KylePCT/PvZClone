using UnityEngine;

public class GameMainMenuState : GameBaseState
{
    public override void OnEnterState(GameStateManager game)
    {
        //TODO: Load main menu.
    }

    public override void UpdateState(GameStateManager game)
    {

    }

    public override void OnExitState(GameStateManager game)
    {
        //TODO:
        //Based on button press, either load introstate, or settings.
        //game.ChangeState(game.PlayIntroState);
    }
}
