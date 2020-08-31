using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using Assets.Code.NPCCode;

namespace Assets.Code.FSM.MyStates
{
    [RequireComponent(typeof(IdleState))]
    [CreateAssetMenu(fileName = "StunState", menuName = "Unity-FSM/MyStates/Stun", order = 6)]//Second In List
    // Start is called before the first frame update
    public class StunState : FSMState
    {

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.STUN;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                EnteredState = true;
            }
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                Debug.Log("Entering Stun State");
                if (!fsm.stunned)
                {
                    fsm.EnterState(FSMStateType.CHASE);
                }
                else
                {
                    navMeshAgent.isStopped = true;
                }
            }
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Stun state");

            return true;
        }
    }
}
