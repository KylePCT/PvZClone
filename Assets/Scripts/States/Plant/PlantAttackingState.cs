using System;
using UnityEngine;

public class PlantAttackingState : PlantBaseState
{
    private Plant_Offensive plantOffensive;

    public override void OnEnterState(PlantStateManager plant)
    {
        plantOffensive = plant.thisPlant.GetComponent<Plant_Offensive>();
        plantOffensive.isFiring = true;
        plantOffensive.StartCoroutine("FireBullet");
    }

    public override void UpdateState(PlantStateManager plant)
    {
        RaycastHit hit;

        if (Physics.Raycast(plant.thisPlant.transform.position, plant.thisPlant.transform.right, out hit, 100.0f, 1 << 9))
        {
            Debug.DrawRay(plant.thisPlant.transform.position, plant.thisPlant.transform.right * 100.0f, Color.green);
        }
        else
        {
            plant.ChangeState(plant.IdleState);
        }
    }

    public override void OnExitState(PlantStateManager plant)
    {
        plantOffensive.isFiring = false;
        plantOffensive.StopCoroutine("FireBullet");
    }
}
