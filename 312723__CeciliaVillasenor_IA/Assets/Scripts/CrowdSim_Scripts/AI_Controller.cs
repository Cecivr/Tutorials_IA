using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public GameObject goal;
    NavMeshAgent character;
    void Start()
    {
        character = this.GetComponent<NavMeshAgent>();
        character.SetDestination(goal.transform.position);
    }
}
