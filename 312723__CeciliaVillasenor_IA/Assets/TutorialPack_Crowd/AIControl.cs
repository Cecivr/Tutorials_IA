using UnityEngine;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent character;
    Animator anim;
    void Start() {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        character = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        character.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("isWalking");
    }
    void Update() {

        if (character.remainingDistance < 1) {

            character.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
}
