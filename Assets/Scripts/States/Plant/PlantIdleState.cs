using System;
using UnityEngine;

public class PlantIdleState : PlantBaseState
{
    private bool isAttacking = false;

    public override void OnEnterState(PlantStateManager plant)
    {
        isAttacking = false;
    }

    public override void UpdateState(PlantStateManager plant)
    {
        RaycastHit hit;

        if (!isAttacking) 
        { 
            if (Physics.Raycast(plant.thisPlant.transform.position, plant.thisPlant.transform.right, out hit, 100.0f, 1 << 9))
            { 
                Debug.DrawRay(plant.thisPlant.transform.position, plant.thisPlant.transform.right * 100.0f, Color.yellow);
                if (hit.transform.CompareTag("Zombie"))
                {
                    Debug.Log("STUF");
                    plant.ChangeState(plant.AttackingState);
                    isAttacking = true;
                }
                else { return; }
            }
        }
    }

    public override void OnExitState(PlantStateManager plant)
    {

    }
}
