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

        
        public bool ikActive = false;
        public Transform rightHandObj = null;
        public Transform lookObj = null;

        
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.ATTACK;
        }

        public override bool EnterState()
        {
            EnteredState = true;

            fsm.anim.SetBool("isAttacking", true);
            ikActive = true;
            

            return EnteredState;
        }
               
        public override void UpdateState()
        {
            if(EnteredState)
            {
                npc.Attack(npc.target);
                PlayerHealth health = npc.target.GetComponent<PlayerHealth>();
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
            ikActive = false;
            fsm.anim.SetBool("isAttacking", false);

            return true;
        }

        void OnAnimatorIK()
        {
            if (fsm.anim)
            {
                if (ikActive)
                {
                    if(lookObj != null)
                    {
                        fsm.anim.SetLookAtWeight(1);
                        fsm.anim.SetLookAtPosition(lookObj.position);
                    }

                    if(rightHandObj != null)
                    {
                        fsm.anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        fsm.anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        fsm.anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                        fsm.anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                    }
                }
                else
                {
                    fsm.anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    fsm.anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    fsm.anim.SetLookAtWeight(0);
                }
            }
        }
    }
}
