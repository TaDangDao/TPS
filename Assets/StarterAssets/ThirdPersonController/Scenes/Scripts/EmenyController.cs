using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class EmenyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private List<Transform> points= new List<Transform>();
    private Animator animator;
    private float distanceOffset = 2f;
    private float range = 1f;
    private float time = 0.8f;
    private float timest = 0.8f;
    private Transform currentPoint ;
    private bool isAttack;
    private const string attackString = "attack";
    public enum State
    {
        PATROL,CHASE,ATTACK
    }
    private State state;
    void Start()
    {
        animator=GetComponent<Animator>();
        state= State.PATROL;
        currentPoint = points[Random.Range(0, points.Count)];
        isAttack = false;
        StartCoroutine(patrolState());
        animator.SetBool(attackString, isAttack);

    }
    public void changeState(State newstate) {
      
        StopAllCoroutines();
        state = newstate;
        animator.SetBool(attackString, isAttack);

        switch (newstate)
        {
            case State.PATROL:
                StartCoroutine(patrolState());
                break;
            case State.CHASE:
                StartCoroutine(chaseState());
                break;
            case State.ATTACK:
                StartCoroutine(attackState());
                    break;


        }
        }

    // Update is called once per frame
    public IEnumerator attackState()
    {
       
        while (state == State.ATTACK)
        {
            timest += Time.deltaTime;
            if (timest > time) { ThirdPersonController._instance.Damaged();
                timest = 0;
            }
            
            if (Vector3.Distance(transform.position, player.position) > distanceOffset) { 
                
                isAttack=false;
               
                changeState(State.CHASE);
                yield break;
            }
            yield return null;
        }
        yield break;
    }
    public IEnumerator chaseState()
    {
        
        while (state == State.CHASE)
        {
          
            enemy.SetDestination(player.position);
            if (Vector3.Distance(transform.position, player.position) < distanceOffset)
            {
                isAttack = true;
                changeState(State.ATTACK);
                yield break;
            }
            yield return null;
        }
       
    }
    public IEnumerator patrolState() {
        
        while (state == State.PATROL)
        {
           
            enemy.SetDestination(currentPoint.position);
            if (Vector3.Distance(transform.position, currentPoint.position) < distanceOffset)
            {

                currentPoint = points[Random.Range(0, points.Count)];
            }
            yield return null;
        }
       
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThirdPersonController>() != null) {

            changeState(State.CHASE);
            
        }
    }
   

}
