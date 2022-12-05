using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [SerializeField]
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5, 5, 5);

    [Header("FishSeattings")]
    [Range(0.5f,5.0f)]
    public float minSpeed;
    [Range(0.5f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    void Start()
    {
        allFish = new GameObject[numFish];

        for (int i = 0; i < numFish; i++)
        {
            Vector3 posFish = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x), 
                                                                Random.Range(-swimLimits.y, swimLimits.y), 
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, posFish, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
    void Update()
    {
        
    }
}
