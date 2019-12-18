using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class MeleeEnemy : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public GameObject enemy;
    public Transform playerTransform;
    public bool isRunning;
    public Animator anim;
    public MeshRenderer rend;
    NavMeshAgent agent;
    public GameObject key;
    public ParticleSystem particle;
    public Transform[] nodeObj;
    bool isPatrol;
    int randomSpot;
    bool alerted;
    public WolfLevel PlayerLevel;
    int xpValue = 50;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        randomSpot = Random.Range(0, nodeObj.Length);
        isPatrol = true;
        GoOnPatrol();
    }

    void Update()
    {

        if(Vector3.Distance(playerTransform.position, transform.position) < 10)
        {
            transform.LookAt(playerTransform);
            transform.localScale = new Vector3(1, 1, 1);
            isRunning = true;
            alerted = true;
            agent.SetDestination(playerTransform.position);
        }
        
        if(!alerted)
        {
            isPatrol = true;
            GoOnPatrol();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<CharacterHealth>().TakeDamage(25);
            //Destroy(collision.gameObject);
            //SceneManager.LoadScene(1);
        }
    }
    public void EnemyDie()
    {
        PlayerLevel.AddExperience(xpValue);
        Destroy(this.gameObject);
    }
    public void dropItem(GameObject I)
    {
        Instantiate(I, transform.position, Quaternion.identity);
    }
    public void dieParticleActive()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }
    void GoOnPatrol()
    {
        if (isPatrol == true)
        {
            agent.destination = nodeObj[randomSpot].position;
            if (Vector3.Distance(enemy.transform.position, nodeObj[randomSpot].position) < 1f)
            {
                randomSpot = Random.Range(0, nodeObj.Length);
            }
        }
    }
}
