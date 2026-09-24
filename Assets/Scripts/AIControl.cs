using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    [SerializeField] private GameObject[] goal;
    NavMeshAgent agent;
    private Animator anim;
    private float speedMulti;
    private float detectionRadius = 20f;
    private float fleeRadius = 10f;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();

        if (goal.Length == 0)
            goal = GameObject.FindGameObjectsWithTag("goal");

        int i = Random.Range(0, goal.Length);

        agent.SetDestination(goal[i].transform.position);

        if (TryGetComponent<Animator>(out anim))
            anim.SetFloat("wOffset", Random.Range(0f, 1f));
        
        ResetAgent();
    }

    private void ResetAgent()
    {
        speedMulti = Random.Range(0.1f, 1.5f);
        if (anim)
        {
            anim.SetFloat("speedMulti", speedMulti);
            anim.SetTrigger("isWalking");
        }
        agent.speed *= speedMulti;
        agent.angularSpeed = 120f;
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (transform.position - position).normalized;
            Vector3 newGoal = transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                if (anim)
                    anim.SetTrigger("isRunning");
                agent.speed = 10f;
                agent.angularSpeed = 500f;
            }
        }
    }

    private void Update() 
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            int i = Random.Range(0, goal.Length);
            agent.SetDestination(goal[i].transform.position);
        }
    }
}