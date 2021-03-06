﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float damage = 200.0f;   //Set CannonBall damage

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0f, 500 * Time.fixedDeltaTime), Space.Self);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boat") //Checks for collision with ships
        {
            Destroy(gameObject);    //Destroys Cannonball after impact
        }
    }
}
