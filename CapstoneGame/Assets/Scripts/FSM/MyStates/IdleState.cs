using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.FSM.MyStates
{

    [CreateAssetMenu(fileName ="IdleState", menuName ="Unity-FSM/MyStates/Idle", order =1)]//First in List
    public class IdleState : FSMState
    {
        [SerializeField]
        float idleDuration = 3f;

        float totalDuration;

        float speed = 0;

        [SerializeField]
        bool canScan;

        [SerializeField]
        float scanDegrees;
        [SerializeField]
        float scanDistance;



        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FSMStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();
            if (EnteredState)
            {
                totalDuration = 0f;
                fsm.anim.SetFloat("speed", speed);
            }
            
            return EnteredState;
        }
        public bool Scan()
        {
            RaycastHit hit;
            //For Loop for Ray Cast
            for (int i = 0; i <= scanDegrees / 5; i++)
            {
                Vector3 rayDir = Quaternion.Euler(0, (i - scanDegrees / 10) * 5, 0) * npc.transform.forward;
                Debug.DrawRay(npc.transform.position, rayDir * scanDistance, Color.red);
                if (Physics.Raycast(npc.transform.position, rayDir, out hit, scanDistance))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        npc.target = hit.transform;
                        fsm.EnterState(FSMStateType.CHASE);
                        return true;
                    }
                }
            }
            //if raycast hits
            //if target is a player 
            //set the target
            //start the chase state
            // return true;
            return false;
        }
    

    public override void UpdateState()
        {
            if (EnteredState)
            {
                totalDuration += Time.deltaTime;

                if(totalDuration >= idleDuration)
                {
                    fsm.EnterState(FSMStateType.PATROL);
                }else
                {
                    if(canScan)
                    {
                        Scan();
                    }
                }
            }
        }

        public override bool ExitState()
        {
            
            base.ExitState();          

            return true;
        }

        
    }
}
