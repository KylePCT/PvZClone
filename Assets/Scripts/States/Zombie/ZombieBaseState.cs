using UnityEngine;

public abstract class ZombieBaseState
{

    //Abstract on each method means we need to define their functionality
    //in classes that derive from ZombieBaseState.

    public abstract void OnEnterState(ZombieStateManager zombie);

    public abstract void UpdateState(ZombieStateManager zombie);

    public abstract void OnExitState(ZombieStateManager zombie);

}
