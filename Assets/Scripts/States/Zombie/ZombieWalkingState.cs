using UnityEngine;

public class ZombieWalkingState : ZombieBaseState
{
    public override void OnEnterState(ZombieStateManager zombie)
    {

    }

    public override void UpdateState(ZombieStateManager zombie)
    {
        zombie.transform.position -= new Vector3(zombie.thisZombie.walkingSpeed * Time.deltaTime, 0, 0);
    }

    public override void OnExitState(ZombieStateManager zombie)
    {

    }
}
