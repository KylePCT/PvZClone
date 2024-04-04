using UnityEngine;

public class ZombieEatingState : ZombieBaseState
{
    public override void OnEnterState(ZombieStateManager zombie)
    {
        zombie.thisZombie.isEating = true;
        zombie.thisZombie.plantInCollision = zombie.thisZombie.colTouching.gameObject.GetComponent<Plant>();
        zombie.thisZombie.StartCoroutine("eatPlant");
    }

    public override void UpdateState(ZombieStateManager zombie)
    {
        if (zombie.thisZombie.isEating == false)
        {
            zombie.ChangeState(zombie.WalkingState);
        }
    }

    public override void OnExitState(ZombieStateManager zombie)
    {
        zombie.thisZombie.isEating = false;
        zombie.thisZombie.plantInCollision = null;
        zombie.thisZombie.StopCoroutine("eatPlant");
    }
}
