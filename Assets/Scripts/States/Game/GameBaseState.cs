using UnityEngine;

public abstract class GameBaseState
{

    //Abstract on each method means we need to define their functionality
    //in classes that derive from GameStateManager.

    public abstract void OnEnterState(GameStateManager game);

    public abstract void UpdateState(GameStateManager game);

    public abstract void OnExitState(GameStateManager game);

}
