using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    float speed;
    bool turning = false;
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }
    void Update()
    {
        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 20)
            {
                speed = Random.Range(myManager.minSpeed, 
                                     myManager.maxSpeed);
            }
            if (Random.Range(0, 100) < 20)
            {
                ApplyRules();
            }
        }

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
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
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
