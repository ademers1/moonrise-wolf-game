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
    [CreateAssetMenu(fileName = "RangedAttackState", menuName = "Unity-FSM/MyStates/Ranged Attack", order = 4)]
    public class RangedAttack : FSMState
    {



        [SerializeField]
        float attackDelay = 1f;
        float delayTimeRemaining;

        [SerializeField]
        float shootDistance;

        [SerializeField]
        public GameObject bullet;

        [SerializeField]
        private Transform muzzleEnd;

        [SerializeField]
        public AudioClip gunShot;

        private bool isShooting;
        private float shootForce;
        private float shootRate;
        private float shootRateTime;

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.ATTACK;

        }

        public override bool EnterState()
        {
            EnteredState = true;
            navMeshAgent.isStopped = true;
            return EnteredState;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                delayTimeRemaining -= Time.deltaTime;
                if (delayTimeRemaining > 0)
                {
                    return;
                }
                if (Vector3.Distance(npc.target.position, npc.transform.position) > shootDistance)
                {
                    navMeshAgent.isStopped = false;
                    fsm.EnterState(FSMStateType.CHASE);
                    return;
                }



                PlayerHealth health = npc.target.GetComponent<PlayerHealth>();

                if (health.isAlive)
                {
                    delayTimeRemaining = attackDelay;

                }
                else
                {
                    navMeshAgent.isStopped = false;
                    fsm.EnterState(FSMStateType.PATROL);

                }
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