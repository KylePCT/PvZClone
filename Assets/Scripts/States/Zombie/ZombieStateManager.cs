using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    #region States
    //States.
    public ZombieIdleState IdleState = new ZombieIdleState();
    public ZombieWalkingState WalkingState = new ZombieWalkingState();
    public ZombieEatingState EatingState = new ZombieEatingState();
    public ZombieDyingState DyingState = new ZombieDyingState();
    public ZombieDeathState DeathState = new ZombieDeathState();

    public ZombieBaseState currentState;
    [SerializeField] private string activeState;
    #endregion

    #region Controllers
    //References to controllers.
    [HideInInspector] public WaveController waveController;

    [HideInInspector] public Zombie thisZombie;
    #endregion

    void Start()
    {
        GetControllers();

        currentState = WalkingState;
        currentState.OnEnterState(this);
        activeState = currentState.ToString();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void GetControllers()
    {
        waveController = GameObject.Find("Wave Controller").GetComponent<WaveController>();

        thisZombie = GetComponent<Zombie>();
    }

    public void ChangeState(ZombieBaseState state)
    {
        StartCoroutine("DelayedStateChange", state);
    }

    IEnumerator DelayedStateChange(ZombieBaseState state)
    {
        if (currentState != null) currentState.OnExitState(this);
        TraceBeans.Info("Exiting State <" + currentState.ToString() + ">.");

        yield return new WaitForSeconds(0.1f); //Otherwise ExitState() doesn't fire. Idk.

        currentState = state;
        activeState = currentState.ToString();
        state.OnEnterState(this);
        TraceBeans.Info("Entering State <" + currentState.ToString() + ">.");
    }
}
