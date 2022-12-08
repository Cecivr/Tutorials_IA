using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shell : MonoBehaviour
{
    public GameObject explosion;
    Rigidbody rb;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "tank")
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 1f);
            Destroy(this.gameObject,5);
        }
    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        this.transform.forward = rb.velocity;
    }
}
