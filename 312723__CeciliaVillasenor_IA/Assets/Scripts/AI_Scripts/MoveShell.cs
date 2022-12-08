using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speed = 1;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(0, 0.5f * speed * Time.deltaTime, speed * Time.deltaTime);
    }
}
