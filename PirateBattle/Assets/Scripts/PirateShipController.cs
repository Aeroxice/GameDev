﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShipController : MonoBehaviour
{
    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public GameObject Lookout = null;
    public GameObject[] sails = null;
    private BaseAI ai = null;

    private float BoatSpeed = 0f;
    private float SeaSize = 500.0f;
    private float RotationSpeed = 180.0f;
    public float Health = 1000.0f;   
    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = this.transform.localPosition;
    }

    public void SetAI(BaseAI _ai)
    {
        ai = _ai;
        ai.Ship = this;
    }

    public void StartBattle()
    {
        //Debug.Log("test");
        StartCoroutine(ai.RunAI());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Boat")
        {
            ScannedRobotEvent scannedRobotEvent = new ScannedRobotEvent();
            scannedRobotEvent.Distance = Vector3.Distance(transform.position, other.transform.position);
            scannedRobotEvent.Name = other.name;
            ai.OnScannedRobot(scannedRobotEvent, other.transform.position);
        }
    }   

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "CannonBall")
            {
                CannonBall cannonball = collision.gameObject.GetComponent(typeof(CannonBall)) as CannonBall;
                Health -= cannonball.damage;
                Debug.Log(Health);
                if (Health <= 0)
                {
                    Destroy(gameObject);
                }
            }

        }

    public IEnumerator __Move()
    {
        if (newPosition == this.transform.localPosition)
        {
            float PositionX = Random.Range(-500f, 500f);
            float PositionZ = Random.Range(-500f, 500f);
            newPosition = new Vector3(PositionX, 0, PositionZ);
        }
        this.transform.LookAt(newPosition);
        this.transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPosition, BoatSpeed * Time.fixedDeltaTime);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __BoatSpeed(float speed)
        {
            BoatSpeed = speed;

            yield return new WaitForFixedUpdate();
        }

    public IEnumerator __Health(float hp)
    {
        Health = hp;

        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __Ahead(float distance)
    {
        int numFrames = (int)(distance / (BoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            transform.Translate(new Vector3(0f, 0f, BoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator __Back(float distance)
    {
        int numFrames = (int)(distance / (BoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            transform.Translate(new Vector3(0f, 0f, -BoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator __TurnLeft(float angle)
    {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator __TurnRight(float angle)
    {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator __DoNothing()
    {
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireFront(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, CannonFrontSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireLeft(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, CannonLeftSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __FireRight(float power)
    {
        GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, CannonRightSpawnPoint.rotation);
        yield return new WaitForFixedUpdate();
    }

    public void __SetColor(Color color)
    {
        foreach (GameObject sail in sails)
        {
            sail.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public IEnumerator __TurnLookoutLeft(float angle)
    {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator __TurnLookoutRight(float angle)
    {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++)
        {
            Lookout.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();
        }
    }
} 
    


