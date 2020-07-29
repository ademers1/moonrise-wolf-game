/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.FSM;

namespace Assets.Code.FSM {

    public enum FSMStateType
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK,
        RETREAT,
    };

    public class NPC_Animate : MonoBehaviour
    {
        public FiniteStateMachine fsm;
        Animator anim;
        
        // Start is called before the first frame update
        void Start()
        {
            anim = this.GetComponent<Animator>();   
        }

        // Update is called once per frame
        void Update()
        {
            if(fsm._currentState = FSMStateType.PATROL)
            {
                anim.Se
            }
            if(fsm._currentState = FSMStateType.ATTACK)
            {
                anim.SetBool("isAttacking", true);
            }
            if(fsm._currentState = FSMStateType.)
        }
    }
}*/