using System;
using UnityEngine;

public class PlantDeathState : PlantBaseState
{

    public override void OnEnterState(PlantStateManager plant)
    {
        TraceBeans.Info("Plant: <" + plant.thisPlant.gameObject.name + "> has been eaten! :(");
        plant.thisPlant.Kill();
    }

    public override void UpdateState(PlantStateManager plant)
    {

    }

    public override void OnExitState(PlantStateManager plant)
    {

    }
}
