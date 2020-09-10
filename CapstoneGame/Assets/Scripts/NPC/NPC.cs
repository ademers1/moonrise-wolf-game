using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using Assets.Code.FSM;
using UnityEngine.Experimental.TerrainAPI;

namespace Assets.Code.NPCCode
{
    [RequireComponent(typeof(NavMeshAgent), (typeof(FiniteStateMachine)))]

    public class NPC : MonoBehaviour
    {
        public Transform target;

        public PlayerController player;

        [SerializeField]
        protected NPCPatrolPoints[] patrolPoints;

        NavMeshAgent navMeshAgent;
        FiniteStateMachine finiteStatemachine;
        public int attackDamage;

        public float stunDuration = 0f;

        public BoxCollider meleeWeapon;

        //Audio
        [FMODUnity.EventRefAttribute]
        public string hunterStepEventString = "event:/HunterSteps";
        FMOD.Studio.EventInstance hunterStepEvent;
        FMOD.Studio.PARAMETER_ID groundQualityID;

        public void Awake()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>(); //this. is not neccessary but leaving for now
            finiteStatemachine = this.GetComponent<FiniteStateMachine>();
            gameObject.layer = 9;
            if(target != null)
            {
                player = target.GetComponent<PlayerController>();
            }
            if(meleeWeapon == null)
            {
                meleeWeapon = GetComponentInChildren<BoxCollider>();
            }
            meleeWeapon.enabled = false;
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public virtual void Attack(Transform attackTarget)
        {
            PlayerHealth health = attackTarget.GetComponent<PlayerHealth>();
            health.Health -= attackDamage;

            //Set animation here
        }
        public NPCPatrolPoints[] PatrolPoints
        {
            get
            {
                return patrolPoints;
            }
        }

        public void HunterStepSound()
        {
            //Audio
            hunterStepEvent = FMODUnity.RuntimeManager.CreateInstance(hunterStepEventString);
            FMOD.Studio.PARAMETER_DESCRIPTION groundDesc;
            FMOD.Studio.EventDescription stepDesc;

            hunterStepEvent.getDescription(out stepDesc);
            stepDesc.getParameterDescriptionByName("Ground Quality", out groundDesc);
            groundQualityID = groundDesc.id;
            hunterStepEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));

            hunterStepEvent.setParameterByID(groundQualityID, 50);

            hunterStepEvent.start();
            hunterStepEvent.release();

        }

        public void EnableWeaponCollider()
        {
            meleeWeapon.enabled = true;
        }

        public void DisableWeaponCollider()
        {
            meleeWeapon.enabled = false;
        }
    }
}
