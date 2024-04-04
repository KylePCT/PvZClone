using System;
using UnityEngine;

public class ZombieDeathState : ZombieBaseState
{

    public override void OnEnterState(ZombieStateManager zombie)
    {
        TraceBeans.Info("Zombie: <" + zombie.thisZombie.gameObject.name + "> is now dead!");
        zombie.waveController.currentZombies--;
        zombie.thisZombie.Kill();
    }

    public override void UpdateState(ZombieStateManager zombie)
    {

    }

    public override void OnExitState(ZombieStateManager zombie)
    {

    }
}
