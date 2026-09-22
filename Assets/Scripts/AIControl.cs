using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    [SerializeField] private GameObject[] goal;
    NavMeshAgent agent;
    private Animator anim;

    void Start() {

        agent = GetComponent<NavMeshAgent>();

        if (goal.Length == 0)
            goal = GameObject.FindGameObjectsWithTag("goal");

        int i = Random.Range(0, goal.Length);

        agent.SetDestination(goal[i].transform.position);

        if (TryGetComponent<Animator>(out anim))
            anim.SetTrigger("isWalking");
            anim.SetFloat("wOffset", Random.Range(0f, 1f));
            float sm = Random.Range(0.5f, 2f);
            anim.SetFloat("speedMulti", sm);
            agent.speed *= sm;
    }


    void Update() {
        if (agent.remainingDistance < 1)
        {
            int i = Random.Range(0, goal.Length);
            agent.SetDestination(goal[i].transform.position);
        }
    }
}