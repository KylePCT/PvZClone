using UnityEngine;

public abstract class PlantBaseState
{

    //Abstract on each method means we need to define their functionality
    //in classes that derive from PlantStateManager.

    public abstract void OnEnterState(PlantStateManager plant);

    public abstract void UpdateState(PlantStateManager plant);

    public abstract void OnExitState(PlantStateManager plant);

}
