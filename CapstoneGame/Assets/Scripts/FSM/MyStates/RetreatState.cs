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
    [CreateAssetMenu(fileName = "RetreatState", menuName = "Unity-FSM/MyStates/Retreat", order = 5)]//Fifth in List
    public class RetreatState : FSMState
    {
        public float retreatLength;
        private float retreatTimeRemaining;
        
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.RETREAT;
        }

        public override bool EnterState()
        {
            base.EnterState();
            retreatTimeRemaining = retreatLength;
            navMeshAgent.destination = npc.transform.position;
            //Play animation taunt
            return true;
        }

        public override void UpdateState()
        {
            if (fsm.stunned)
            {
                fsm.EnterState(FSMStateType.STUN);
            }
            if((retreatTimeRemaining -= Time.deltaTime) <= 0)
            {
                fsm.EnterState(FSMStateType.PATROL);
            }
        }


        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exiting Attack state");

            return true;
        }

    }
}
