using UnityEngine;

public class AIControl : MonoBehaviour {

    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent character;
    Animator anim;
    float speedMult;
    void Start() {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        character = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        character.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetFloat("wOffset", Random.Range(0,1));
        anim.SetTrigger("isWalking");
        speedMult = Random.Range(0.5f, 1.3f);
        anim.SetFloat("speedMultiplier", speedMult);
        character.speed *= speedMult;
    }
    void Update() {

        if (character.remainingDistance < 1) {

            character.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
}
