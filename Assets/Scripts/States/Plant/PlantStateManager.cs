using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateManager : MonoBehaviour
{
    #region States
    //States.
    public PlantIdleState IdleState = new PlantIdleState();
    public PlantAttackingState AttackingState = new PlantAttackingState();
    public PlantDeathState DeathState = new PlantDeathState();

    public PlantBaseState currentState;
    [SerializeField] private string activeState;
    #endregion

    #region Controllers
    //References to controllers.
    [HideInInspector] public Plant thisPlant;
    #endregion

    private void Awake()
    {
        thisPlant = GetComponent<Plant>();
    }

    void Start()
    {
        currentState = IdleState;
        currentState.OnEnterState(this);
        activeState = currentState.ToString();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }


    public void ChangeState(PlantBaseState state)
    {
        StartCoroutine("DelayedStateChange", state);
    }

    IEnumerator DelayedStateChange(PlantBaseState state)
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
