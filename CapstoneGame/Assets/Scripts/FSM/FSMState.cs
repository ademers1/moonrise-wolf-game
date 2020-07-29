using Assets.Code.FSM;
using Assets.Code.NPCCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    RETREAT,
};


//Doesn't need to be attached to Game Object
public abstract class FSMState : ScriptableObject
{
    protected NavMeshAgent navMeshAgent;
    protected NPC npc;
    protected FiniteStateMachine fsm;
    
    

    public ExecutionState ExecutionState { get; protected set; }//Public get, protected Set - Inline property

    public FSMStateType StateType { get; protected set; }
    public bool EnteredState { get; protected set; }

    

    public virtual void OnEnable()
    {
        ExecutionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC = true;
        ExecutionState = ExecutionState.ACTIVE;

        //Does NavMeshAgent exist?
        successNavMesh = (navMeshAgent != null);

        //Does The Executing Agent exist?
        successNPC = (npc != null);

        return successNavMesh & successNPC;//return boolean conjunction
    }

    public abstract void UpdateState();
    

    public virtual bool ExitState()
    {
        ExecutionState = ExecutionState.COMPLETED;
        
        
        return true;
    }

    public virtual void SetNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        if(_navMeshAgent != null)
        {
            navMeshAgent = _navMeshAgent;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine _fsm)
    {
        if(_fsm != null)
        {
            fsm = _fsm;
        }
    }

    public virtual void SetExecutingNPC(NPC _npc)
    {
        if(_npc != null)
        {
            npc = _npc;
        }
    }

}
