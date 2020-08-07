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
    [CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/MyStates/Attack", order = 4)]//Fourth in List
    public class AttackState : FSMState
    {

        [SerializeField]
        int attackDamage = 10;

        
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.ATTACK;
        }

        public override bool EnterState()
        {
            EnteredState = true;

            fsm.anim.SetBool("isAttacking", true);

            return EnteredState;
        }
               
        public override void UpdateState()
        {
            if(EnteredState)
            {
                PlayerHealth health = npc.target.GetComponent<PlayerHealth>();
                health.Damage(attackDamage);
                if(health.isAlive)
                {
                    fsm.EnterState(FSMStateType.RETREAT);
                    
                }
                else
                {
                    fsm.EnterState(FSMStateType.IDLE);
                    
                }
            }
        }

        public override bool ExitState()
        {
            
            base.ExitState();

            fsm.anim.SetBool("isAttacking", false);

            return true;
        }
    }
}
