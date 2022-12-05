using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Cont_Fleeing : MonoBehaviour
{
    GameObject[] goalLocation;
    NavMeshAgent character;
    Animator anim;
    float speedMult;
    float detectionRadius = 20f;
    float fleeRadius = 10f;
    void ResetAgent()
    {
        speedMult = Random.Range(0.3f, 1.3f);
        character.speed = 2 * speedMult;
        character.angularSpeed = 120;
        anim.SetFloat("speedMultiplier", speedMult);
        anim.SetTrigger("isWalking");
        character.ResetPath();
    }
    public void DetectNewObstacle(Vector3 obsCylinder)
    {
        if(Vector3.Distance(obsCylinder, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - obsCylinder).normalized;
            Vector3 newgoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            character.CalculatePath(newgoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                character.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                character.speed = 10;
                character.angularSpeed = 500;
            }
        }
    }
    void Start()
    {
        goalLocation = GameObject.FindGameObjectsWithTag("goal");
        character = this.GetComponent<NavMeshAgent>();
        character.SetDestination(goalLocation[Random.Range(0, goalLocation.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetFloat("wOffset", Random.Range(0, 1));
        ResetAgent();
    }
    void Update()
    {
        if(character.remainingDistance < 1)
        {
            ResetAgent();
            character.SetDestination(goalLocation[Random.Range(0, goalLocation.Length)].transform.position);
        }
    }   
}
