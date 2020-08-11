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

    //Inherited class
    public class NPC_Ranged:NPC
    {
        public Transform muzzleEnd;

        public override void Attack(Transform attackTarget)
        {
            //Particle burst muzzleEnd
            base.Attack(attackTarget);
            //Set animation here
        }
    }

    
}
