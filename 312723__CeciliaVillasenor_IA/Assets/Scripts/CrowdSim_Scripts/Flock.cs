using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    float speed;
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }
    void Update()
    {
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;

        //average position
        Vector3 vcentre = Vector3.zero;
        //average avoidance
        Vector3 vavoid = Vector3.zero;
        float globalSpeed = 0.01f;
        float neighbourDistance;
        int groupSize = 0;

        foreach (GameObject currentFish in gos)
        {
            if(currentFish != this.gameObject)
            {
                neighbourDistance = Vector3.Distance(currentFish.transform.position, this.transform.position);
                if (neighbourDistance <= myManager.neighbourDistance)
                {
                    vcentre += currentFish.transform.position;
                    groupSize++;

                    if (neighbourDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - currentFish.transform.position);
                    }

                    Flock anitherFlock = currentFish.GetComponent<Flock>();
                    globalSpeed = globalSpeed + anitherFlock.speed;
                }
            }
        }
        if(groupSize > 0)
	    {
            vcentre = vcentre / groupSize;
            speed = globalSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      myManager.rotationSpeed * Time.deltaTime);
            }
	    }
    }

}
