using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using Assets.Code.FSM;

namespace Assets.Code.NPCCode
{
    [RequireComponent(typeof(NavMeshAgent), (typeof(FiniteStateMachine)))]

    public class NPC: MonoBehaviour
    {
        public Transform target;

        [SerializeField]
        NPCPatrolPoints[] patrolPoints;

        NavMeshAgent navMeshAgent;
        FiniteStateMachine finiteStatemachine;

        
        public void Awake()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>(); //this. is not neccessary but leaving for now
            finiteStatemachine = this.GetComponent<FiniteStateMachine>();
            //target = null;
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public NPCPatrolPoints[] PatrolPoints
        {
            get
            {
                return patrolPoints;
            }
        }
    }
}
