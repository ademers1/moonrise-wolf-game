using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Assets.Code.NPCCode;
using System.Threading.Tasks;

namespace Assets.Code.FSM
{
    [RequireComponent(typeof(NPCHealth))]
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]
        //FSMState startingState;
        FSMState currentState;

        [SerializeField]
        List<FSMState> validStates;
        [SerializeField]

        NPCHealth health;

        NPC npc;
        public Animator anim;

        public bool stunned = false;

        bool isDead;
                    //Key                  //Value
        Dictionary<FSMStateType, FSMState> fsmStates;

        public void Awake()
        {
            currentState = null;

            fsmStates = new Dictionary<FSMStateType, FSMState>();

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
            npc = this.GetComponent<NPC>();

            anim = this.GetComponent<Animator>();
            //Iterate through each state in list
            foreach (FSMState validState in validStates)
            {
                FSMState state = Instantiate<FSMState>(validState);
                state.SetExecutingFSM(this);
                state.SetExecutingNPC(npc);
                state.SetNavMeshAgent(navMeshAgent);
                fsmStates.Add(state.StateType, state);
            }
        }

        public void Start()
        {
            /*if(startingState != null)
            {
                EnterState(startingState);
            }*/
            EnterState(FSMStateType.IDLE);
        }

        

        public void Update()
        {
            if (health.Health == 0 && !isDead)
            {
                isDead = true;
                anim.SetBool("isDead", true);
            }
            if(npc.stunDuration > 0)
            {
                stunned = true;
                npc.stunDuration -= Time.deltaTime;
            }
            else
            {
                stunned = false;
                npc.stunDuration = 0;
            }
            

            if(currentState != null && !isDead)
            {
                currentState.UpdateState();
            }
        }


        #region STATE MANAGEMENT

        public void EnterState(FSMState nextState)
        {
            if (nextState == null)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.ExitState();
            }
            
            currentState = nextState;
            currentState.EnterState();
        }

        public void EnterState(FSMStateType stateType)
        {
            //Check for Key
            if(fsmStates.ContainsKey(stateType))
            {
                //Grab next state from dictionary
                FSMState nextState = fsmStates[stateType];
                if(currentState != null)
                {
                    currentState.ExitState();
                }
                

                EnterState(nextState);
            }
        }
        
        #endregion

    }
}
