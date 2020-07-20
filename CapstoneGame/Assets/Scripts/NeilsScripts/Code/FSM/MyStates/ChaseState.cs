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
    [CreateAssetMenu(fileName = "ChaseState", menuName = "Unity-FSM/MyStates/Chase", order = 3)]//Third in List
    public class ChaseState: FSMState
    {
        protected float direction;

        [SerializeField]
        private float angularSpeed;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float catchDistance;
        [SerializeField]
        private float chaseDistance;

        

        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.CHASE;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            direction = npc.transform.rotation.y;
            //anim.SetBool("isRunning", true);

            return EnteredState;
        }

        public override void UpdateState()
        {
            if(npc.target == null)
            {
                fsm.EnterState(FSMStateType.PATROL);
                return;
            }

            float distance = (npc.transform.position - npc.target.position).magnitude;

            if(distance < catchDistance && npc.target.GetComponent<PlayerHealth>())
            {
                fsm.EnterState(FSMStateType.ATTACK);
                return;
            }
            else if(distance > chaseDistance)
            {
                fsm.EnterState(FSMStateType.PATROL);
                return;

            }
            /*Vector3 origin = npc.transform.position;
            origin.y = npc.target.position.y;

            float angle = Vector3.Angle(origin, npc.target.position);
            Vector3 cross = Vector3.Cross(origin, npc.target.position);//Cross 
            angle = Mathf.Min(angle, angularSpeed * Time.deltaTime);
            if (cross.y < 0)
            {
                angle = -angle;
            }

            direction += angle;
            npc.transform.rotation = Quaternion.Euler(0, direction, 0);

            
            */

            Vector3 forward = npc.transform.forward;
            Vector3 target = npc.target.position - npc.transform.position;
            target.y = 0;
            forward = Vector3.RotateTowards(forward, target, angularSpeed * Time.deltaTime, 0);
            npc.transform.forward = forward;
            navMeshAgent.destination = npc.transform.position + forward;//Take one step forward avoid kinematic crossover
            //npc.transform.position += npc.transform.forward * Time.deltaTime * speed;
        }

        
    }
}
