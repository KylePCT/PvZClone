using UnityEngine;

public class GamePlayIntroState : GameBaseState
{
    public override void OnEnterState(GameStateManager game)
    {
        //TODO: Show Intro cutscene elements.
        game.gridController.gameObject.SetActive(true);

    }

    public override void UpdateState(GameStateManager game)
    {
        //TODO: Fun text stuff? 
    }

    public override void OnExitState(GameStateManager game)
    {
        //TODO: Hide Intro cutscene elements.

    }
}
